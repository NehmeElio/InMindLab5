using NpgsqlTypes;

namespace UMS_Lab5.Application;

public interface IAdminService
{
    void  AdminCreateCourse(String name,NpgsqlRange<DateOnly> dateRange, int maxNumberOfStudents);
    
}