using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Interfaces.Assemblers;
using Titan.AniTec.Platform.Livestock.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Livestock.Interfaces.Controllers;

[ApiController]
[Route("api/livestock/animals")]
[Authorize]
public class AnimalController(
    ILivestockQueryService queryService,
    ILivestockCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllAnimalsQuery(CurrentUserId);
        var result = await queryService.GetAllAnimalsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{animalId:int}")]
    public async Task<IActionResult> GetById(int animalId)
    {
        var query = new GetAnimalByIdQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalByIdAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var query = new SearchAnimalsQuery(CurrentUserId, q);
        var result = await queryService.SearchAnimalsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var query = new GetActiveAnimalsQuery(CurrentUserId);
        var result = await queryService.GetActiveAnimalsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("inactive")]
    public async Task<IActionResult> GetInactive()
    {
        var query = new GetInactiveAnimalsQuery(CurrentUserId);
        var result = await queryService.GetInactiveAnimalsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("by-breed/{breedId:int}")]
    public async Task<IActionResult> GetByBreed(int breedId)
    {
        var query = new GetAnimalsByBreedQuery(CurrentUserId, breedId);
        var result = await queryService.GetAnimalsByBreedAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("by-sex/{sex}")]
    public async Task<IActionResult> GetBySex(string sex)
    {
        var query = new GetAnimalsBySexQuery(CurrentUserId, sex);
        var result = await queryService.GetAnimalsBySexAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("by-age-range")]
    public async Task<IActionResult> GetByAgeRange([FromQuery] int? ageMin, [FromQuery] int? ageMax)
    {
        var query = new GetAnimalsByAgeRangeQuery(CurrentUserId, ageMin, ageMax);
        var result = await queryService.GetAnimalsByAgeRangeAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("by-weight-range")]
    public async Task<IActionResult> GetByWeightRange([FromQuery] double? weightMin, [FromQuery] double? weightMax)
    {
        var query = new GetAnimalsByWeightRangeQuery(CurrentUserId, weightMin, weightMax);
        var result = await queryService.GetAnimalsByWeightRangeAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnimalResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterAnimalAsync(command);
        return LivestockActionResultAssembler.ToCreatedActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("{animalId:int}")]
    public async Task<IActionResult> Update(int animalId, [FromBody] UpdateAnimalResource resource)
    {
        var command = LivestockAssembler.ToCommand(CurrentUserId, animalId, resource);
        var result = await commandService.UpdateAnimalAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpDelete("{animalId:int}")]
    public async Task<IActionResult> Delete(int animalId)
    {
        var command = new DeleteAnimalCommand(CurrentUserId, animalId);
        var result = await commandService.DeleteAnimalAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreate([FromBody] IReadOnlyCollection<CreateAnimalResource> resources)
    {
        var commands = resources.Select(r => LivestockAssembler.ToCommand(CurrentUserId, r)).ToList();
        var command = new RegisterAnimalBatchCommand(CurrentUserId, commands);
        var result = await commandService.RegisterAnimalBatchAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("batch")]
    public async Task<IActionResult> BatchUpdate([FromBody] IReadOnlyCollection<BatchUpdateAnimalItemResource> resources)
    {
        var commands = resources.Select(r => LivestockAssembler.ToCommand(CurrentUserId, r)).ToList();
        var result = await commandService.UpdateAnimalBatchAsync(
            new UpdateAnimalBatchCommand(CurrentUserId, commands));
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("search/by-code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var query = new GetAnimalByCodeQuery(CurrentUserId, code);
        var result = await queryService.GetAnimalByCodeAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter([FromQuery] int? breedId, [FromQuery] int? ageMin,
        [FromQuery] int? ageMax, [FromQuery] string? sex, [FromQuery] string? status)
    {
        var query = new FilterAnimalsQuery(CurrentUserId, breedId, ageMin, ageMax, sex, status);
        var result = await queryService.FilterAnimalsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] string? format)
    {
        var query = new GetAllAnimalsQuery(CurrentUserId);
        var result = await queryService.GetAllAnimalsAsync(query);
        if (result.IsFailure)
            return LivestockActionResultAssembler.ToActionResult(result);

        var csv = "Code,Species,Sex,DateOfBirth,Name,Weight,Status\n" +
                  string.Join("\n", result.Data.Select(a =>
                      $"{a.Code},{a.Species},{a.Sex},{a.DateOfBirth:yyyy-MM-dd},{a.Name},{a.Weight},{a.Status}"));

        return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "animals-export.csv");
    }

    [HttpGet("{animalId:int}/genealogy")]
    public async Task<IActionResult> GetGenealogy(int animalId)
    {
        var query = new GetAnimalGenealogyQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalGenealogyAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{animalId:int}/genealogy/ancestors")]
    public async Task<IActionResult> GetAncestors(int animalId)
    {
        var query = new GetAnimalAncestorsQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalAncestorsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{animalId:int}/genealogy/descendants")]
    public async Task<IActionResult> GetDescendants(int animalId)
    {
        var query = new GetAnimalDescendantsQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalDescendantsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{animalId:int}/genealogy/siblings")]
    public async Task<IActionResult> GetSiblings(int animalId)
    {
        var query = new GetAnimalSiblingsQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalSiblingsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{animalId:int}/history")]
    public async Task<IActionResult> GetHistory(int animalId)
    {
        return Ok(new List<object>());
    }

    [HttpGet("by-location/{locationId:int}")]
    public async Task<IActionResult> GetByLocation(int locationId)
    {
        var query = new GetAnimalsByLocationQuery(CurrentUserId, locationId);
        var result = await queryService.GetAnimalsByLocationAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("genealogy/lineage/{familyId}")]
    public async Task<IActionResult> GetLineage(int familyId)
    {
        return Ok(new List<object>());
    }

    [HttpGet("genealogy/export/{animalId:int}")]
    public async Task<IActionResult> ExportGenealogy(int animalId)
    {
        var query = new GetAnimalGenealogyQuery(CurrentUserId, animalId);
        var result = await queryService.GetAnimalGenealogyAsync(query);
        if (result.IsFailure)
            return LivestockActionResultAssembler.ToActionResult(result);
        var animals = result.Value!;
        var resources = animals.Select(LivestockAssembler.ToResource).ToList();
        var csv = "Id,Name,Species,Sex\n" +
                  string.Join("\n", resources.Select(a => $"{a.Id},{a.Name},{a.Species},{a.Sex}"));
        return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", $"genealogy-{animalId}.csv");
    }

    [HttpGet("genealogy/inbreeding-check")]
    public async Task<IActionResult> CheckInbreeding([FromQuery] int animal1, [FromQuery] int animal2)
    {
        var query = new CheckInbreedingQuery(CurrentUserId, animal1, animal2);
        var result = await queryService.CheckInbreedingAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(v => new { IsInbreeding = v }));
    }
}
