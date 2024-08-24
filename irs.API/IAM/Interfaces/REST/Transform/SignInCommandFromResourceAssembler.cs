using irs.API.IAM.Domain.Model.Commands;
using irs.API.IAM.Interfaces.REST.Resources;

namespace irs.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}