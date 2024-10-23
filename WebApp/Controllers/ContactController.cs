using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Id = 1,
                FirstName = "Nola",
                LastName = "Addams",
                PhoneNumber = "345 325 542",
                Email = "nolaadams@wsei.edu.pl",
                BirthDate = new DateOnly(2001,10,10)
            }
        },
        {
            2, new ContactModel()
            {
                Id = 1,
                FirstName = "Dill",
                LastName = "Contik",
                PhoneNumber = "675 353 990",
                Email = "dillcontik@wsei.edu.pl",
                BirthDate = new DateOnly(1998,12,01)
            }
        },
        {
            3, new ContactModel()
            {
                Id = 1,
                FirstName = "Kon",
                LastName = "Firk",
                PhoneNumber = "678 345 194",
                Email = "kinhuru@wsei.edu.pl",
                BirthDate = new DateOnly(1980,02,12)
            }
        }
    };

    private static int _currentId = 3;
    
    
    
    // Lista kontaktów
    public IActionResult Index()
    {
        return View(_contacts);
    }

    // Zwraca formularz dodania kontaktu
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    //Odbiera danych z formularza, zapisa kontaktu i powrot do listy kontaktu
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        model.Id = ++_currentId;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }

    public IActionResult Details(int id)
    {
        return View(_contacts[id]);
    }
}