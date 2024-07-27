using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Application;

namespace UMS_Lab5.API.Controllers;

public class GradesController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradesController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost("set-grade")]
    public IActionResult SetGrade(int studentId, int courseId, decimal grade)
    {
        if ( studentId <= 0 || courseId <= 0 || grade < 0)
        {
            return BadRequest("Invalid input.");
        }

        _gradeService.SetGrade(studentId, courseId, grade);
        return Ok("Grade set successfully.");
    }
}