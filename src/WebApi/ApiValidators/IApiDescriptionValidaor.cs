using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Webapi.Contract;

public interface IApiDescriptionValidaor
{
    public ValidationResult Validate(IReadOnlyList<ApiDescription> apiDescription);

}