

using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IDbService
{
    public  Task<List<CourseStatisticsDto>> GetCourseStatisticsAsync(int? id);
    //public Task<bool> AddCourseAsync();
}