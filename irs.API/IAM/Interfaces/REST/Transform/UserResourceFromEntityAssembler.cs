using irs.API.IAM.Domain.Model.Aggregates;
using irs.API.IAM.Interfaces.REST.Resources;

namespace irs.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.Username);
    }
}