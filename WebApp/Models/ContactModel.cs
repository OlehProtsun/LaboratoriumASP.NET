using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class ContactModel
{
    [HiddenInput] public int Id { get; set; }

    [Required(ErrorMessage = "Muszisz wpisac imię!")]
    [MaxLength(length: 20, ErrorMessage = "Imię nie może być dłyższe niż 20 znaków!")]
    [MinLength(length: 2, ErrorMessage = "Imię musi meic co najmniej 2 znaki!")]
    [Display(Name = "Imię")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Muszisz wpisac nazwisko!")]
    [MaxLength(length: 50, ErrorMessage = "Nazwisko nie może być dłyższe niż 20 znaków!")]
    [MinLength(length: 2, ErrorMessage = "Nazwisko musi meic co najmniej 2 znaki!")]
    [Display(Name = "Nazwisko", Order = 2)]
    public string LastName { get; set; }

    [EmailAddress]
    [Display(Name = "Andres e-mail")]
    public string Email { get; set; }

    [Phone]
    [RegularExpression("\\d{3} \\d{3} \\d{3}", ErrorMessage = "Wpisz numer wg wzoru:xxx xxx xxx")]
    [Display(Name = "Telefon")]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Data urodzenia")]
    public DateOnly BirthDate { get; set; }
    
    [Display(Name = "Kategoria")]
    public Category Category { get; set; }

    [HiddenInput]
    public int? OrganizationId { get; set; }
    
    [Display(Name = "Organizacja")]
    public OrganizationEntity? Organization { get; set; }
    
    [ValidateNever]
    public List<SelectListItem> Organizations { get; set; }
}