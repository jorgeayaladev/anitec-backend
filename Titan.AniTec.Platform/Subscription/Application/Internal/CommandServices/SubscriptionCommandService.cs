using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Titan.AniTec.Platform.Subscription.Application.CommandServices;
using Titan.AniTec.Platform.Subscription.Domain.Model;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;

namespace Titan.AniTec.Platform.Subscription.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionPlanRepository planRepository,
    IUserSubscriptionRepository subscriptionRepository,
    IPaymentMethodRepository paymentMethodRepository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<Result<SubscriptionPlan>> RegisterPlanAsync(RegisterPlanCommand command)
    {
        try
        {
            var existing = await planRepository.FindByNameAsync(command.Name);
            if (existing != null)
                return Result<SubscriptionPlan>.Failure(SubscriptionError.PlanAlreadyExists);

            var plan = new SubscriptionPlan(command.Name, command.Description, command.Price,
                command.Currency, command.BillingCycle, command.MaxAnimals, command.MaxFarms,
                command.MaxUsers, command.Features);
            await planRepository.AddAsync(plan);
            await unitOfWork.CompleteAsync();
            return Result<SubscriptionPlan>.Success(plan);
        }
        catch (Exception)
        {
            return Result<SubscriptionPlan>.Failure(SubscriptionError.InvalidPlanData);
        }
    }

    public async Task<Result<SubscriptionPlan>> UpdatePlanAsync(UpdatePlanCommand command)
    {
        try
        {
            var plan = await planRepository.FindByIdAsync(command.PlanId);
            if (plan == null)
                return Result<SubscriptionPlan>.Failure(SubscriptionError.PlanNotFound);

            plan.UpdateDetails(command.Name, command.Description, command.Price, command.Currency,
                command.BillingCycle, command.MaxAnimals, command.MaxFarms, command.MaxUsers,
                command.Features, command.IsActive);
            await unitOfWork.CompleteAsync();
            return Result<SubscriptionPlan>.Success(plan);
        }
        catch (Exception)
        {
            return Result<SubscriptionPlan>.Failure(SubscriptionError.InvalidPlanData);
        }
    }

    public async Task<Result> DeletePlanAsync(DeletePlanCommand command)
    {
        try
        {
            var plan = await planRepository.FindByIdAsync(command.PlanId);
            if (plan == null)
                return Result.Failure(SubscriptionError.PlanNotFound);

            plan.Deactivate();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(SubscriptionError.InvalidPlanData);
        }
    }

    public async Task<Result<UserSubscription>> SubscribeAsync(SubscribeCommand command)
    {
        try
        {
            var existing = await subscriptionRepository.FindByFarmIdAsync(command.UserId);
            if (existing != null && existing.Status == "active")
                return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionAlreadyActive);

            var plan = await planRepository.FindByIdAsync(command.PlanId);
            if (plan == null)
                return Result<UserSubscription>.Failure(SubscriptionError.PlanNotFound);

            var startDate = DateTime.UtcNow;
            DateTime? endDate = command.TrialEndDate.HasValue
                ? command.TrialEndDate.Value.AddMonths(1)
                : startDate.AddMonths(1);

            var subscription = new UserSubscription(command.UserId, command.PlanId,
                startDate, endDate, command.TrialEndDate, command.AutoRenew);
            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            return Result<UserSubscription>.Success(subscription);
        }
        catch (Exception)
        {
            return Result<UserSubscription>.Failure(SubscriptionError.InvalidSubscriptionData);
        }
    }

    public async Task<Result<UserSubscription>> ChangePlanAsync(ChangePlanCommand command)
    {
        try
        {
            var subscription = await subscriptionRepository.FindByFarmIdAsync(command.UserId);
            if (subscription == null)
                return Result<UserSubscription>.Failure(SubscriptionError.SubscriptionNotFound);

            var plan = await planRepository.FindByIdAsync(command.NewPlanId);
            if (plan == null)
                return Result<UserSubscription>.Failure(SubscriptionError.PlanNotFound);

            subscription.ChangePlan(command.NewPlanId);
            await unitOfWork.CompleteAsync();
            return Result<UserSubscription>.Success(subscription);
        }
        catch (Exception)
        {
            return Result<UserSubscription>.Failure(SubscriptionError.InvalidSubscriptionData);
        }
    }

    public async Task<Result> CancelSubscriptionAsync(CancelSubscriptionCommand command)
    {
        try
        {
            var subscription = await subscriptionRepository.FindByFarmIdAsync(command.UserId);
            if (subscription == null)
                return Result.Failure(SubscriptionError.SubscriptionNotFound);

            subscription.Cancel();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(SubscriptionError.InvalidSubscriptionData);
        }
    }

    public async Task<Result<PaymentMethod>> AddPaymentMethodAsync(AddPaymentMethodCommand command)
    {
        try
        {
            var methods = await paymentMethodRepository.FindByFarmIdAsync(command.UserId);
            var isDefault = !methods.Any();

            var paymentMethod = new PaymentMethod(command.UserId, command.StripePaymentMethodId,
                command.CardBrand, command.Last4, command.ExpMonth, command.ExpYear);
            if (isDefault)
                paymentMethod.SetAsDefault();

            await paymentMethodRepository.AddAsync(paymentMethod);
            await unitOfWork.CompleteAsync();
            return Result<PaymentMethod>.Success(paymentMethod);
        }
        catch (Exception)
        {
            return Result<PaymentMethod>.Failure(SubscriptionError.InvalidPaymentMethodData);
        }
    }

    public async Task<Result> DeletePaymentMethodAsync(DeletePaymentMethodCommand command)
    {
        try
        {
            var method = await paymentMethodRepository.FindByIdAsync(command.PaymentMethodId);
            if (method == null)
                return Result.Failure(SubscriptionError.PaymentMethodNotFound);

            paymentMethodRepository.Remove(method);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(SubscriptionError.InvalidPaymentMethodData);
        }
    }

    public async Task<Result> SetDefaultPaymentMethodAsync(SetDefaultPaymentMethodCommand command)
    {
        try
        {
            var method = await paymentMethodRepository.FindByIdAsync(command.PaymentMethodId);
            if (method == null)
                return Result.Failure(SubscriptionError.PaymentMethodNotFound);

            var currentDefault = await paymentMethodRepository.FindDefaultByFarmIdAsync(command.UserId);
            if (currentDefault != null)
                currentDefault.UnsetDefault();

            method.SetAsDefault();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(SubscriptionError.InvalidPaymentMethodData);
        }
    }

    public async Task<Result> ProcessWebhookAsync(ProcessWebhookCommand command)
    {
        try
        {
            // Placeholder for Stripe webhook processing
            // In production, deserialize StripeEventJson, handle event types:
            // - invoice.paid -> mark invoice as paid
            // - invoice.payment_failed -> mark invoice as failed
            // - customer.subscription.deleted -> cancel subscription
            await Task.CompletedTask;
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(SubscriptionError.WebhookProcessingFailed);
        }
    }
}
