using emr.Application.Commands;
using emr.Application.Dtos;
using emr.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace emr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<PatientDto>), 200)]
    public async Task<IActionResult> GetPatients()
    {
        try
        {
            var res = await _mediator.Send(new GetPatients());

            if (res.IsSuccess)
                return Ok(res.Value);

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Find/{clinicNo}")]
    [ProducesResponseType(typeof(PatientDto), 200)]
    public async Task<IActionResult> GetPatientBy(string clinicNo)
    {
        try
        {
            var res = await _mediator.Send(new GetPatients(clinicNo));

            if (res.IsSuccess)
                return Ok(res.Value.FirstOrDefault());

            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error loading");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("Prescription/New")]
    [ProducesResponseType( 200)]
    public async Task<IActionResult> CreateNewPrescription([FromBody] NewPrescriptionDto dto)
    {
        try
        {
            var res = await _mediator.Send(new PrescribeDrugs(dto));
    
            if (res.IsSuccess)
                return Ok(); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error Creating Prescription");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("Dispense/New")]
    [ProducesResponseType( 200)]
    public async Task<IActionResult> CreateNewDispense([FromBody] List<NewDispenseDto> dtos)
    {
        try
        {
            var res = await _mediator.Send(new DispenseDrugs(dtos));
    
            if (res.IsSuccess)
                return Ok(); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error Creating Prescription");
            return StatusCode(500, e.Message);
        }
    }
}