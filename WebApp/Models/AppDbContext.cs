using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> organizations { get; set; }

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
        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    NIP = "24234123",
                    Name = "WSEI",
                    REGON = "12314124"
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    NIP = "24234123",
                    Name = "Firma",
                    REGON = "124124134"
                }
                );
        
        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Address)
            .HasData(
                new { OrganizationEntityId = 101, Street = "sw. Filipa 17", City = "Krakow" },
                new { OrganizationEntityId = 102, Street = "sw. marka 17", City = "Poznan" }
            );
        
        modelBuilder.Entity<ContactEntity>()
            .Property(c => c.OrganizationId)
            .HasDefaultValue(101);
        
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
                    Category = Category.Friend,
                    OrganizationId = 101
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
                    Category = Category.Business,
                    OrganizationId = 102
                }
            );
    }
}