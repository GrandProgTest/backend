using irs.API.IAM.Domain.Model.Aggregates;
using irs.API.IAM.Domain.Model.Queries;

namespace irs.API.IAM.Domain.Model.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}