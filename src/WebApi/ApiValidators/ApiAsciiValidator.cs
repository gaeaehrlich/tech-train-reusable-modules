using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Patterns;
using Webapi.Contract;

namespace Webapi.ApiValidators;

public class ApiAsciiValidator :IApiDescriptionValidaor
{
    public ValidationResult Validate(ApiDescription apiDescription)
    {

        
        if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
            return new ValidationResult(false, "All Api methods should have a path");

        var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

        if (!apiDescription.RelativePath.All(char.IsAscii))
            return new ValidationResult(false, "urls should only contain ascii characters");

        return new ValidationResult(true,string.Empty);
    }
}