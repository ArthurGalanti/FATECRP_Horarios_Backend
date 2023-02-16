namespace SeleniumBot.Models.Dtos;

public class LessonDTO
{
    public string? WeekDay { get; set; }
    public List<LessonProperties>? Properties { get; set; }
    
}

public struct LessonProperties
{
    public string? TeacherName { get; set; }
    public string? LessonTime { get; set; }
    public string? SubjectName { get; set; }
    public string? Semester { get; set; }
    public string? Classroom { get; set; }
    public string? Location { get; set; }
}
