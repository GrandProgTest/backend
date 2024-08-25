using System.Net.Mime;
using irs.API.DueDiligence.Domain.Model.Commands;
using irs.API.DueDiligence.Domain.Model.Queries;
using irs.API.DueDiligence.Domain.Services;
using irs.API.DueDiligence.Interfaces.REST.Resources;
using irs.API.DueDiligence.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace irs.API.DueDiligence.Interfaces;

/// <summary>
/// Controller to handle vendor-related operations.
/// </summary>
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VendorsController : ControllerBase
{
    private readonly IVendorQueryService _vendorQueryService;
    private readonly IVendorCommandService _vendorCommandService;

    public VendorsController(IVendorQueryService vendorQueryService, IVendorCommandService vendorCommandService)
    {
        _vendorQueryService = vendorQueryService;
        _vendorCommandService = vendorCommandService;
    }

    /// <summary>
    /// Creates a new vendor.
    /// </summary>
    /// <param name="resource">The vendor resource to create.</param>
    /// <returns>The created vendor.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateVendor([FromBody] CreateVendorResource resource)
    {
        var createVendorCommand = CreateVendorSourceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _vendorCommandService.Handle(createVendorCommand);
        return CreatedAtAction(nameof(GetVendorById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves a vendor by its ID.
    /// </summary>
    /// <param name="id">The ID of the vendor to retrieve.</param>
    /// <returns>The vendor with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVendorById(int id)
    {
        var getVendorByIdQuery = new GetVendorByIdQuery(id);
        var result = await _vendorQueryService.Handle(getVendorByIdQuery);
        var resource = VendorResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    /// <summary>
    /// Deletes a vendor by its ID.
    /// </summary>
    /// <param name="vendorId">The ID of the vendor to delete.</param>
    /// <returns>A confirmation message.</returns>
    [HttpDelete("{vendorId}")]
    public async Task<ActionResult> DeleteVendor([FromRoute] int vendorId)
    {
        var deleteVendorCommand = new DeleteVendorCommand(vendorId);
        await _vendorCommandService.Handle(deleteVendorCommand);
        return Ok($"Vendor with id {vendorId} has been deleted.");
    }

    /// <summary>
    /// Updates a vendor by its ID.
    /// </summary>
    /// <param name="vendorId">The ID of the vendor to update.</param>
    /// <param name="updateVendorResource">The updated vendor resource.</param>
    /// <returns>The updated vendor.</returns>
    [HttpPut("{vendorId}")]
    public async Task<ActionResult> UpdateVendor([FromRoute] int vendorId, [FromBody] UpdateVendorResource updateVendorResource)
    {
        var updateVendorCommand = UpdateVendorSourceCommandFromResourceAssembler.ToCommandFromResource(vendorId, updateVendorResource);
        var vendor = await _vendorCommandService.Handle(updateVendorCommand);
        if (vendor is null) return NotFound();
        var resource = VendorResourceFromEntityAssembler.ToResourceFromEntity(vendor);
        return Ok(resource);
    }

    /// <summary>
    /// Retrieves all vendors.
    /// </summary>
    /// <returns>A list of all vendors.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllVendors()
    {
        var getAllVendorsQuery = new GetAllVendorsQuery();
        var vendors = await _vendorQueryService.Handle(getAllVendorsQuery);
        var resources = vendors.Select(VendorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}