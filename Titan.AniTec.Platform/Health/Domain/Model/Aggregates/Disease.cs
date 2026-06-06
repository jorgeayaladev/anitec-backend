namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Disease
{
    public Disease(string name, string? description, string? category, string? symptoms, bool? isContagious)
    {
        Name = name;
        Description = description;
        Category = category;
        Symptoms = symptoms;
        IsContagious = isContagious;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public string? Symptoms { get; private set; }
    public bool? IsContagious { get; private set; }

    public Disease UpdateDetails(string name, string? description, string? category, string? symptoms, bool? isContagious)
    {
        Name = name;
        Description = description;
        Category = category;
        Symptoms = symptoms;
        IsContagious = isContagious;
        return this;
    }
}
