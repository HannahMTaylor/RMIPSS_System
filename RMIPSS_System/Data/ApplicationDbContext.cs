using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

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
    public DbSet<ConsentForm> ConsentForms { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ReferrerPerson> ReferrerPeople { get; set; }
    public DbSet<Referral> Referrals { get; set; }
    public DbSet<SE2> Se2 { get; set; }
}
