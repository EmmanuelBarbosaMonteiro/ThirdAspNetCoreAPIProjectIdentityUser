using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos;

// Classe DTO para criar um novo usuário
// DTO class to create a new user
public class CreateUsuarioDto
{
    // Nome de usuário é obrigatório
    // Username is required
    [Required]
    public string Username { get; set; }

    // Data de nascimento é obrigatória
    // Birth date is required
    [Required]
    public DateTime DataNascimento { get; set; }

    // Senha é obrigatória e deve ser do tipo Password
    // Password is required and should be of type Password
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // Confirmação de senha é obrigatória e deve ser igual à senha
    // Password confirmation is required and should be equal to the password
    [Required]
    [Compare("Password")]
    public string RePassord { get; set; }
}
