using irs.API.Shared.Domain.Repositories;
using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Commands;
using irs.API.DueDiligence.Domain.Repositories;
using irs.API.DueDiligence.Domain.Services;
using irs.API.DueDiligence.Domain.Model.ValueObjects;
using System;
using System.Threading.Tasks;

namespace irs.API.DueDiligence.Application.Internal.CommandServices;

/// <summary>
/// Service to handle vendor-related commands.
/// </summary>
public class VendorCommandService : IVendorCommandService
{
    private readonly IVendorRepository vendorRepository;
    private readonly IUnitOfWork unitOfWork;

    public VendorCommandService(IVendorRepository vendorRepository, IUnitOfWork unitOfWork)
    {
        this.vendorRepository = vendorRepository;
        this.unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the creation of a new vendor.
    /// </summary>
    /// <param name="command">The command containing vendor creation details.</param>
    /// <returns>The created vendor.</returns>
    /// <exception cref="Exception">Thrown when the country is invalid or an error occurs during creation.</exception>
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

    /// <summary>
    /// Handles the deletion of a vendor.
    /// </summary>
    /// <param name="command">The command containing vendor deletion details.</param>
    /// <exception cref="Exception">Thrown when the vendor is not found or an error occurs during deletion.</exception>
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

    /// <summary>
    /// Handles the update of a vendor.
    /// </summary>
    /// <param name="command">The command containing vendor update details.</param>
    /// <returns>The updated vendor.</returns>
    /// <exception cref="Exception">Thrown when the country is invalid, the vendor is not found, or an error occurs during update.</exception>
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