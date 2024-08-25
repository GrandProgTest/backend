namespace irs.API.DueDiligence.Interfaces.REST.Resources;

public record UpdateVendorResource (string BusinessName,
    string TradeName,
    string TaxId,
    string PhoneNumber,
    string Email,
    string Website,
    string Address,
    string Country,
    decimal AnnualBilling);