using Titan.AniTec.Platform.Admin.Domain.Model;
using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Admin.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Admin.Interfaces.Assemblers;

public static class AdminAssembler
{
    public static FaqResource ToResource(this Faq entity) =>
        new(entity.Id, entity.Question, entity.Answer, entity.SortOrder);

    public static CreateFaqCommand ToCommand(int userId, CreateFaqResource resource) =>
        new(userId, resource.Question, resource.Answer, resource.SortOrder);

    public static UpdateFaqCommand ToCommand(int userId, int faqId, UpdateFaqResource resource) =>
        new(userId, faqId, resource.Question, resource.Answer, resource.SortOrder);

    public static AnnouncementResource ToResource(this Announcement entity) =>
        new(entity.Id, entity.Title, entity.Content, entity.Severity, entity.IsActive, entity.CreatedAt);

    public static CreateAnnouncementCommand ToCommand(int userId, CreateAnnouncementResource resource) =>
        new(userId, resource.Title, resource.Content, resource.Severity);

    public static UpdateAnnouncementCommand ToCommand(int userId, int announcementId, UpdateAnnouncementResource resource) =>
        new(userId, announcementId, resource.Title, resource.Content, resource.Severity);

    public static ContentPageResource ToResource(this ContentPage entity) =>
        new(entity.Id, entity.Slug, entity.Title, entity.Content, entity.UpdatedAt);
}

public static class AdminActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new OkObjectResult(result.Data);
        return MapError(result.Error);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess) return new OkResult();
        return MapError(result.Error);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new CreatedResult(string.Empty, result.Data);
        return MapError(result.Error);
    }

    private static IActionResult MapError(Enum? error) => error switch
    {
        AdminError.AuditLogNotFound or
        AdminError.SettingNotFound or
        AdminError.ContentPageNotFound or
        AdminError.FaqNotFound or
        AdminError.AnnouncementNotFound or
        AdminError.DashboardDataNotFound
            => new NotFoundObjectResult(new { Error = error!.ToString() }),

        AdminError.InvalidSettingData or
        AdminError.InvalidContentData
            => new BadRequestObjectResult(new { Error = error!.ToString() }),

        _ => new StatusCodeResult(500)
    };
}
