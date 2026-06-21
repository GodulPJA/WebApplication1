using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;
namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/")]
public class Controller : ControllerBase
{
    private readonly IDbService _dbService;
    
    public Controller(IDbService dbService)
    {
        _dbService = dbService;
    }
   [HttpGet("courseStatistics")]
   public async Task<ActionResult<List<CourseStatisticsDto>>> GetCourseStatisticsAsync(int? id)
   {
       var result = await _dbService.GetCourseStatisticsAsync(id);
       if (result == null)
       {
           return NotFound();
       }
       return Ok(result);
   }
    
    
}
