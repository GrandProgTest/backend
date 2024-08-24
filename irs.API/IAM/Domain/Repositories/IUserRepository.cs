using irs.API.IAM.Domain.Model.Aggregates;
using irs.API.Shared.Domain.Repositories;

namespace irs.API.IAM.Domain.Model.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
}