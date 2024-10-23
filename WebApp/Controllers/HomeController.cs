using System.ComponentModel.DataAnnotations;
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ViewResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            //kod wykonywany gdy dane są poprawne
            return View();
        }
        else
        {
            return View(); // ponowne wyświetlenie formularza z informacjami o błędach
        }
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
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

// Lab 2 zadanie

public class Product
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Proszę podać nazwe!")]
    public string Nazwa { get; set; }
    
    [Required(ErrorMessage = "Proszę podać cenę!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od zera.")]
    public decimal Cena { get; set; }
    
    [Required(ErrorMessage = "Proszę podać producenta!")]
    public string Producent { get; set; }

    [Required(ErrorMessage = "Proszę podać datę produkcji!")]
    [DataType(DataType.Date)]
    public DateTime DataProdukcji { get; set; }
    
    public string Opis { get; set; }
}

