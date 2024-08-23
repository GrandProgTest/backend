using irs.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using irs.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Repositories;

namespace irs.API.DueDiligence.Infrastructure.Persistence.EFC.Repositories;

public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
{
    public VendorRepository(AppDbContext context) : base(context)
    {
    }
}