using Application.Common.Models;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.DTOs;
using Application.Products.Queries.GetProductById;
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


        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductDto>> GetProductById(long id)
        {
            return await Mediator.Send(new GetProductByIdQuery { Id = id });
        }


        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult> Update(long id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });

            return NoContent();
        }


    }
}
