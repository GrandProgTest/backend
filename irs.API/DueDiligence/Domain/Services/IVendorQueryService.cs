using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Queries;

namespace irs.API.DueDiligence.Domain.Services;

public interface IVendorQueryService
{
    Task<Vendor?> Handle (GetVendorByIdQuery query);
    Task <IEnumerable<Vendor>> Handle (GetAllVendorsQuery query);
}