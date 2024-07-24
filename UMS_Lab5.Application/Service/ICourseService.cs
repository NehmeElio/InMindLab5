using NpgsqlTypes;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;

namespace UMS_Lab5.Application;

public interface ICourseService
{
    IEnumerable<Course> GetAllCourses();
    
   
    Course GetCourseById(int id);
    long GetMaxCourseId();
    Course AddCourse(string name, NpgsqlRange<DateOnly> dateRange, int maxNumberOfStudents);
    Course UpdateCourse(Course course);
    void DeleteCourse(int id);
}