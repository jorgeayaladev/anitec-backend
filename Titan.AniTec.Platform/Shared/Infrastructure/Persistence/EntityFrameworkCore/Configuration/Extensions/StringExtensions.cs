using Humanizer;

namespace Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        return input.Underscore();
    }

    public static string ToPlural(this string input)
    {
        return input.Pluralize();
    }
}
