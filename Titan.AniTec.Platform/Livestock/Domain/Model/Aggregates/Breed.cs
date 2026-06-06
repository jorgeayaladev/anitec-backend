using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;

public class Breed : IAuditableEntity
{
    public Breed(string name, string species, string? description)
    {
        Name = name;
        Species = species;
        Description = description;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Species { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Breed UpdateDetails(string name, string species, string? description)
    {
        Name = name;
        Species = species;
        Description = description;
        return this;
    }
}
