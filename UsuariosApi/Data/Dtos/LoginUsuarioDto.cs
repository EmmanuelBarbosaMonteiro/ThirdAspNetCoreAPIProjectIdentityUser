using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos;

// Classe DTO para realizar o login do usuário
// DTO class to perform user login
public class LoginUsuarioDto
{
    // Nome de usuário é obrigatório
    // Username is required
    [Required]
    public string Username { get; set; }

    // Senha é obrigatória
    // Password is required
    [Required]
    public string Password { get; set; }
}
