using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using UMS_Lab5.Application;
using UMS_Lab5.Application.DTO;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    
  
    [HttpPost("AdminCreateCourse")]
    public IActionResult AdminCreateCourse([FromBody] CreateCourseDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        var dateRange = new NpgsqlRange<DateOnly>(dto.StartDate, dto.EndDate);
        _adminService.AdminCreateCourse(dto.Name, dateRange, dto.MaxNumberOfStudents);
        return Ok("Course created successfully");
    }
}