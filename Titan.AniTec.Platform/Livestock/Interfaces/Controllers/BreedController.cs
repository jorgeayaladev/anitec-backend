using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Interfaces.Assemblers;
using Titan.AniTec.Platform.Livestock.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Livestock.Interfaces.Controllers;

[ApiController]
[Route("api/livestock/breeds")]
[Authorize]
public class BreedController(
    ILivestockQueryService queryService,
    ILivestockCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllBreedsQuery();
        var result = await queryService.GetAllBreedsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpGet("{breedId:int}")]
    public async Task<IActionResult> GetById(int breedId)
    {
        var query = new GetBreedByIdQuery(breedId);
        var result = await queryService.GetBreedByIdAsync(query);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var query = new SearchBreedsQuery(q);
        var result = await queryService.SearchBreedsAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBreedResource resource)
    {
        var command = LivestockAssembler.ToCommand(resource);
        var result = await commandService.RegisterBreedAsync(command);
        return LivestockActionResultAssembler.ToCreatedActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpPut("{breedId:int}")]
    public async Task<IActionResult> Update(int breedId, [FromBody] UpdateBreedResource resource)
    {
        var command = LivestockAssembler.ToCommand(breedId, resource);
        var result = await commandService.UpdateBreedAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result.Map(LivestockAssembler.ToResource));
    }

    [HttpDelete("{breedId:int}")]
    public async Task<IActionResult> Delete(int breedId)
    {
        var command = new DeleteBreedCommand(breedId);
        var result = await commandService.DeleteBreedAsync(command);
        return LivestockActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("{breedId:int}/animals")]
    public async Task<IActionResult> GetAnimalsByBreed(int breedId)
    {
        var query = new GetAnimalsByBreedQuery(CurrentUserId, breedId);
        var result = await queryService.GetAnimalsByBreedAsync(query);
        return LivestockActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(LivestockAssembler.ToResource).ToList()));
    }
}
