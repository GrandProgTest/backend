namespace irs.API.DueDiligence.Domain.Model.Commands;

public record CreateVendorCommand(string BusinessName,
    string TradeName,
    string TaxId,
    string PhoneNumber,
    string Email,
    string Website,
    string Address,
    string Country,
    decimal AnnualBilling);