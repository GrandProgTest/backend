using System.Net.Mime;
using irs.API.DueDiligence.Domain.Model.Commands;
using irs.API.DueDiligence.Domain.Model.Queries;
using irs.API.DueDiligence.Domain.Services;
using irs.API.DueDiligence.Interfaces.REST.Resources;
using irs.API.DueDiligence.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace irs.API.DueDiligence.Interfaces;



[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VendorsController (IVendorQueryService vendorQueryService, 
    IVendorCommandService vendorCommandService): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateVendor([FromBody] CreateVendorResource resource)
    {
        var createVendorCommand = 
            CreateVendorSourceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await vendorCommandService.Handle(createVendorCommand);
        return CreatedAtAction(nameof(GetVendorById), new { id = result.Id }, result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVendorById(int id)
    {
        var getVendorByIdQuery = new GetVendorByIdQuery(id);
        var result = await vendorQueryService.Handle(getVendorByIdQuery);
        var resource = VendorResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    [HttpDelete("{vendorId}")]
    public async Task<ActionResult> DeleteVendor([FromRoute]int vendorId)
    {
        var deleteVendorCommand = new DeleteVendorCommand(vendorId);
        await vendorCommandService.Handle(deleteVendorCommand);
        return Ok("Vendor with id " + vendorId + " has been deleted.");
    }
    [HttpPut("{vendorId}")]
    public async Task<ActionResult> UpdateVendor([FromRoute]int vendorId, [FromBody] UpdateVendorResource updateVendorResource)
    {
        var updateVendorCommand = UpdateVendorSourceCommandFromResourceAssembler.ToCommandFromResource(vendorId, updateVendorResource);
        var vendor = await vendorCommandService.Handle(updateVendorCommand);
        if (vendor is null) return NotFound();
        var resource = VendorResourceFromEntityAssembler.ToResourceFromEntity(vendor);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVendors()
    {
        var getAllVendorsQuery = new GetAllVendorsQuery();
        var vendors = await vendorQueryService.Handle(getAllVendorsQuery);
        var resources = vendors.Select(VendorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}