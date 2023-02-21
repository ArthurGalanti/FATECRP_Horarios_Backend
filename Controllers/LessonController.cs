using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeleniumBot.Data;
using SeleniumBot.Models;
using SeleniumBot.Services;
using SeleniumBot.ViewModels;

namespace SeleniumBot.Controllers;

[EnableCors(origins: "*", headers: "*", methods: "*")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost("v1/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginData loginData,
        [FromServices] ScrapperService scrapperService,
        [FromServices] CustomizeService customizeService,
        [FromServices] AppDataContext context)
    {
        if (string.IsNullOrEmpty(loginData.Login) || string.IsNullOrEmpty(loginData.Password))
        {
            return StatusCode(404, new ResultViewModel<string>("Usuário ou senha inválidos!"));
        }
        try
        {
            if (!scrapperService.LoginSiga(loginData).Result)
            {
                return StatusCode(404, new ResultViewModel<string>("Usuário ou senha inválidos!"));
            }
            scrapperService.ScrapStudent(out var student,out var shortCourse);
            scrapperService.ScrapLessons(out var lessonsTags);

            var gradeTable = await context
                .Lessons
                .Where(x => x.Course == shortCourse && lessonsTags.Contains(x.TagSiga))
                .ToListAsync();

            var lessons = customizeService.OrderByWeekday(gradeTable);
            
            return Ok(new ResultViewModel<dynamic>(new 
            {
                student,
                lessons
            }));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("500 - Erro interno do servidor!"));
        }
    }
}