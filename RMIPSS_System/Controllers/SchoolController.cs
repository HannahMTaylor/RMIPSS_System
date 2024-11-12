using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class SchoolController : Controller
{
    /*
    The SchoolController is just an example class to demonstrate CRRUD Operation
    */

    private readonly ISchoolRepository _schoolRepo;

    public SchoolController(ISchoolRepository db)
    {
        _schoolRepo = db;
    }

    public IActionResult Index()
    {
        // Create
        School school = new School();
        school.Name = "ETSU";
        school.Address = "Johnson City";
        school.Phone = "123456789";

        _schoolRepo.Add(school);
        _schoolRepo.Save();

        // Get All
        List<School> objCategoryList = _schoolRepo.GetAll().ToList();

        // Update
        school.Phone = "987654321";
        _schoolRepo.Update(school);
        _schoolRepo.Save();

        // Get
        School? schoolFromDb = _schoolRepo.Get(u => u.Name == "ETSU");

        // Delete
        _schoolRepo.Remove(schoolFromDb);
        _schoolRepo.Save();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Create()
    {
        School school = new School();
        school.Name = "ETSU";
        school.Address = "Johnson City";
        school.Phone = "123456789";

        await _schoolRepo.AddAsync(school);
        await _schoolRepo.SaveAsync();

        return Content($"School Created !!! [school.Name = {school.Name}, school.Address = {school.Address}, school.Phone = {school.Phone}]");
    }

    public async Task<IActionResult> ReadAll()
    {
        var objCategoryList = await _schoolRepo.GetAllAsync();
        return Content($"All Schools Extracted!!!");
    }

    public async Task<IActionResult> Read()
    {
        School? schoolFromDb = await _schoolRepo.GetAsync(u => u.Name == "ETSU");
        return Content($"School Extracted !!! [school.Name = {schoolFromDb.Name}, school.Address = {schoolFromDb.Address}, school.Phone = {schoolFromDb.Phone}]");
    }

    public async Task<IActionResult> Update()
    {
        School? school = await _schoolRepo.GetAsync(u => u.Name == "ETSU");
        school.Phone = "987654321";
        _schoolRepo.Update(school);
        await _schoolRepo.SaveAsync();

        return Content($"School Updated !!! [school.Name = {school.Name}, school.Address = {school.Address}, school.Phone = {school.Phone}]");
    }

    public async Task<IActionResult> Delete()
    {
        School? school = await _schoolRepo.GetAsync(u => u.Name == "ETSU");
        _schoolRepo.Remove(school);
        await _schoolRepo.SaveAsync();

        return Content($"School Deleted !!! [school.Name = {school.Name}, school.Address = {school.Address}, school.Phone = {school.Phone}]");
    }
}
