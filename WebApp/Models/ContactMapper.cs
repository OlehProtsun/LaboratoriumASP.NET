using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Models;

public class ContactMapper
{
    public static ContactEntity ToEntity(ContactModel arg)
    {
        return new ContactEntity()
        {
            Id = arg.Id,
            FirstName = arg.FirstName,
            LastName = arg.LastName,
            BirthDate = arg.BirthDate,
            Email = arg.Email,
            PhoneNumber = arg.PhoneNumber,
            Category = arg.Category,
            Organization = arg.Organization,
            OrganizationId = arg.OrganizationId
        };
    }
    
    public static ContactModel FromEntity(ContactEntity arg)
    {
        return new ContactModel()
        {
            Id = arg.Id,
            FirstName = arg.FirstName,
            LastName = arg.LastName,
            BirthDate = arg.BirthDate,
            Email = arg.Email,
            PhoneNumber = arg.PhoneNumber,
            Category = arg.Category,
            Organization = arg.Organization,
            OrganizationId = arg.OrganizationId
        };
    }
}