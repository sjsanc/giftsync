using GiftSync.Server.Contexts;
using GiftSync.Server.Dtos;
using GiftSync.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GiftSync.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CircleController : ControllerBase
{
    private readonly ILogger<CircleController> _logger;
    private readonly AppDbContext _dbContext;
    
    public CircleController(ILogger<CircleController> logger, AppDbContext dbContext)
    {   
        _logger = logger;
        _dbContext = dbContext; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CircleDto>>> GetCircles()
    {
        var circles = await _dbContext.Circles
            .Include(c => c.Owner)
            .Include(c => c.UserCircles)
            .ThenInclude(uc => uc.User)
            .ToListAsync();
        
        var circleDtos = CircleDto.FromEntities(circles);
        
        return Ok(circleDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CircleDto>> GetCircle(int id)
    {
        var circle = await _dbContext.Circles
            .Include(c => c.Owner)
            .Include(c => c.UserCircles)
            .ThenInclude(uc => uc.User)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (circle is null)
        {
            return NotFound();
        }
        
        var circleDto = CircleDto.FromEntity(circle);
        
        return Ok(circleDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCircle([FromBody] CreateCircleDto circleDto)
    {       
        var newCircle = new Circle
        {
            Name = circleDto.Name,
            OwnerId = circleDto.OwnerId,
        };
        
        _dbContext.Circles.Add(newCircle);
        
        await _dbContext.SaveChangesAsync();
        
        return new CreatedResult(nameof(Circle), new { id = newCircle.Id });
    }

    [HttpPost("{circleId:int}/members")]
    public async Task<IActionResult> AddUserToCircle(int circleId, [FromBody] AddUserToCircleDto addUserDto)
    {
        var circle = await _dbContext.Circles
            .Include(c => c.UserCircles)
            .FirstOrDefaultAsync(c => c.Id == circleId);

        if (circle is null)
        {
            return NotFound();
        }
        
        var userToAdd = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == addUserDto.UserId);

        if (userToAdd is null)
        {
            return NotFound();
        }

        if (circle.UserCircles.Any(uc => uc.UserId == userToAdd.Id))
        {
            return Conflict();
        }

        var userCircle = new UserCircle
        {
            CircleId = circle.Id,
            UserId = addUserDto.UserId
        };
        
        circle.UserCircles.Add(userCircle);
        
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{circleId:int}/members/{userId:int}")]
    public async Task<ActionResult> RemoveUserFromCircle(int circleId, int userId)
    {
        var circle = await _dbContext.Circles
            .Include(c => c.UserCircles)
            .FirstOrDefaultAsync(c => c.Id == circleId);

        if (circle is null)
        {
            return NotFound();
        }
        
        var userCircle = circle.UserCircles.FirstOrDefault(uc => uc.UserId == userId);

        if (userCircle is null)
        {
            return NotFound();
        }
        
        circle.UserCircles.Remove(userCircle);
        
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }
}