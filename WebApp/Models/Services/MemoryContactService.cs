namespace WebApp.Models.Services;

public class MemoryContactService : IContactService
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1, new ContactModel()
            {
                Category = Category.Business,
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
                Category = Category.Friend,
                Id = 2,
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
                Category = Category.Family,
                Id = 3,
                FirstName = "Kon",
                LastName = "Firk",
                PhoneNumber = "678 345 194",
                Email = "kinhuru@wsei.edu.pl",
                BirthDate = new DateOnly(1980,02,12)
            }
        }
    };

    private static int _currentId = 3;
    
    public void Add(ContactModel model)
    {
        model.Id = ++_currentId;
        _contacts.Add(model.Id, model);
    }

    public void Update(ContactModel contact)
    {
        if (_contacts.ContainsKey(contact.Id))
        {
            _contacts[contact.Id] = contact;
        }
    }

    public void Delete(int id)
    {
       _contacts.Remove(id);
    }

    public List<ContactModel> GetAll()
    {
        return _contacts.Values.ToList();
    }

    public ContactModel? GetById(int id)
    {
        return _contacts[id];
    }

    public List<OrganizationEntity> GetAllOrganizations()
    {
        throw new NotImplementedException();
    }
}