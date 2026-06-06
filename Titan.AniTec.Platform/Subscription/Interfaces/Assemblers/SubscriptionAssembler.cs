using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Subscription.Domain.Model;
using Titan.AniTec.Platform.Subscription.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;
using Titan.AniTec.Platform.Subscription.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Subscription.Interfaces.Assemblers;

public static class SubscriptionAssembler
{
    // --- Plan ---
    public static SubscriptionPlanResource ToResource(SubscriptionPlan plan)
        => new(plan.Id, plan.Name, plan.Description, plan.Price, plan.Currency,
            plan.BillingCycle, plan.MaxAnimals, plan.MaxFarms, plan.MaxUsers,
            plan.Features, plan.IsActive);

    public static RegisterPlanCommand ToCommand(CreateSubscriptionPlanResource resource)
        => new(resource.Name, resource.Description, resource.Price, resource.Currency,
            resource.BillingCycle, resource.MaxAnimals, resource.MaxFarms, resource.MaxUsers,
            resource.Features);

    public static UpdatePlanCommand ToCommand(int planId, UpdateSubscriptionPlanResource resource)
        => new(planId, resource.Name, resource.Description, resource.Price, resource.Currency,
            resource.BillingCycle, resource.MaxAnimals, resource.MaxFarms, resource.MaxUsers,
            resource.Features, resource.IsActive);

    // --- Subscription ---
    public static UserSubscriptionResource ToResource(UserSubscription subscription)
        => new(subscription.Id, subscription.FarmId, subscription.PlanId, subscription.Status,
            subscription.StartDate, subscription.EndDate, subscription.TrialEndDate,
            subscription.CanceledAt, subscription.AutoRenew, subscription.StripeSubscriptionId);

    public static SubscribeCommand ToCommand(int userId, SubscribeResource resource)
        => new(userId, resource.PlanId, resource.TrialEndDate, resource.AutoRenew);

    // --- Invoice ---
    public static InvoiceResource ToResource(Invoice invoice)
        => new(invoice.Id, invoice.UserSubscriptionId, invoice.FarmId, invoice.Amount,
            invoice.Currency, invoice.Status, invoice.DueDate, invoice.PaidAt,
            invoice.StripeInvoiceId);

    // --- Payment Method ---
    public static PaymentMethodResource ToResource(PaymentMethod method)
        => new(method.Id, method.FarmId, method.StripePaymentMethodId, method.CardBrand,
            method.Last4, method.ExpMonth, method.ExpYear, method.IsDefault);

    public static AddPaymentMethodCommand ToCommand(int userId, CreatePaymentMethodResource resource)
        => new(userId, resource.StripePaymentMethodId, resource.CardBrand,
            resource.Last4, resource.ExpMonth, resource.ExpYear);

    // --- Payment ---
    public static PaymentResource ToResource(Payment payment)
        => new(payment.Id, payment.InvoiceId, payment.FarmId, payment.Amount,
            payment.Currency, payment.Status, payment.StripePaymentIntentId,
            payment.PaymentMethodId);
}

public static class SubscriptionActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                SubscriptionError.PlanNotFound or
                SubscriptionError.SubscriptionNotFound or
                SubscriptionError.InvoiceNotFound or
                SubscriptionError.PaymentMethodNotFound or
                SubscriptionError.PaymentNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                SubscriptionError.PlanAlreadyExists or
                SubscriptionError.SubscriptionAlreadyActive => new ConflictObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkObjectResult(result.Value);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
            return ToActionResult(result);
        return new CreatedResult(string.Empty, result.Value);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                SubscriptionError.PlanNotFound or
                SubscriptionError.SubscriptionNotFound or
                SubscriptionError.PaymentMethodNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkResult();
    }
}
