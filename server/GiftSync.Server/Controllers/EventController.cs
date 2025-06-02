using GiftSync.Server.Contexts;
using GiftSync.Server.Dtos;
using GiftSync.Server.Models;
using GiftSync.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftSync.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{   
    private readonly ILogger<EventController> _logger;
    private readonly EventService _eventService;

    public EventController(ILogger<EventController> logger, EventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
    {
        var result = await _eventService.GetEvents();

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(int id)
    {
        var result = await _eventService.GetEventById(id);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEvent(CreateEventDto model)
    {
        var result = await _eventService.CreateEvent(model);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        
        return new CreatedResult(nameof(Circle), new { id = result.Value?.Id });
    }

    [HttpPost("{id:int}")]
    public async Task<ActionResult> AddUserToEvent(int id, int userId)
    {
        var result = await _eventService.AddParticipantToEvent(id, userId);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok();
    }
}