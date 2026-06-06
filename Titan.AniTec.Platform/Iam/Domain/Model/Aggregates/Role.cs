using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;

public class Role : IAuditableEntity
{
    public Role(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Role UpdateDetails(string name, string? description)
    {
        Name = name;
        Description = description;
        return this;
    }
}
