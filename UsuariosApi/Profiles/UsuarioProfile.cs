using AutoMapper;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles;

// Classe que representa o perfil de mapeamento do usuário
// Class that represents the user mapping profile
public class UsuarioProfile : Profile
{
    // Construtor que cria o mapeamento de CreateUsuarioDto para Usuario
    // Constructor that creates the mapping from CreateUsuarioDto to Usuario
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}
