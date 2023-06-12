using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models;

// Classe que representa um usuário, herdando da classe IdentityUser
// Class that represents a user, inheriting from the IdentityUser class
public class Usuario : IdentityUser
{
    public DateTime DataNascimento { get; set; }
    public Usuario() : base()
    { 
    }
}
