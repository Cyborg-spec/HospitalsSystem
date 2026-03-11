using HospitalSystems.Application.Patients.Commands.CreatePatient;
using HospitalSystems.Domain.Constants;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers.Patients;


[ApiController]
[Route("api/patients")]
public class PatientController(ISender sender) : ControllerBase
{
    [HttpPost]
    [HasPermission(Permissions.Patients.Create)]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var id = await sender.Send(request, cancellationToken);
        return Ok(id);
    }
}