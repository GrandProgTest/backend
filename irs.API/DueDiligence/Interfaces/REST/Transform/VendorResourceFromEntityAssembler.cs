using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Interfaces.REST.Resources;

namespace irs.API.DueDiligence.Interfaces.REST.Transform;

public static class VendorResourceFromEntityAssembler
{
    public static VendorResource ToResourceFromEntity(Vendor entity)
    {
        return new VendorResource(entity.Id,
            entity.BusinessName,
            entity.TradeName,
            entity.TaxId,
            entity.PhoneNumber,
            entity.Email,
            entity.Website,
            entity.Address,
            entity.Country,
            entity.AnnualBilling);
    }
}