using irs.API.Shared.Domain.Repositories;
using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Commands;
using irs.API.DueDiligence.Domain.Repositories;
using irs.API.DueDiligence.Domain.Services;
using irs.API.DueDiligence.Domain.Model.ValueObjects;
using System;
using System.Threading.Tasks;

namespace irs.API.DueDiligence.Application.Internal.CommandServices;

public class VendorCommandService : IVendorCommandService
{
    private readonly IVendorRepository vendorRepository;
    private readonly IUnitOfWork unitOfWork;

    public VendorCommandService(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
    {
        this.vendorRepository = vendorRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Vendor> Handle(CreateVendorCommand command)
    {
        if (!Enum.TryParse<ECountry>(command.Country, out var country))
        {
            throw new Exception("Invalid country specified");
        }

        var vendor = new Vendor(command);
        try
        {
            await vendorRepository.AddAsync(vendor);
            await unitOfWork.CompleteAsync();
            return vendor;
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while creating the vendor", e);
        }
    }

    public async Task Handle(DeleteVendorCommand command)
    {
        var vendor = await vendorRepository.FindByIdAsync(command.VendorId);
        if (vendor == null)
        {
            throw new Exception("Vendor not found");
        }
        try
        {
            vendorRepository.Remove(vendor);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while deleting the vendor", e);
        }
    }

    public async Task<Vendor> Handle(UpdateVendorCommand command)
    {
        if (!Enum.TryParse<ECountry>(command.Country, out var country))
        {
            throw new Exception("Invalid country specified");
        }

        var vendor = await vendorRepository.FindByIdAsync(command.VendorId);
        if (vendor is null) throw new Exception("Vendor not found");
        vendor.UpdateVendorInformation(command);
        try
        {
            vendorRepository.Update(vendor);
            await unitOfWork.CompleteAsync();
            return vendor;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the vendor: {e.Message}");
            throw;
        }
    }
}