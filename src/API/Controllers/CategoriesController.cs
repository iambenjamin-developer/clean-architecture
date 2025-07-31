using Application.Categories.DTOs;
using Application.Categories.Queries.GetAllCategories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para gestionar categorias.
    /// </summary>
    /// <remarks>
    /// Permite consultar, crear, actualizar y eliminar categorias.
    /// Todos los endpoints devuelven códigos HTTP adecuados según el resultado de la operación.
    /// </remarks>
    public class CategoriesController : ApiController
    {

        /// <summary>
        /// Obtiene todas las categorías.
        /// </summary>
        /// <remarks>
        /// Retorna una lista de todas las categorías disponibles en el sistema.
        /// </remarks>
        /// <returns>Lista de categorías.</returns>
        /// <response code="200">Consulta realizada correctamente.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            return await Mediator.Send(new GetAllCategoriesQuery());
        }

    }
}
