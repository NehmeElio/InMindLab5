using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Application;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Application.DTO;

namespace UMS_Lab5.API.Controllers;

public class GradesController : ControllerBase
{
    //private readonly GradeService _gradeService;
    private readonly ISender _sender;

    public GradesController(ISender sender)
    {
        //_gradeService = gradeService;
        _sender = sender;
    }

    /*[HttpPost("SetGrade")]
    public IActionResult SetGrade(int studentId, int courseId, decimal grade)
    {
        if ( studentId <= 0 || courseId <= 0 || grade < 0)
        {
            return BadRequest("Invalid input.");
        }

        _gradeService.SetGrade(studentId, courseId, grade);
        return Ok("Grade set successfully.");
    }*/
    
    [HttpPost("SetGreade")]
    public IActionResult SetGrade([FromBody] SetGradeDto setGradeDto)
    {
        _sender.Send(new SetGradeCommand(setGradeDto));
        return Ok("Grade set successfully.");
    }
}