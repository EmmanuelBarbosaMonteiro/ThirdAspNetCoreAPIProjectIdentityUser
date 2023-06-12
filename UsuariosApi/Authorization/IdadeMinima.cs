using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization;

// Classe que representa o requisito mínimo de idade para autorização
// Class that represents the minimum age requirement for authorization
public class IdadeMinima : IAuthorizationRequirement
{
    public IdadeMinima(int idade)
    {
        Idade = idade;
    }
    public int Idade { get; set; }
}
