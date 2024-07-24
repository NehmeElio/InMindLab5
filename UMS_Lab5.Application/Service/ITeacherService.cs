using UMS_Lab5.Application.DTO;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;

namespace UMS_Lab5.Application;

public interface ITeacherService
{
    public void RegisterTeacher(TeacherDto teacher);
    //public void CreateTimeSlots();
}