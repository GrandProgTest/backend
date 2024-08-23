using irs.API.DueDiligence.Domain.Model;
using irs.API.DueDiligence.Domain.Model.Commands;

namespace irs.API.DueDiligence.Domain.Services;

public interface IVendorCommandService
{
    Task<Vendor> Handle(CreateVendorCommand command);
    Task<Vendor> Handle(UpdateVendorCommand command);
    Task Handle(DeleteVendorCommand command);
}