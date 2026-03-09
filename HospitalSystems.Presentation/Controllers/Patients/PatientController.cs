using HospitalSystems.Application.Patients.Commands.CreatePatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers.Patients;


[ApiController]
[Route("api/patients")]
public class PatientController:ControllerBase
{
    private readonly ISender _sender;

    public PatientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Policy = "CanCreatePatient")]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var id = await _sender.Send(request, cancellationToken);
        return Ok(id);
    }
}