using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

// Classe que representa o serviço de token
// Class that represents the token service
public class TokenService
{
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Método para gerar o token de um usuário
    // Method to generate a user's token
    public string GenerateToken(Usuario usuario)
    {
        // Definindo as claims do token
        // Defining the token's claims
        Claim[] claims = new Claim[]
        {
        new Claim("username", usuario.UserName),
        new Claim("id", usuario.Id),
        new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
        new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        // Definindo a chave de segurança
        // Defining the security key
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

        // Definindo as credenciais de assinatura
        // Defining the signing credentials
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        // Criando o token
        // Creating the token
        var token = new JwtSecurityToken
            (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            );

        // Retornando o token como string
        // Returning the token as string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}