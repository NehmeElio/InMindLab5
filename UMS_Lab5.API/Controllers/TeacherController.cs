using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Application;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Application.DTO;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController:ControllerBase
{
    private readonly ISender _sender;

    public TeacherController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("TeacherRegisterCourse")]
    public IActionResult TeacherRegisterCourse([FromQuery]int teacherId,[FromQuery] int courseId)
    {
        try
        {
            var command = new TeacherRegisterCourseCommand(teacherId, courseId);
            var result = _sender.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [HttpPost("CreateTimeSlot")]
    public IActionResult CreateTimeSlot(DateTime startTime, DateTime endTime)
    {
        try
        {
            var command = new CreateTimeSlotCommand(startTime,endTime);
            var result = _sender.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}