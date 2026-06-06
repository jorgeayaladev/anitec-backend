namespace Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

public class Faq
{
    public Faq(string question, string answer, int sortOrder)
    {
        Question = question;
        Answer = answer;
        SortOrder = sortOrder;
    }

    public int Id { get; private set; }
    public string Question { get; private set; }
    public string Answer { get; private set; }
    public int SortOrder { get; private set; }

    public Faq Update(string question, string answer, int sortOrder)
    {
        Question = question;
        Answer = answer;
        SortOrder = sortOrder;
        return this;
    }
}
