using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Device.Domain.Model.Aggregates;

public class DeviceType : IAuditableEntity
{
    public DeviceType(string name, string? description, string category,
        string? specifications, string? metrics)
    {
        Name = name;
        Description = description;
        Category = category;
        Specifications = specifications;
        Metrics = metrics;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string Category { get; private set; }
    public string? Specifications { get; private set; }
    public string? Metrics { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public DeviceType UpdateDetails(string name, string? description, string category,
        string? specifications, string? metrics)
    {
        Name = name;
        Description = description;
        Category = category;
        Specifications = specifications;
        Metrics = metrics;
        return this;
    }
}
