using NpgsqlTypes;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application;

public class CourseService:ICourseService
{
    private readonly UMSContext _context;

    public CourseService(UMSContext context)
    {
        _context=context;
    }
    public IEnumerable<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }

    public long GetMaxCourseId()
    {
        return _context.Courses.Any()?_context.Courses.Max(x => x.Id):0;
    }

    public Course AddCourse(string name, NpgsqlRange<DateOnly> dateRange, int maxNumberOfStudents)
    {
        var id=GetMaxCourseId()+1;
        Course course = new Course
        {
            Id = id,
            Name = name,
            EnrolmentDateRange = dateRange,
            MaxStudentsNumber = maxNumberOfStudents
        };
        _context.Courses.Add(course);
        return course;
    }

    public Course GetCourseById(int id)
    {
        return _context.Courses.FirstOrDefault(x => x.Id == id);
    }

    public Course UpdateCourse(Course course)
    {
        _context.Update(course);
        return course;
    }

    public void DeleteCourse(int id)
    {
        var course=GetCourseById(id);
        _context.Courses.Remove(course);
    }
}