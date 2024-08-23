using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Queries;
using irs.API.DueDiligence.Domain.Repositories;
using irs.API.DueDiligence.Domain.Services;

namespace irs.API.DueDiligence.Application.Internal.QueryServices;

public class VendorQueryService(IVendorRepository vendorRepository): IVendorQueryService
{
    public async Task<Vendor?> Handle(GetVendorByIdQuery query)
    {
        return await vendorRepository.FindByIdAsync(query.Id);
    }
}