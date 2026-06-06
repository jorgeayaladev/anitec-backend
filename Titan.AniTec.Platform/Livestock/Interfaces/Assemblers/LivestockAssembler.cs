using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Livestock.Interfaces.Assemblers;

public static class LivestockAssembler
{
    public static RegisterAnimalCommand ToCommand(int userId, CreateAnimalResource resource)
        => new(userId, resource.Code, resource.Species, resource.Sex, resource.DateOfBirth,
            resource.BreedId, resource.Name, resource.Weight, resource.Color, resource.Notes,
            resource.MotherId, resource.FatherId);

    public static UpdateAnimalCommand ToCommand(int userId, int animalId, UpdateAnimalResource resource)
        => new(userId, animalId, resource.Code, resource.Species, resource.Sex, resource.DateOfBirth,
            resource.BreedId, resource.Name, resource.Weight, resource.Color, resource.Notes,
            resource.Status, resource.PurchaseDate, resource.PurchasePrice,
            resource.MotherId, resource.FatherId);

    public static AnimalResource ToResource(Animal entity)
        => new(entity.Id, entity.FarmId, entity.Code, entity.Species, entity.Sex, entity.DateOfBirth,
            entity.BreedId, entity.Name, entity.Weight, entity.Color, entity.Notes,
            entity.Status, entity.PurchaseDate, entity.PurchasePrice,
            entity.MotherId, entity.FatherId);

    public static RegisterBreedCommand ToCommand(CreateBreedResource resource)
        => new(resource.Name, resource.Species, resource.Description);

    public static UpdateBreedCommand ToCommand(int breedId, UpdateBreedResource resource)
        => new(breedId, resource.Name, resource.Species, resource.Description);

    public static BreedResource ToResource(Breed entity)
        => new(entity.Id, entity.Name, entity.Species, entity.Description);

    public static RegisterBirthCommand ToCommand(int userId, CreateBirthResource resource)
        => new(userId, resource.MotherId, resource.BirthDate, resource.OffspringCount, resource.FatherId, resource.Notes);

    public static UpdateBirthCommand ToCommand(int userId, int birthId, UpdateBirthResource resource)
        => new(userId, birthId, resource.BirthDate, resource.OffspringCount, resource.FatherId, resource.Notes);

    public static BirthResource ToResource(Birth entity)
        => new(entity.Id, entity.FarmId, entity.MotherId, entity.BirthDate, entity.OffspringCount, entity.FatherId, entity.Notes);

    public static RegisterMatingCommand ToCommand(int userId, CreateMatingResource resource)
        => new(userId, resource.FemaleId, resource.MaleId, resource.MatingDate, resource.Notes);

    public static UpdateMatingCommand ToCommand(int userId, int matingId, UpdateMatingResource resource)
        => new(userId, matingId, resource.MatingDate, resource.MaleId, resource.Notes);

    public static MatingResource ToResource(Mating entity)
        => new(entity.Id, entity.FarmId, entity.FemaleId, entity.MaleId, entity.MatingDate, entity.Result, entity.Notes, entity.ConfirmedAt);

    public static RegisterWeaningCommand ToCommand(int userId, CreateWeaningResource resource)
        => new(userId, resource.CalfId, resource.MotherId, resource.WeaningDate, resource.Weight, resource.Notes);

    public static UpdateWeaningCommand ToCommand(int userId, int weaningId, UpdateWeaningResource resource)
        => new(userId, weaningId, resource.WeaningDate, resource.Weight, resource.Notes);

    public static WeaningResource ToResource(Weaning entity)
        => new(entity.Id, entity.FarmId, entity.CalfId, entity.MotherId, entity.WeaningDate, entity.Weight, entity.Notes);

    public static UpdateAnimalCommand ToCommand(int userId, BatchUpdateAnimalItemResource resource)
        => new(userId, resource.AnimalId, resource.Code, resource.Species, resource.Sex, resource.DateOfBirth,
            resource.BreedId, resource.Name, resource.Weight, resource.Color, resource.Notes,
            resource.Status, resource.PurchaseDate, resource.PurchasePrice,
            resource.MotherId, resource.FatherId);
}

public static class LivestockActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            Domain.Model.LivestockError.AnimalNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.BreedNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.BirthNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.MatingNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.WeaningNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.MotherNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.FatherNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error switch
        {
            Domain.Model.LivestockError.AnimalNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.BreedNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.BirthNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.MatingNotFound => new NotFoundResult(),
            Domain.Model.LivestockError.WeaningNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new CreatedResult(string.Empty, result.Value);

        return ToActionResult(result);
    }
}
