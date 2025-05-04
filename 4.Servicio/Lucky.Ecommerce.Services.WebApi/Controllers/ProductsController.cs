using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lucky.Ecommerce.Application.Dto;
using Lucky.Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Lucky.Ecommerce.Services.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsApplication _productsApplication;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsApplication productsApplication, ILogger<ProductsController> logger)
        {
            _productsApplication = productsApplication;
            _logger = logger;
        }

        #region Métodos Asíncronos

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
                return BadRequest(new { message = "El objeto ProductsDto no puede ser nulo." });

            var response = await _productsApplication.InsertAsync(productsDto);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductsDto productsDto)
        {
            if (productsDto == null)
                return BadRequest(new { message = "El objeto ProductsDto no puede ser nulo." });

            var response = await _productsApplication.UpdateAsync(productsDto);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (productId <= 0)
                return BadRequest(new { message = "El ProductId debe ser un número válido mayor que 0." });

            try
            {
                var response = await _productsApplication.DeleteAsync(productId);

                if (!response.IsSuccess)
                {
                    _logger.LogError("Error al eliminar el producto con ID {ProductId}: {Message}", productId, response.Message);
                    return StatusCode(500, new { message = "Error interno al eliminar el producto." });
                }

                if (response.Data == false)
                {
                    _logger.LogWarning("Intento de eliminar un producto inexistente. ID: {ProductId}", productId);
                    return NotFound(new { message = "El producto no existe o ya ha sido eliminado." });
                }

                _logger.LogInformation("Producto eliminado correctamente. ID: {ProductId}", productId);
                return Ok(new { message = "Producto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al intentar eliminar el producto con ID {ProductId}", productId);
                return StatusCode(500, new { message = "Error inesperado al procesar la solicitud." });
            }
        }

        [Authorize]
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int productId)
        {
            if (productId <= 0)
                return BadRequest(new { message = "El ProductId debe ser un número válido mayor que 0." });

            var response = await _productsApplication.GetAsync(productId);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productsApplication.GetAllAsync();

            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }

        #endregion
    }
}
