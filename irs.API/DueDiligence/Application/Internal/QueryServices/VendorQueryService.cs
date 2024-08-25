using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Queries;
using irs.API.DueDiligence.Domain.Repositories;
using irs.API.DueDiligence.Domain.Services;

namespace irs.API.DueDiligence.Application.Internal.QueryServices;

/// <summary>
/// Service to handle vendor-related queries.
/// </summary>
public class VendorQueryService : IVendorQueryService
{
    private readonly IVendorRepository _vendorRepository;

    public VendorQueryService(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    /// <summary>
    /// Handles the retrieval of a vendor by its ID.
    /// </summary>
    /// <param name="query">The query containing the vendor ID.</param>
    /// <returns>The vendor with the specified ID, or null if not found.</returns>
    public async Task<Vendor?> Handle(GetVendorByIdQuery query)
    {
        return await _vendorRepository.FindByIdAsync(query.Id);
    }

    /// <summary>
    /// Handles the retrieval of all vendors.
    /// </summary>
    /// <param name="query">The query to retrieve all vendors.</param>
    /// <returns>A list of all vendors.</returns>
    public async Task<IEnumerable<Vendor>> Handle(GetAllVendorsQuery query)
    {
        return await _vendorRepository.ListAsync();
    }
}