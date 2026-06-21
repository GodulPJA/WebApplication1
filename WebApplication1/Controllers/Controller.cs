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
   [HttpGet("course-Statistics")]
   public async Task<ActionResult<List<CourseStatisticsDto>>> GetCourseStatisticsAsync(int? id)
   {
       var result = await _dbService.GetCourseStatisticsAsync(id);
       if (result == null)
       {
           return NotFound("Course not found");
       }
       return Ok(result);
   }
   [HttpPost("certificates/issue")]
   public async Task<ActionResult<bool>> CommitIssueAsync([FromBody] IssueDto dto)
   {
       var result = await _dbService.AddCertRevAsync(dto);
       if (result == false)
       {
           return  BadRequest("User/Course not found");
       }
       return Created();
   }
   
    
    
}
