using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Application.Commands;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController:ControllerBase
{
    private readonly ISender _sender;
    public StudentController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("EnrollInCourse")]
    public IActionResult EnrollInCourse(int teacherPerCourseId,int studentId,DateOnly currentDate)
    {
        try
        {
            var command = new StudentEnrollInClassCommand(studentId,teacherPerCourseId,currentDate);
            var result = _sender.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
}