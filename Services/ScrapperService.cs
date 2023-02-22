using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumBot.Models;

namespace SeleniumBot.Services;

public class ScrapperService
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    
    public  ScrapperService()
    {
        ChromeOptions options = new();
        options.AddArguments("headless");
        _driver = new ChromeDriver(options);
        _wait = new WebDriverWait(_driver, timeout: TimeSpan.FromSeconds(10));
    }

    public async Task<bool> LoginSiga(LoginData loginData)
    {
            _driver.Navigate().GoToUrl("https://siga.cps.sp.gov.br/aluno/login.aspx");
            _driver.FindElement(By.Id("vSIS_USUARIOID")).SendKeys(loginData.Login);
            _driver.FindElement(By.Id("vSIS_USUARIOSENHA")).SendKeys(loginData.Password);
            _driver.FindElement(By.Name("BTCONFIRMA")).Click();
            var checkHome = Task.Run(() => _wait.Until(x => x.Url == "https://siga.cps.sp.gov.br/aluno/home.aspx"));
            var checkError = Task.Run(() => _wait.Until(x => x.FindElement(By.XPath("//span[@id='span_vSAIDA']//text")).Text != ""));
            var checkCompleted = await Task.WhenAny(checkHome, checkError);
            return checkCompleted == checkHome;
    }

    public void ScrapStudent(out StudentInfo student, out string shortCourse)
    {
            student = new StudentInfo
            {
                Name = _wait.Until(x=>x.FindElement(By.XPath("//span[.='RA:']/../../../../../../.././/span"))).Text.Replace(" -",""),
                Course = _wait.Until(x=>x.FindElement(By.XPath("//span[.='Faculdade de Tecnologia de Ribeirão Preto']/following-sibling::span"))).Text,
                Period = _wait.Until(x=>x.FindElement(By.XPath("//span[.='Em Curso']/following-sibling::span"))).Text
            };
            
            shortCourse = student.Course switch
            {
                "Tecnologia em Análise e Desenvolvimento de Sistemas" => $"ADS{student.Period.Substring(0,1)}",
                "Tecnologia em Gestão de Negócios e Inovação" => "GNI",
                "Tecnologia em Sistemas Biomédicos" => "SBM",
                _ => student.Course
            };
    }
    public void ScrapLessons(out IEnumerable<string> lessonsTags)
    {
        var lessonsString = _wait.Until(x=>x.
            FindElement(By.XPath("//span[.='Planos de Ensino']/../../../../.././following-sibling::div"))).Text
            .Replace("\r\n", "@");
        _driver.Quit();
        lessonsTags = lessonsString.Split("@").ToList();
        lessonsTags = lessonsTags.Select(l =>
        {
            l = l[..6];
            return l;
        }).ToList();
        
    }
}