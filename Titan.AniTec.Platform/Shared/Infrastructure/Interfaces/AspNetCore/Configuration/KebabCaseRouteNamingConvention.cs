using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Titan.AniTec.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;

public partial class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel?.Template != null)
            {
                var template = selector.AttributeRouteModel.Template;
                selector.AttributeRouteModel.Template = ToKebabCase(template);
            }
        }

        foreach (var action in controller.Actions)
        {
            foreach (var selector in action.Selectors)
            {
                if (selector.AttributeRouteModel?.Template != null)
                {
                    var template = selector.AttributeRouteModel.Template;
                    selector.AttributeRouteModel.Template = ToKebabCase(template);
                }
            }
        }
    }

    private static string ToKebabCase(string input)
    {
        return KebabCaseRegex().Replace(input, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z0-9])([A-Z])")]
    private static partial Regex KebabCaseRegex();
}
