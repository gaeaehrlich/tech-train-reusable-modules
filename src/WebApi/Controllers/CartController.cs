using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechTrain.ReusableModules.WebApi.Models;
using Webapi.ApiValidators;

namespace TechTrain.ReusableModules.WebApi.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet]
        [Route("/users/{userId}/סל")]
        public Task<Cart> GetCart([FromRoute]string userName)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [DisableValidator<ApiLowerCaseValidator>()]
        [Route("/carts/{cartId}/items")]
        public Task<IEnumerable<CartItem>> ListCartItems([FromRoute]int cartId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/carts/{cartId}/items")]
        public Task<IActionResult> AddItemToCart([FromRoute]int cartId, [FromBody]CartItem item)
        {
            throw new NotImplementedException();
        }    
    }
}
