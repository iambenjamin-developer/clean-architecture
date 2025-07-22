using Application.Common.Models;
using Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class ProductsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
