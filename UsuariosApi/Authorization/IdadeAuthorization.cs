using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosApi.Authorization;

// Classe responsável pela autorização de idade
// Class responsible for age authorization
public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
{
    // Este método verifica se o usuário atende ao requisito mínimo de idade
    // This method checks if the user meets the minimum age requirement
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
    {
        // Busca a reivindicação de data de nascimento do usuário
        // Searches for the user's birth date claim
        var dataNascimentoClaim = context
            .User.FindFirst(claim =>
            claim.Type == ClaimTypes.DateOfBirth);

        if (dataNascimentoClaim is null)
            return Task.CompletedTask;

        var dataNascimento = Convert
            .ToDateTime(dataNascimentoClaim.Value);

        var idadeUsuario =
            DateTime.Today.Year - dataNascimento.Year;

        // Corrige a idade se a data de nascimento for maior que o ano atual (considerando o mês e o dia)
        // Corrects the age if the birth date is greater than the current year (considering the month and day)
        if (dataNascimento >
            DateTime.Today.AddYears(-idadeUsuario))
            idadeUsuario--;

        if (idadeUsuario >= requirement.Idade)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
