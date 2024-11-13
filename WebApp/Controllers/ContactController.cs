using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Models.Services;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }


    // Lista kontaktów
    public IActionResult Index()
    {
        return View(_contactService.GetAll());
    }

    // Zwraca formularz dodania kontaktu
    [HttpGet]
    public IActionResult Add()
    {
        var model = new ContactModel();
        model.Organizations = _contactService.GetAllOrganizations()
            .Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name,
                Selected = e.Id == 102
            }).ToList();
        return View(model);
    }

    //Odbiera danych z formularza, zapisa kontaktu i powrot do listy kontaktu
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        
        if (!ModelState.IsValid)
        {
            
            model.Organizations = _contactService.GetAllOrganizations()
                .Select(e => new SelectListItem()
                {
                    Value = e.Id.ToString(),
                    Text = e.Name,
                    Selected = e.Id == 102
                }).ToList();
            return View(model);
        }
        _contactService.Add(model);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        return View(_contactService.GetById(id));
    }

    public ActionResult Edit(int id)
    {
        var model = _contactService.GetById(id);
        model.Organizations = _contactService.GetAllOrganizations()
            .Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name,
                Selected = e.Id == model.OrganizationId  // вибираємо організацію, яку зараз має контакт
            }).ToList();
        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Organizations = _contactService.GetAllOrganizations()
                .Select(e => new SelectListItem()
                {
                    Value = e.Id.ToString(),
                    Text = e.Name,
                    Selected = e.Id == model.OrganizationId  // вибираємо організацію, яку зараз має контакт
                }).ToList();
            return View(model);
            
        }
        _contactService.Update(model);
        return RedirectToAction(nameof(Index));
    }
    
    
}