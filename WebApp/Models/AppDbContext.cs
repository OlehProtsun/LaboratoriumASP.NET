using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "Poul",
                    LastName = "Dominik",
                    Email = "Dominik@gmail.com",
                    PhoneNumber = "123123123",
                    BirthDate = new DateOnly(2000,10,10),
                    Created = DateTime.Now,
                    Category = Category.Friend
                },
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "Adam",
                    LastName = "Pob",
                    Email = "Pob@gmail.com",
                    PhoneNumber = "234234234",
                    BirthDate = new DateOnly(2002,11,2),
                    Created = DateTime.Now,
                    Category = Category.Business
                }
            );
    }
}