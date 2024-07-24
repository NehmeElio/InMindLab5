using MediatR;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using UMS_Lab5.Application;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Application.DTO;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ISender _sender;
    public AdminController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("AdminCreateCourse")]
    public IActionResult AdminCreateCourse([FromBody] CreateCourseDto dto)
    {
        var command = new AdminCreateCourseCommand(dto);
        var result = _sender.Send(command);
        return Ok(result);
    }
}