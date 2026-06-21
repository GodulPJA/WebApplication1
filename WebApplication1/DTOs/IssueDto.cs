namespace WebApplication1.DTOs;

public class IssueDto
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    
}