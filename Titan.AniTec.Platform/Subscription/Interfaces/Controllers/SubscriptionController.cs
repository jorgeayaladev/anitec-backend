using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Subscription.Application.CommandServices;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;
using Titan.AniTec.Platform.Subscription.Interfaces.Assemblers;
using Titan.AniTec.Platform.Subscription.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Subscription.Interfaces.Controllers;

[ApiController]
[Route("api/subscription")]
[Authorize]
public class SubscriptionController(
    ISubscriptionQueryService queryService,
    ISubscriptionCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // --- Planes ---
    [AllowAnonymous]
    [HttpGet("plans")]
    public async Task<IActionResult> GetAllPlans()
    {
        var query = new GetAllPlansQuery();
        var result = await queryService.GetAllPlansAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [AllowAnonymous]
    [HttpGet("plans/{planId:int}")]
    public async Task<IActionResult> GetPlanById(int planId)
    {
        var query = new GetPlanByIdQuery(planId);
        var result = await queryService.GetPlanByIdAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpPost("plans")]
    public async Task<IActionResult> CreatePlan([FromBody] CreateSubscriptionPlanResource resource)
    {
        var command = SubscriptionAssembler.ToCommand(resource);
        var result = await commandService.RegisterPlanAsync(command);
        return SubscriptionActionResultAssembler.ToCreatedActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpPut("plans/{planId:int}")]
    public async Task<IActionResult> UpdatePlan(int planId, [FromBody] UpdateSubscriptionPlanResource resource)
    {
        var command = SubscriptionAssembler.ToCommand(planId, resource);
        var result = await commandService.UpdatePlanAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpDelete("plans/{planId:int}")]
    public async Task<IActionResult> DeletePlan(int planId)
    {
        var command = new DeletePlanCommand(planId);
        var result = await commandService.DeletePlanAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    // --- Suscripción del usuario ---
    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeResource resource)
    {
        var command = SubscriptionAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.SubscribeAsync(command);
        return SubscriptionActionResultAssembler.ToCreatedActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpGet("my-subscription")]
    public async Task<IActionResult> GetMySubscription()
    {
        var query = new GetMySubscriptionQuery(CurrentUserId);
        var result = await queryService.GetMySubscriptionAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpPut("my-subscription")]
    public async Task<IActionResult> ChangePlan([FromBody] ChangePlanResource resource)
    {
        var command = new ChangePlanCommand(CurrentUserId, resource.NewPlanId);
        var result = await commandService.ChangePlanAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpDelete("my-subscription")]
    public async Task<IActionResult> CancelSubscription()
    {
        var command = new CancelSubscriptionCommand(CurrentUserId);
        var result = await commandService.CancelSubscriptionAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    // --- Facturas ---
    [HttpGet("my-subscription/invoices")]
    public async Task<IActionResult> GetMyInvoices()
    {
        var query = new GetMyInvoicesQuery(CurrentUserId);
        var result = await queryService.GetMyInvoicesAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [HttpGet("my-subscription/invoices/{invoiceId:int}")]
    public async Task<IActionResult> GetMyInvoiceById(int invoiceId)
    {
        var query = new GetMyInvoiceByIdQuery(CurrentUserId, invoiceId);
        var result = await queryService.GetMyInvoiceByIdAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    // --- Métodos de pago ---
    [HttpGet("my-subscription/payment-methods")]
    public async Task<IActionResult> GetMyPaymentMethods()
    {
        var query = new GetMyPaymentMethodsQuery(CurrentUserId);
        var result = await queryService.GetMyPaymentMethodsAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [HttpPost("my-subscription/payment-methods")]
    public async Task<IActionResult> AddPaymentMethod([FromBody] CreatePaymentMethodResource resource)
    {
        var command = SubscriptionAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddPaymentMethodAsync(command);
        return SubscriptionActionResultAssembler.ToCreatedActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpDelete("my-subscription/payment-methods/{paymentMethodId:int}")]
    public async Task<IActionResult> DeletePaymentMethod(int paymentMethodId)
    {
        var command = new DeletePaymentMethodCommand(CurrentUserId, paymentMethodId);
        var result = await commandService.DeletePaymentMethodAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("my-subscription/payment-methods/{paymentMethodId:int}/default")]
    public async Task<IActionResult> SetDefaultPaymentMethod(int paymentMethodId)
    {
        var command = new SetDefaultPaymentMethodCommand(CurrentUserId, paymentMethodId);
        var result = await commandService.SetDefaultPaymentMethodAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    // --- Historial de pagos ---
    [HttpGet("my-subscription/payment-history")]
    public async Task<IActionResult> GetMyPaymentHistory()
    {
        var query = new GetMyPaymentHistoryQuery(CurrentUserId);
        var result = await queryService.GetMyPaymentHistoryAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    // --- Webhook ---
    [AllowAnonymous]
    [HttpPost("webhook")]
    public async Task<IActionResult> ProcessWebhook([FromBody] WebhookResource resource)
    {
        var command = new ProcessWebhookCommand(resource.EventType, resource.StripeEventJson);
        var result = await commandService.ProcessWebhookAsync(command);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    // --- Admin: Suscripciones ---
    [HttpGet("admin/subscriptions")]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var query = new GetAllSubscriptionsQuery();
        var result = await queryService.GetAllSubscriptionsAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [HttpGet("admin/subscriptions/{subscriptionId:int}")]
    public async Task<IActionResult> GetSubscriptionById(int subscriptionId)
    {
        var query = new GetSubscriptionByIdQuery(subscriptionId);
        var result = await queryService.GetSubscriptionByIdAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(SubscriptionAssembler.ToResource));
    }

    [HttpGet("admin/subscriptions/active")]
    public async Task<IActionResult> GetActiveSubscriptions()
    {
        var query = new GetSubscriptionsByStatusQuery("active");
        var result = await queryService.GetSubscriptionsByStatusAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [HttpGet("admin/subscriptions/expired")]
    public async Task<IActionResult> GetExpiredSubscriptions()
    {
        var query = new GetSubscriptionsByStatusQuery("expired");
        var result = await queryService.GetSubscriptionsByStatusAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    [HttpGet("admin/subscriptions/canceled")]
    public async Task<IActionResult> GetCanceledSubscriptions()
    {
        var query = new GetSubscriptionsByStatusQuery("canceled");
        var result = await queryService.GetSubscriptionsByStatusAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(SubscriptionAssembler.ToResource).ToList()));
    }

    // --- Admin: Reportes de ingresos ---
    [HttpGet("admin/revenue")]
    public async Task<IActionResult> GetRevenueReport()
    {
        var query = new GetRevenueReportQuery();
        var result = await queryService.GetRevenueReportAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("admin/revenue/monthly")]
    public async Task<IActionResult> GetMonthlyRevenue()
    {
        var query = new GetMonthlyRevenueQuery();
        var result = await queryService.GetMonthlyRevenueAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("admin/revenue/by-plan")]
    public async Task<IActionResult> GetRevenueByPlan()
    {
        var query = new GetRevenueByPlanQuery();
        var result = await queryService.GetRevenueByPlanAsync(query);
        return SubscriptionActionResultAssembler.ToActionResult(result);
    }
}
