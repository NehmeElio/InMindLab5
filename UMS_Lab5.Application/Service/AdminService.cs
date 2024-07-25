using NpgsqlTypes;
using UMS_Lab5.Application.DTO;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application;

public class AdminService: IAdminService
{
    private readonly UMSContext _context;
    
    public AdminService(UMSContext context)
    {
        _context = context;
    }

    public void AdminCreateCourse(CreateCourseDto courseDto)
    {
        var course = new Course
        {
            Id = _context.Courses.Any() ? _context.Courses.Max(c => c.Id) + 1 : 1,
            Name = courseDto.Name,
            EnrolmentDateRange = new NpgsqlRange<DateOnly>(courseDto.StartDate, courseDto.EndDate),
            MaxStudentsNumber = courseDto.MaxNumberOfStudents

        };
        _context.Courses.Add(course);
        _context.SaveChanges();
    }
}