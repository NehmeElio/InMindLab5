using System.Text.Json;
using EnrollmentService.Application.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UMS_Lab5.Application;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Application.DTO;
using UMS_Lab5.Application.Service;

namespace UMS_Lab5.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController:ControllerBase
{
    private readonly ISender _sender;
    private readonly IRabbitMQService _rabbitMQService;
    private readonly ICourseService _courseService;
    private readonly ILogger<StudentController> _logger;
    public StudentController(ISender sender,IRabbitMQService rabbitMQService,ICourseService courseService,ILogger<StudentController> logger)
    {
        _sender = sender;
        _rabbitMQService = rabbitMQService;
        _courseService = courseService;
        _logger = logger;
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
    
    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO course)
    {
        
        // Convert the course request to a message format, e.g., JSON
        var message = JsonSerializer.Serialize(course);

        // Publish the message to RabbitMQ
        _rabbitMQService.PublishMessage(message);

        return Ok("Course addition request sent.");
    }
}