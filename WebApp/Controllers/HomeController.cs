using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    
    //Zadanie Domowe
    public IActionResult Age(DateTime? birthDate)
    {
        //.../?birthDate=YYYY-MM-DD
        if (birthDate == null)
        {
            ViewBag.ErrorMessage = "Please select a date";
            return View("AgeError");
        }
        var age = CalculateAge(birthDate);
        ViewBag.AgeYears = age.Value.Years;
        ViewBag.AgeMonths = age.Value.Months;
        ViewBag.AgeDays = age.Value.Days;
        return View("Age");
    }

    private (int Years, int Months, int Days)? CalculateAge(DateTime? birthDate)
    {
        var years = DateTime.Today.Year - birthDate.Value.Year;
        var months = DateTime.Today.Month - birthDate.Value.Month;
        var days = DateTime.Today.Day - birthDate.Value.Day;
        if (days<0)
        {
            months--;
            days += DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        }

        if (months < 0)
        {
            years--;
            days += DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        }
        return (years, months, days);
    }
    
    //Zadanie 1.
    public IActionResult Calculator(Operator? op, double? x, double? y)
    {
        //...?op=add&x=4&y=1,5
        //var op = Request.Query["op"];
        //var x = double.Parse(Request.Query["x"]);
        //var y = double.Parse(Request.Query["y"]);
        if (x is null || y is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format liczby x lub y lub ich brak!";
            return View("CalculatorEror");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "Nieznany operator!";
            return View("CalculatorEror");
        }
        double? result = 0.0;
        switch (op)
        {
            case Operator.Add:
                result = x + y;
                ViewBag.Operator = "+";
                break;
            case Operator.Div:
                result = x / y;
                ViewBag.Operator = "/";
                break;
            case Operator.Sub:
                result = x - y;
                ViewBag.Operator = "-";
                break;
            case Operator.Mul:
                result = x * y;
                ViewBag.Operator = "*";
                break;
        }
        ViewBag.Result = result;
        ViewBag.X = x;
        ViewBag.Y = y;
        return View();
    }
    
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    Add, Sub, Mul, Div 
}

// Zdanie domowe: napisz metode Age, ktora przyjmuje paratre z data urodzin i wyswietla wiek w latach miesiacach i dniach. I send link do repoztorium d E.Wsei
