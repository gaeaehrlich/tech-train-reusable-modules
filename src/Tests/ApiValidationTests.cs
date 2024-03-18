using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System.Reflection;
using TechTrain.ReusableModules.WebApi.Controllers;
using Webapi.ApiValidators;

namespace TechTrain.ReusableModules.Tests;

[TestClass]
public class ApiValidationTests
{
    [TestMethod]
    public void ValidateApi()
    {
        var description = new ApiDescription {
            RelativePath = "/users/{userId}/סל"
        };

        var result = new ApiLowerCaseValidator().Validate(description);


        Assert.AreEqual("urls should only contain ascii characters", result.Message);
    }

    [TestMethod]
    public void ValidateConsistentMethodNames()
    {
        var methods = typeof(CartController).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach(var method in methods)
        {
            if(method.GetCustomAttributes<HttpGetAttribute>().Any() && !(method.Name.StartsWith("Get") || method.Name.StartsWith("List")))
            {
                Assert.Fail();
            }
        }
    }
}