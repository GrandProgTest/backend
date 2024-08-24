using irs.API.IAM.Domain.Model.Aggregates;
using irs.API.IAM.Domain.Model.Commands;

namespace irs.API.IAM.Domain.Model.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(User user, string token)> Handle(SignInCommand command);
}