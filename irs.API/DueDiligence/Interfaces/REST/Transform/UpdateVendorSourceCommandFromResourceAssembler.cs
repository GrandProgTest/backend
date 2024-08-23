using irs.API.DueDiligence.Domain.Model.Commands;
using irs.API.DueDiligence.Interfaces.REST.Resources;

namespace irs.API.DueDiligence.Interfaces.REST.Transform;

public static class UpdateVendorSourceCommandFromResourceAssembler
{
    public static UpdateVendorCommand ToCommandFromResource(int vendorId, UpdateVendorResource resource)
    {
        return new UpdateVendorCommand(vendorId, resource.BusinessName,
            resource.TradeName,
            resource.TaxId,
            resource.PhoneNumber,
            resource.Email,
            resource.Website,
            resource.Address,
            resource.Country,
            resource.AnnualBilling);
    }
}