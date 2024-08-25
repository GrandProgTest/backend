namespace irs.API.DueDiligence.Domain.Model.Commands;

public record UpdateVendorCommand(int VendorId,string BusinessName,
    string TradeName,
    string TaxId,
    string PhoneNumber,
    string Email,
    string Website,
    string Address,
    string Country,
    decimal AnnualBilling);