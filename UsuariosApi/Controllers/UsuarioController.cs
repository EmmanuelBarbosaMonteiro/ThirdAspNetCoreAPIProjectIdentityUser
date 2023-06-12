using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

// Definindo a classe Controller para usuários
// Defining the Controller class for users
[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }

    // Método POST para cadastrar usuários
    // POST method to register users
    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        // Chamando o serviço para cadastrar o usuário
        // Calling the service to register the user
        await _usuarioService.CadastraUsuario(dto);
        return Ok("Usuário cadastrado!");
    }

    // Método POST para realizar o login
    // POST method to perform login
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
    {
        // Chamando o serviço para realizar o login e pegar o token
        // Calling the service to perform login and get the token
        var token = await _usuarioService.Login(dto);
        // Retorna o token
        // Returns the token
        return Ok(token);
    }
}
