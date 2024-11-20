using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace WebApp.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
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
        base.OnModelCreating(modelBuilder);
        var ADMIN_ID = Guid.NewGuid().ToString();
        var ADMIN_ROLE_ID = Guid.NewGuid().ToString();
        var USER_ID = Guid.NewGuid().ToString();
        var USER_ROLE_ID = Guid.NewGuid().ToString();

        
        modelBuilder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole()
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "admin",
                    NormalizedName = "admin".ToUpper(),
                    ConcurrencyStamp = ADMIN_ROLE_ID
                },
                new IdentityRole()
                {
                    Id = USER_ROLE_ID,
                    Name = "user",
                    NormalizedName = "user".ToUpper(),
                    ConcurrencyStamp = USER_ROLE_ID
                }
            );
        var admin = new IdentityUser()
        {
            Id = ADMIN_ID,
            UserName = "oleh",
            NormalizedUserName = "oleh".ToUpper(),
            Email = "wsei@wsei.edu.pl",
            NormalizedEmail = "wsei@wsei.edu.pl".ToUpper(),
            EmailConfirmed = true,
            
        };
        var user = new IdentityUser()
        {
            Id = USER_ID,
            UserName = "olehuser",
            NormalizedUserName = "olehuser".ToUpper(),
            Email = "wseiuser@wsei.edu.pl",
            NormalizedEmail = "wseiuser@wsei.edu.pl".ToUpper(),
            EmailConfirmed = true,
            
        };
        
        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "123!");
        user.PasswordHash = hasher.HashPassword(user, "123456!");
        modelBuilder.Entity<IdentityUser>()
            .HasData(admin,user);
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = USER_ID
                },
                new IdentityUserRole<string>()
                {
                    RoleId = USER_ROLE_ID,
                    UserId = ADMIN_ID
                }
            );
        
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