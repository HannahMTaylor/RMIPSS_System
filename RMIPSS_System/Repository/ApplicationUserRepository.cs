﻿using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private ApplicationDbContext _db;

    public ApplicationUserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Update(ApplicationUser applicationUser)
    {
        _db.ApplicationUsers.Update(applicationUser);
    }
}
