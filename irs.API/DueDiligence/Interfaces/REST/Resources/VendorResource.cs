namespace irs.API.DueDiligence.Interfaces.REST.Resources;

public record VendorResource(int Id,string BusinessName,
    string TradeName,
    long TaxId,
    long PhoneNumber,
    string Email,
    string Website,
    string Address,
    string Country,
    decimal AnnualBilling);