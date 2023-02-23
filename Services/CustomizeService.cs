using System.Globalization;
using SeleniumBot.Models;
using SeleniumBot.Models.Dtos;

namespace SeleniumBot.Services;

public class CustomizeService
{
    public List<LessonDTO> OrderByWeekday(List<Lesson> gradeTable)
    {
        var weekdayMap = new List<string> {"Segunda-Feira","Terça-Feira","Quarta-Feira","Quinta-Feira", "Sexta-Feira", "Sábado"};
        var currentDayOfWeek = DateTime.Now.DayOfWeek;
        var textInfo = new CultureInfo("pt-BR",false).TextInfo;
        var currentDayName = textInfo.ToTitleCase(DateTimeFormatInfo.CurrentInfo.GetDayName(currentDayOfWeek).ToLower());
        weekdayMap.Remove(currentDayName);
        weekdayMap.Insert(0, currentDayName + "- (Hoje)");

        return (from day in weekdayMap
            let lessonsDay = gradeTable.Where(x => x.WeekDay == day.Replace("- (Hoje)", ""))
                .OrderBy(x => x.LessonTime)
                .Select(x => new LessonProperties
                {
                    TeacherName = x.TeacherName,
                    LessonTime = x.LessonTime,
                    SubjectName = x.SubjectName,
                    Semester = x.Semester,
                    Classroom = $"{x.Classroom} - {x.Floor}",
                    Location = x.LocationURL
                })
                .ToList()
            select new LessonDTO() { WeekDay = day, Properties = lessonsDay }).ToList();
    }
}
