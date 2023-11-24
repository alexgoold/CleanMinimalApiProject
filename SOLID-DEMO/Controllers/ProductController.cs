using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class ProductController : ControllerBase
    {
       
        [HttpGet("/products")]
        public async Task<IActionResult> GetProducts()
        {
            throw new NotImplementedException();
        }

        [HttpPost("/products")]
        public async Task<IActionResult> AddProduct(Product newProd)
        {
            throw new NotImplementedException();
        }
    }
}
