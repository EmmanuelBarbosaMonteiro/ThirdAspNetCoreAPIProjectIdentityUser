using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;

    public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    
    [HttpPost]
    // Metodo assincrono, pois se trata de uma operacao que pode ou não retornar um valor.
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        Usuario usuario = _mapper.Map<Usuario>(dto);

        // Aguardando o resultado operacao, ou seja, se o usuario foi cadastrado ou não.
        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        if (resultado.Succeeded)
            return Ok("Usuário cadastrado!");

        throw new ApplicationException("Falha ao cadastrar usuário!"); 
    }
}
