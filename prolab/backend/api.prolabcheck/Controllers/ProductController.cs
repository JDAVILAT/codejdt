using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prolabcheck.application.services;
using prolabcheck.domain.exceptions;

namespace prolabcheck.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigins")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("getbycode")]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var entidades = await _service.GetByCodeAsync(code);
                return Ok(entidades);
            }
            catch (CustomException ex)
            {
                log.Error("Error de aplicación:", ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                log.Error("Error inesperado:", ex);
                return StatusCode(500, new { message = "Se ha producido un error inesperado.", details = ex.Message });
            }
        }
    }
}
