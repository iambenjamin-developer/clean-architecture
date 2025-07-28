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
    /// <summary>
    /// Controlador para gestionar productos.
    /// </summary>
    /// <remarks>
    /// Permite consultar, crear, actualizar y eliminar productos.
    /// Todos los endpoints devuelven códigos HTTP adecuados según el resultado de la operación.
    /// </remarks>
    //[Authorize]
    [AllowAnonymous]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Obtiene una lista paginada de productos.
        /// </summary>
        /// <param name="query">Parámetros de paginación y filtro.</param>
        /// <returns>Lista paginada de productos.</returns>
        /// <response code="200">Consulta realizada correctamente.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPagination([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Obtiene un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <returns>Producto encontrado.</returns>
        /// <response code="200">Producto encontrado correctamente.</response>
        /// <response code="404">No se encontró un producto con el ID especificado.</response>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(long id)
        {
            return await Mediator.Send(new GetProductByIdQuery { Id = id });
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="command">Datos del producto a crear.</param>
        /// <returns>Identificador del producto creado.</returns>
        /// <response code="201">Producto creado correctamente.</response>
        /// <response code="400">Datos inválidos para la creación del producto.</response>
        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> Create(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <param name="command">Datos actualizados del producto.</param>
        /// <response code="204">Producto actualizado correctamente.</response>
        /// <response code="400">El ID del path no coincide con el del cuerpo.</response>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(long id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Elimina un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar.</param>
        /// <response code="204">Producto eliminado correctamente.</response>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }

    }
}
