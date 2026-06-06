namespace Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

public class ContentPage
{
    public ContentPage(string slug, string title, string content)
    {
        Slug = slug;
        Title = title;
        Content = content;
    }

    public int Id { get; private set; }
    public string Slug { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public ContentPage Update(string title, string content)
    {
        Title = title;
        Content = content;
        return this;
    }
}
