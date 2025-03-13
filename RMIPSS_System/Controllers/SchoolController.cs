using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;
/// <summary>
/// Using primary constructors for classes, which simplify constructor declaration by allowing
/// you to declare parameters directly in the class declaration.
/// </summary>
/// <param name="db"></param>
[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class SchoolController(ISchoolRepository db) : Controller
{
    /*
    The SchoolController is just an example class to demonstrate CRRUD Operation
    */

    public IActionResult Index()
    {
        // Create
        School school = new School
        {
            Name = "ETSU",
            Address = "Johnson City",
            Phone = "123456789"
        };

        db.Add(school);
        db.SaveAsync();

        // Get All
       // List<School> objCategoryList = db.GetAll().ToList();

        // Update
        school.Phone = "987654321";
        db.Update(school);
        db.SaveAsync();

        // Get
        School? schoolFromDb = db.Get(u => u != null && u.Name == "ETSU");

        // Delete
        db.Remove(schoolFromDb);
        db.SaveAsync();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Create()
    {
        School school = new School
        {
            Name = "ETSU",
            Address = "Johnson City",
            Phone = "123456789"
        };

        await db.AddAsync(school);
        await db.SaveAsync();

        return Content($"School Created !!! [school.Name = {school.Name}, school.Address = {school.Address}, school.Phone = {school.Phone}]");
    }

    public Task<IActionResult> ReadAll()
    {
      //  var objCategoryList = await db.GetAllAsync();
        return Task.FromResult<IActionResult>(Content($"All Schools Extracted!!!"));
    }

    public async Task<IActionResult> Read()
    {
        School? schoolFromDb = await db.GetAsync(u => u != null && u.Name == "ETSU");
        return Content($"School Extracted !!! [school.Name = {schoolFromDb?.Name}, school.Address = {schoolFromDb?.Address}, school.Phone = {schoolFromDb?.Phone}]");
    }

    public async Task<IActionResult?> Update()
    {
        School? school = await db.GetAsync(u => u != null && u.Name == "ETSU");
        if (school != null)
        {
            school.Phone = "987654321";
            db.Update(school);
            await db.SaveAsync();

            return Content(
                $"School Updated !!! [school.Name = {school.Name}, school.Address = {school.Address}, school.Phone = {school.Phone}]");
        }

        return null;
    }

    public async Task<IActionResult> Delete()
    {
        School? school = await db.GetAsync(u => u != null && u.Name == "ETSU");
        db.Remove(school);
        await db.SaveAsync();

        return Content($"School Deleted !!! [school.Name = {school?.Name}, school.Address = {school?.Address}, school.Phone = {school?.Phone}]");
    }
}
