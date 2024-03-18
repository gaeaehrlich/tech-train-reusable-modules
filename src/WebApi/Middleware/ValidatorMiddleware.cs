
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using TechTrain.ReusableModules.WebApi.Controllers;
using Webapi.Contract;
using Webapi.ValidatorListener;

namespace Webapi.Middleware
{
    public class ValidatorMiddleware : IMiddleware
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        private readonly IEnumerable<IApiDescriptionValidaor> _validators;
        private readonly IEnumerable<IErrorListener> _errorListeners;

        public ValidatorMiddleware(
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider, 
            IEnumerable<IApiDescriptionValidaor> validators, 
            IEnumerable<IErrorListener> errorListeners)
        {
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _validators = validators;
            _errorListeners = errorListeners; 
        }

        protected virtual void OnValidationErrorFound(ValidatorEvent e)
        {
            foreach (var listener in _errorListeners)
            {
                listener.Invoke(e);
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var validationResult = ValidateApi(context);
            if (validationResult.Result)
            {
                await next(context);
            }
            else
            {
                OnValidationErrorFound(new ValidatorEvent(validationResult.Message));
                throw new Exception(validationResult.Message);//should throw exception?
            }
        }

        private ValidationResult ValidateApi(HttpContext context)
        {
            foreach (var validator in _validators)
            {
                foreach (var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
                {
                    foreach (ApiDescription item in group.Items)
                    {

                        if (item.TryGetMethodInfo(out MethodInfo methodInfo))
                        {
                            Type type = typeof(DisableValidatorAttribute<>);
                            var attributes = methodInfo.GetCustomAttributes(type);
                            
                            if (!attributes.Any(attr => attr.GetType().GetGenericTypeDefinition().Equals(validator)))
                            {
                                var validationResult = validator.Validate(item);
                                if (!validationResult.Result)
                                {
                                    return validationResult;
                                }
                            }
                        }
                    }
                }
            }
            return new ValidationResult(true, string.Empty);
        }

    }
}
