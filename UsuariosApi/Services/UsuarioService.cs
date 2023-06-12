using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

// Classe que representa o serviço do usuário
// Class that represents the user service
public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    // Método para cadastrar um usuário
    // Method to register a user
    public async Task CadastraUsuario(CreateUsuarioDto dto)
    {
        // Mapeando o DTO para o modelo
        // Mapping the DTO to the model
        Usuario usuario = _mapper.Map<Usuario>(dto);

        // Criando o usuário
        // Creating the user
        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        // Se a criação falhar, lança uma exceção
        // If creation fails, throws an exception
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }

    // Método para realizar o login do usuário
    // Method to perform the user login
    public async Task<string> Login(LoginUsuarioDto dto)
    {
        // Verificando as credenciais do usuário
        // Checking the user's credentials
        var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

        // Se as credenciais não forem válidas, lança uma exceção
        // If the credentials are not valid, throws an exception
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuário não autenticado!");
        }

        // Buscando o usuário
        // Fetching the user
        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

        // Gerando o token do usuário
        // Generating the user's token
        var token = _tokenService.GenerateToken(usuario);

        // Retornando o token
        // Returning the token
        return token;

    }
}