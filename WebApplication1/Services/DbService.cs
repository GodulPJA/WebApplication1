
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly _2019sbdContext _context; 
    public DbService(_2019sbdContext context)
    {
        _context = context;
    }

    public async Task<List<CourseStatisticsDto>> GetCourseStatisticsAsync(int? id)
    {
        var query = _context.Courses.AsQueryable();
        if (id.HasValue)
        {
            query = query.Where(c => c.Id == id.Value);
        }

        if (!query.Any())
        {
            return null;
        }
        return await query.Select(c => new CourseStatisticsDto()
        {
            CourseId = c.Id,
            Title = c.Title,
            LessonsCount = c.Lessons.Count,
            AverageRating = c.Reviews.Any() ? c.Reviews.Average(r => r.Rating) : 0,
            CertificatesIssued = c.Certificates.Count(),
        }).ToListAsync();
    }

    public async Task<bool> AddCourseAsync(IssueDto issueDto)
    {
        throw new NotImplementedException();
    }

}