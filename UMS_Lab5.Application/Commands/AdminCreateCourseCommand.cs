using MediatR;
using UMS_Lab5.Application.DTO;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application.Commands;

public record AdminCreateCourseCommand(CreateCourseDto CourseDto) : IRequest<Course>;