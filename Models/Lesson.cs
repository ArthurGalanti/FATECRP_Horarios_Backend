using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeleniumBot.Models;
[Keyless]
[Table("FatecHorarios")]
public class Lesson
{
    public string? Course { get; set; }
    public string? Semester { get; set; }
    public string? SubjectName { get; set; }
    public string? TagSiga { get; set; }
    public string? WeekDay { get; set; }
    public string? LessonTime { get; set; }
    public string? TeacherName { get; set; }
    public string? Classroom { get; set; }
    public string? Floor { get; set; }
    public string? LocationURL { get; set; }
    
}