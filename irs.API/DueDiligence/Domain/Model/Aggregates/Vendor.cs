using irs.API.DueDiligence.Domain.Model.Commands;

namespace irs.API.DueDiligence.Domain.Model;

public partial class Vendor
{
    public int Id { get; set; }
    public string BusinessName { get; set; }
    public string TradeName { get; set; }
    public long TaxId { get; set; }
    public long PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public decimal AnnualBilling { get; set; }

    public Vendor() { }

    public Vendor(CreateVendorCommand command)
    {
        this.BusinessName = command.BusinessName;
        this.TradeName = command.TradeName;
        this.TaxId = command.TaxId;
        this.PhoneNumber = command.PhoneNumber;
        this.Email = command.Email;
        this.Website = command.Website;
        this.Address = command.Address;
        this.Country = command.Country;
        this.AnnualBilling = command.AnnualBilling;
    }

    public Vendor UpdateVendorInformation(UpdateVendorCommand command)
    {
        BusinessName = command.BusinessName;
        TradeName = command.TradeName;
        TaxId = command.TaxId;
        PhoneNumber = command.PhoneNumber;
        Email = command.Email;
        Website = command.Website;
        Address = command.Address;
        Country = command.Country;
        AnnualBilling = command.AnnualBilling;
        return this;
    }
}