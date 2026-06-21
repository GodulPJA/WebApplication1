namespace WebApplication1.DTOs;

public class CourseStatisticsDto
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;
    
    public int LessonsCount { get; set; }
    
    public double AverageRating { get; set; }
    
    public int CertificatesIssued { get; set; }
   
}