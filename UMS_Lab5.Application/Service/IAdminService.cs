using NpgsqlTypes;
using UMS_Lab5.Application.DTO;

namespace UMS_Lab5.Application;

public interface IAdminService
{
    void  AdminCreateCourse(CreateCourseDto courseDto);
    
}