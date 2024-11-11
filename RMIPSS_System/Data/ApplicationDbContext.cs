using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
