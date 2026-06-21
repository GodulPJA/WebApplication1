using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;
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

    public async Task<bool> AddCertRevAsync(IssueDto dto)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == dto.CourseId);
            if (!courseExists)
            {
                return false;
            }
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists) 
            {              
                return false;
            }
            var newCertificate = new Certificate
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                IssueDate = DateTime.Now,
                CertificateCode = Guid.NewGuid()
            };

            var newReview = new Review
            {
                UserId = dto.UserId,
                CourseId = dto.CourseId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };
            await _context.Certificates.AddAsync(newCertificate);
            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            return false;
        }
    }


}