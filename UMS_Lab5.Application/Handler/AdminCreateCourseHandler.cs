using MediatR;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using NpgsqlTypes;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class AdminCreateCourseHandler:IRequestHandler<AdminCreateCourseCommand,Course>
{
    private readonly UMSContext _context;

    public AdminCreateCourseHandler(UMSContext context)
    {
        _context = context;
    }

    public Task<Course> Handle(AdminCreateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseDto = request.CourseDto;
        var course = new Course
        {
            Id = IdGenerator.GenerateNewId<Course>(_context),
            Name = courseDto.Name,
            EnrolmentDateRange = new NpgsqlRange<DateOnly>(courseDto.StartDate, courseDto.EndDate),
            MaxStudentsNumber = courseDto.MaxNumberOfStudents

        };
        _context.Courses.Add(course);
        _context.SaveChanges();
        return Task.FromResult(course);
    }
}