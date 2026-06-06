namespace Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

public class SystemSetting
{
    public SystemSetting(string key, string value, string category)
    {
        Key = key;
        Value = value;
        Category = category;
    }

    public int Id { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string Category { get; private set; }

    public SystemSetting UpdateValue(string value)
    {
        Value = value;
        return this;
    }
}
