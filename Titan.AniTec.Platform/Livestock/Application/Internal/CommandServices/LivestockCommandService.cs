using Titan.AniTec.Platform.Livestock.Domain.Model;
using Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Livestock.Application.Internal.CommandServices;

public class LivestockCommandService(
    IAnimalRepository animalRepository,
    IBreedRepository breedRepository,
    IBirthRepository birthRepository,
    IMatingRepository matingRepository,
    IWeaningRepository weaningRepository,
    IUnitOfWork unitOfWork) : ILivestockCommandService
{
    public async Task<Result<Animal>> RegisterAnimalAsync(RegisterAnimalCommand command)
    {
        try
        {
            var existing = await animalRepository.FindByCodeAsync(command.Code);
            if (existing != null)
                return Result<Animal>.Failure(LivestockError.AnimalAlreadyExists);

            var animal = new Animal(command.UserId, command.Code, command.Species, command.Sex, command.DateOfBirth);
            animal.UpdateDetails(command.Code, command.Species, command.Sex, command.DateOfBirth,
                command.BreedId, command.Name, command.Weight, command.Color, command.Notes,
                "active", null, null);
            animal.SetParents(command.MotherId, command.FatherId);

            await animalRepository.AddAsync(animal);
            await unitOfWork.CompleteAsync();
            return Result<Animal>.Success(animal);
        }
        catch (Exception)
        {
            return Result<Animal>.Failure(LivestockError.InvalidAnimalData);
        }
    }

    public async Task<Result<Animal>> UpdateAnimalAsync(UpdateAnimalCommand command)
    {
        try
        {
            var animal = await animalRepository.FindByIdAsync(command.AnimalId);
            if (animal == null)
                return Result<Animal>.Failure(LivestockError.AnimalNotFound);

            animal.UpdateDetails(command.Code, command.Species, command.Sex, command.DateOfBirth,
                command.BreedId, command.Name, command.Weight, command.Color, command.Notes,
                command.Status, command.PurchaseDate, command.PurchasePrice);
            animal.SetParents(command.MotherId, command.FatherId);

            await unitOfWork.CompleteAsync();
            return Result<Animal>.Success(animal);
        }
        catch (Exception)
        {
            return Result<Animal>.Failure(LivestockError.InvalidAnimalData);
        }
    }

    public async Task<Result> DeleteAnimalAsync(DeleteAnimalCommand command)
    {
        try
        {
            var animal = await animalRepository.FindByIdAsync(command.AnimalId);
            if (animal == null)
                return Result.Failure(LivestockError.AnimalNotFound);

            animalRepository.Remove(animal);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.AnimalNotFound);
        }
    }

    public async Task<Result<Breed>> RegisterBreedAsync(RegisterBreedCommand command)
    {
        try
        {
            var existing = await breedRepository.FindByNameAndSpeciesAsync(command.Name, command.Species);
            if (existing != null)
                return Result<Breed>.Failure(LivestockError.BreedAlreadyExists);

            var breed = new Breed(command.Name, command.Species, command.Description);
            await breedRepository.AddAsync(breed);
            await unitOfWork.CompleteAsync();
            return Result<Breed>.Success(breed);
        }
        catch (Exception)
        {
            return Result<Breed>.Failure(LivestockError.InvalidBreedData);
        }
    }

    public async Task<Result<Breed>> UpdateBreedAsync(UpdateBreedCommand command)
    {
        try
        {
            var breed = await breedRepository.FindByIdAsync(command.BreedId);
            if (breed == null)
                return Result<Breed>.Failure(LivestockError.BreedNotFound);

            breed.UpdateDetails(command.Name, command.Species, command.Description);
            await unitOfWork.CompleteAsync();
            return Result<Breed>.Success(breed);
        }
        catch (Exception)
        {
            return Result<Breed>.Failure(LivestockError.InvalidBreedData);
        }
    }

    public async Task<Result> DeleteBreedAsync(DeleteBreedCommand command)
    {
        try
        {
            var breed = await breedRepository.FindByIdAsync(command.BreedId);
            if (breed == null)
                return Result.Failure(LivestockError.BreedNotFound);

            breedRepository.Remove(breed);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.BreedNotFound);
        }
    }

    public async Task<Result<Birth>> RegisterBirthAsync(RegisterBirthCommand command)
    {
        try
        {
            var mother = await animalRepository.FindByIdAsync(command.MotherId);
            if (mother == null)
                return Result<Birth>.Failure(LivestockError.MotherNotFound);

            var birth = new Birth(command.UserId, command.MotherId, command.BirthDate, command.OffspringCount, command.FatherId, command.Notes);
            await birthRepository.AddAsync(birth);
            await unitOfWork.CompleteAsync();
            return Result<Birth>.Success(birth);
        }
        catch (Exception)
        {
            return Result<Birth>.Failure(LivestockError.InvalidBirthData);
        }
    }

    public async Task<Result<Birth>> UpdateBirthAsync(UpdateBirthCommand command)
    {
        try
        {
            var birth = await birthRepository.FindByIdAsync(command.BirthId);
            if (birth == null)
                return Result<Birth>.Failure(LivestockError.BirthNotFound);

            birth.UpdateDetails(command.BirthDate, command.OffspringCount, command.FatherId, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Birth>.Success(birth);
        }
        catch (Exception)
        {
            return Result<Birth>.Failure(LivestockError.InvalidBirthData);
        }
    }

    public async Task<Result> DeleteBirthAsync(DeleteBirthCommand command)
    {
        try
        {
            var birth = await birthRepository.FindByIdAsync(command.BirthId);
            if (birth == null)
                return Result.Failure(LivestockError.BirthNotFound);

            birthRepository.Remove(birth);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.BirthNotFound);
        }
    }

    public async Task<Result<Mating>> RegisterMatingAsync(RegisterMatingCommand command)
    {
        try
        {
            var mating = new Mating(command.UserId, command.FemaleId, command.MaleId, command.MatingDate, command.Notes);
            await matingRepository.AddAsync(mating);
            await unitOfWork.CompleteAsync();
            return Result<Mating>.Success(mating);
        }
        catch (Exception)
        {
            return Result<Mating>.Failure(LivestockError.InvalidMatingData);
        }
    }

    public async Task<Result<Mating>> UpdateMatingAsync(UpdateMatingCommand command)
    {
        try
        {
            var mating = await matingRepository.FindByIdAsync(command.MatingId);
            if (mating == null)
                return Result<Mating>.Failure(LivestockError.MatingNotFound);

            mating.UpdateDetails(command.MatingDate, command.MaleId, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Mating>.Success(mating);
        }
        catch (Exception)
        {
            return Result<Mating>.Failure(LivestockError.InvalidMatingData);
        }
    }

    public async Task<Result> DeleteMatingAsync(DeleteMatingCommand command)
    {
        try
        {
            var mating = await matingRepository.FindByIdAsync(command.MatingId);
            if (mating == null)
                return Result.Failure(LivestockError.MatingNotFound);

            matingRepository.Remove(mating);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.MatingNotFound);
        }
    }

    public async Task<Result<Mating>> ConfirmMatingAsync(ConfirmMatingCommand command)
    {
        try
        {
            var mating = await matingRepository.FindByIdAsync(command.MatingId);
            if (mating == null)
                return Result<Mating>.Failure(LivestockError.MatingNotFound);

            mating.ConfirmResult(command.Result);
            await unitOfWork.CompleteAsync();
            return Result<Mating>.Success(mating);
        }
        catch (Exception)
        {
            return Result<Mating>.Failure(LivestockError.InvalidMatingData);
        }
    }

    public async Task<Result<Weaning>> RegisterWeaningAsync(RegisterWeaningCommand command)
    {
        try
        {
            var weaning = new Weaning(command.UserId, command.CalfId, command.MotherId, command.WeaningDate, command.Weight, command.Notes);
            await weaningRepository.AddAsync(weaning);
            await unitOfWork.CompleteAsync();
            return Result<Weaning>.Success(weaning);
        }
        catch (Exception)
        {
            return Result<Weaning>.Failure(LivestockError.InvalidWeaningData);
        }
    }

    public async Task<Result<Weaning>> UpdateWeaningAsync(UpdateWeaningCommand command)
    {
        try
        {
            var weaning = await weaningRepository.FindByIdAsync(command.WeaningId);
            if (weaning == null)
                return Result<Weaning>.Failure(LivestockError.WeaningNotFound);

            weaning.UpdateDetails(command.WeaningDate, command.Weight, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Weaning>.Success(weaning);
        }
        catch (Exception)
        {
            return Result<Weaning>.Failure(LivestockError.InvalidWeaningData);
        }
    }

    public async Task<Result> DeleteWeaningAsync(DeleteWeaningCommand command)
    {
        try
        {
            var weaning = await weaningRepository.FindByIdAsync(command.WeaningId);
            if (weaning == null)
                return Result.Failure(LivestockError.WeaningNotFound);

            weaningRepository.Remove(weaning);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.WeaningNotFound);
        }
    }

    public async Task<Result> RegisterAnimalBatchAsync(RegisterAnimalBatchCommand command)
    {
        try
        {
            foreach (var item in command.Animals)
            {
                var existing = await animalRepository.FindByCodeAsync(item.Code);
                if (existing != null)
                    return Result.Failure(LivestockError.AnimalAlreadyExists);

                var animal = new Animal(command.UserId, item.Code, item.Species, item.Sex, item.DateOfBirth);
                animal.UpdateDetails(item.Code, item.Species, item.Sex, item.DateOfBirth,
                    item.BreedId, item.Name, item.Weight, item.Color, item.Notes,
                    "active", null, null);
                animal.SetParents(item.MotherId, item.FatherId);
                await animalRepository.AddAsync(animal);
            }

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.InvalidAnimalData);
        }
    }

    public async Task<Result> UpdateAnimalBatchAsync(UpdateAnimalBatchCommand command)
    {
        try
        {
            foreach (var item in command.Animals)
            {
                var animal = await animalRepository.FindByIdAsync(item.AnimalId);
                if (animal == null)
                    return Result.Failure(LivestockError.AnimalNotFound);

                animal.UpdateDetails(item.Code, item.Species, item.Sex, item.DateOfBirth,
                    item.BreedId, item.Name, item.Weight, item.Color, item.Notes,
                    item.Status, item.PurchaseDate, item.PurchasePrice);
                animal.SetParents(item.MotherId, item.FatherId);
            }

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(LivestockError.InvalidAnimalData);
        }
    }
}
