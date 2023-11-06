using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Estou aqui!");
        }
    }
}
