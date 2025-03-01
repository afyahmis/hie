using MediatR;
using Microsoft.AspNetCore.Mvc;
using pis.Application.Commands;
using pis.Application.Dtos;
using pis.Application.Queries;
using Serilog;

namespace pis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationController : ControllerBase
{
    private readonly IMediator _mediator;

    public MedicationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<DrugDto>), 200)]
    public async Task<IActionResult> GetStock()
    {
        try
        {
            var res = await _mediator.Send(new GetDrugs());

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
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientDto>), 200)]
    public async Task<IActionResult> GetClients()
    {
        try
        {
            var res = await _mediator.Send(new GetClients());

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
    
    [HttpGet("Find/{id}")]
    [ProducesResponseType(typeof(ClientDto), 200)]
    public async Task<IActionResult> GetClientById(Guid id)
    {
        try
        {
            var res = await _mediator.Send(new GetClients(id));

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
    
    [HttpGet("Find/Ref/{refId}")]
    [ProducesResponseType(typeof(ClientDto), 200)]
    public async Task<IActionResult> GetClientByRef(string refId)
    {
        try
        {
            var res = await _mediator.Send(new GetClients(refId));

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
    
    [HttpPost("Request/New")]
    [ProducesResponseType( 200)]
    public async Task<IActionResult> CreateRequest([FromBody] NewMedicationRequestDto dto)
    {
        try
        {
            var res = await _mediator.Send(new GenerateRequest(dto));
    
            if (res.IsSuccess)
                return Ok(); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error Creating MedicationRequest");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("Issue")]
    [ProducesResponseType( 200)]
    public async Task<IActionResult> IssueMeds([FromBody] IssueMedicationDto dtos)
    {
        try
        {
            var res = await _mediator.Send(new IssueDrug(dtos));
    
            if (res.IsSuccess)
                return Ok(); 
    
            throw new Exception($"An error occured: {res.Error}");
        }
        catch (Exception e)
        {
            Log.Error(e,"Error Creating MedicationRequest");
            return StatusCode(500, e.Message);
        }
    }
}