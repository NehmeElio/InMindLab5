using NpgsqlTypes;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;

namespace UMS_Lab5.Application;

public class AdminService: IAdminService
{
    private readonly UMSContext _context;
    
    public AdminService(UMSContext context)
    {
        _context = context;
    }

    public void AdminCreateCourse(string name, NpgsqlRange<DateOnly> dateRange, int maxNumberOfStudents)
    {
        var course = new Course
        {
            Id = _context.Courses.Any() ? _context.Courses.Max(c => c.Id) + 1 : 1,
            Name = name,
            EnrolmentDateRange = dateRange,
            MaxStudentsNumber = maxNumberOfStudents,

        };
        _context.Courses.Add(course);
        _context.SaveChanges();
    }
}