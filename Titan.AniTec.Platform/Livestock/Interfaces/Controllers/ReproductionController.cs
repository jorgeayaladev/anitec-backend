using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Interfaces.Assemblers;
using Titan.AniTec.Platform.Livestock.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Livestock.Interfaces.Controllers;

[ApiController]
[Route("api/livestock/reproduction")]
[Authorize]
public class ReproductionController(
    ILivestockQueryService queryService,
    ILivestockCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // Births
    [HttpGet("births")]
    public async Task<IActionResult> GetAllBirths()
    {
        var query = new GetAllBirthsQuery(CurrentUserId);
        var result = await queryService.GetAllBirthsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("births/{birthId:int}")]
    public async Task<IActionResult> GetBirthById(int birthId)
    {
        var query = new GetBirthByIdQuery(CurrentUserId, birthId);
        var result = await queryService.GetBirthByIdAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("births/by-date-range")]
    public async Task<IActionResult> GetBirthsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetBirthsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetBirthsByDateRangeAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("births/by-mother/{motherId:int}")]
    public async Task<IActionResult> GetBirthsByMother(int motherId)
    {
        var query = new GetBirthsByMotherQuery(CurrentUserId, motherId);
        var result = await queryService.GetBirthsByMotherAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("births/by-father/{fatherId:int}")]
    public async Task<IActionResult> GetBirthsByFather(int fatherId)
    {
        var query = new GetBirthsByFatherQuery(CurrentUserId, fatherId);
        var result = await queryService.GetBirthsByFatherAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpPost("births")]
    public async Task<IActionResult> CreateBirth([FromBody] CreateBirthResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterBirthAsync(command);
        return LivestockActionResultAssembler.ToCreatedActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("births/{birthId:int}")]
    public async Task<IActionResult> UpdateBirth(int birthId, [FromBody] UpdateBirthResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, birthId, resource);
        var result = await commandService.UpdateBirthAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpDelete("births/{birthId:int}")]
    public async Task<IActionResult> DeleteBirth(int birthId)
    {
        var command = new DeleteBirthCommand(CurrentUserId, birthId);
        var result = await commandService.DeleteBirthAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    // Matings
    [HttpGet("matings")]
    public async Task<IActionResult> GetAllMatings()
    {
        var query = new GetAllMatingsQuery(CurrentUserId);
        var result = await queryService.GetAllMatingsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("matings/{matingId:int}")]
    public async Task<IActionResult> GetMatingById(int matingId)
    {
        var query = new GetMatingByIdQuery(CurrentUserId, matingId);
        var result = await queryService.GetMatingByIdAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("matings/pending")]
    public async Task<IActionResult> GetPendingMatings()
    {
        var query = new GetPendingMatingsQuery(CurrentUserId);
        var result = await queryService.GetPendingMatingsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("matings/by-female/{femaleId:int}")]
    public async Task<IActionResult> GetMatingsByFemale(int femaleId)
    {
        var query = new GetMatingsByFemaleQuery(CurrentUserId, femaleId);
        var result = await queryService.GetMatingsByFemaleAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("matings/by-male/{maleId:int}")]
    public async Task<IActionResult> GetMatingsByMale(int maleId)
    {
        var query = new GetMatingsByMaleQuery(CurrentUserId, maleId);
        var result = await queryService.GetMatingsByMaleAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("matings/by-date-range")]
    public async Task<IActionResult> GetMatingsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetMatingsByDateRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetMatingsByDateRangeAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpPost("matings")]
    public async Task<IActionResult> CreateMating([FromBody] CreateMatingResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterMatingAsync(command);
        return LivestockActionResultAssembler.ToCreatedActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("matings/{matingId:int}")]
    public async Task<IActionResult> UpdateMating(int matingId, [FromBody] UpdateMatingResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, matingId, resource);
        var result = await commandService.UpdateMatingAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("matings/{matingId:int}/confirm")]
    public async Task<IActionResult> ConfirmMating(int matingId, [FromBody] ConfirmMatingResource resource)
    {
        var command = new ConfirmMatingCommand(CurrentUserId, matingId, resource.Result);
        var result = await commandService.ConfirmMatingAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpDelete("matings/{matingId:int}")]
    public async Task<IActionResult> DeleteMating(int matingId)
    {
        var command = new DeleteMatingCommand(CurrentUserId, matingId);
        var result = await commandService.DeleteMatingAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    // Weanings
    [HttpGet("weanings")]
    public async Task<IActionResult> GetAllWeanings()
    {
        var query = new GetAllWeaningsQuery(CurrentUserId);
        var result = await queryService.GetAllWeaningsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("weanings/{weaningId:int}")]
    public async Task<IActionResult> GetWeaningById(int weaningId)
    {
        var query = new GetWeaningByIdQuery(CurrentUserId, weaningId);
        var result = await queryService.GetWeaningByIdAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("weanings/by-calf/{calfId:int}")]
    public async Task<IActionResult> GetWeaningsByCalf(int calfId)
    {
        var query = new GetWeaningsByCalfQuery(CurrentUserId, calfId);
        var result = await queryService.GetWeaningsByCalfAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("weanings/by-mother/{motherId:int}")]
    public async Task<IActionResult> GetWeaningsByMother(int motherId)
    {
        var query = new GetWeaningsByMotherQuery(CurrentUserId, motherId);
        var result = await queryService.GetWeaningsByMotherAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpPost("weanings")]
    public async Task<IActionResult> CreateWeaning([FromBody] CreateWeaningResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterWeaningAsync(command);
        return LivestockActionResultAssembler.ToCreatedActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("weanings/{weaningId:int}")]
    public async Task<IActionResult> UpdateWeaning(int weaningId, [FromBody] UpdateWeaningResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, weaningId, resource);
        var result = await commandService.UpdateWeaningAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpDelete("weanings/{weaningId:int}")]
    public async Task<IActionResult> DeleteWeaning(int weaningId)
    {
        var command = new DeleteWeaningCommand(CurrentUserId, weaningId);
        var result = await commandService.DeleteWeaningAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    // Statistics
    [HttpGet("births/statistics")]
    public async Task<IActionResult> GetBirthsStatistics()
    {
        var query = new GetAllBirthsQuery(CurrentUserId);
        var result = await queryService.GetAllBirthsAsync(query);
        if (result.IsFailure)
            return LivestockActionResultAssembler.ToActionResult(result);
        var births = result.Value!;
        return Ok(new
        {
            TotalBirths = births.Count,
            MultipleBirths = births.Count(b => b.OffspringCount > 1),
            AverageOffspring = births.Any() ? births.Average(b => b.OffspringCount) : 0
        });
    }

    [HttpGet("matings/statistics")]
    public async Task<IActionResult> GetMatingsStatistics()
    {
        var query = new GetAllMatingsQuery(CurrentUserId);
        var result = await queryService.GetAllMatingsAsync(query);
        if (result.IsFailure)
            return LivestockActionResultAssembler.ToActionResult(result);
        var matings = result.Value!;
        return Ok(new
        {
            TotalMatings = matings.Count,
            Confirmed = matings.Count(m => m.Result == "confirmed"),
            Pending = matings.Count(m => m.Result == "pending" || string.IsNullOrEmpty(m.Result)),
            Failed = matings.Count(m => m.Result == "failed")
        });
    }

    [HttpGet("weanings/upcoming")]
    public async Task<IActionResult> GetUpcomingWeanings()
    {
        var query = new GetAllWeaningsQuery(CurrentUserId);
        var result = await queryService.GetAllWeaningsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }
}
