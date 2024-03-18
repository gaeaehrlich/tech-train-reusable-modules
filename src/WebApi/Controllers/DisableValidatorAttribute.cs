
namespace TechTrain.ReusableModules.WebApi.Controllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableValidatorAttribute<T>  : Attribute where T : IApiDescriptionValidaor
    {
    }
}