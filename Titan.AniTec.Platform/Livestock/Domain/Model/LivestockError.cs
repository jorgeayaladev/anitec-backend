namespace Titan.AniTec.Platform.Livestock.Domain.Model;

public enum LivestockError
{
    AnimalNotFound,
    AnimalAlreadyExists,
    BreedNotFound,
    BreedAlreadyExists,
    BirthNotFound,
    MatingNotFound,
    WeaningNotFound,
    InvalidAnimalData,
    InvalidBreedData,
    InvalidBirthData,
    InvalidMatingData,
    InvalidWeaningData,
    FarmNotFound,
    MotherNotFound,
    FatherNotFound,
    InbreedingDetected,
    UnauthorizedAccess
}
