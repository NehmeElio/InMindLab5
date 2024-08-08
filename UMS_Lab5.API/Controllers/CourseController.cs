using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UMS_Lab5.Persistence;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly MultiTenantContext _dbContext;

    public CoursesController(MultiTenantContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        //var schemaName = GetSchemaNameForBranch(branchId);
        //_dbContext.ChangeSchema(schemaName); // Method to change schema dynamically

        var courses = await _dbContext.Courses.ToListAsync();
        return Ok(courses);
    }

    private string GetSchemaNameForBranch(string branchId)
    {
        // Implement your schema name resolution logic
        return branchId switch
        {
            "1" => "science",
            "2" => "arts",
            _ => throw new Exception("Unknown branch ID")
        };
    }
}