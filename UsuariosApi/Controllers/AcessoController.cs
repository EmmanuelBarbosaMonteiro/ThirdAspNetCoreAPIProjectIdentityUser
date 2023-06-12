using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers;

// Definindo a classe Controller para acessos
// Defining the Controller class for accesses
[ApiController]
[Route("[Controller]")]
public class AcessoController : ControllerBase 
{
    [HttpGet]
    [Authorize(Policy = "IdadeMinima")]
    public IActionResult Get()
    {
        return Ok("Acesso permitido!");
    }
}
