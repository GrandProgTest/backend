using irs.API.IAM.Domain.Model.Aggregates;
using irs.API.IAM.Interfaces.REST.Resources;

namespace irs.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, token);
    } 
}