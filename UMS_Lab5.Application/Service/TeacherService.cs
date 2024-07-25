using UMS_Lab5.Application.DTO;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application;

public class TeacherService:ITeacherService
{
    private readonly UMSContext _context;

    public TeacherService(UMSContext context)
    {
        _context = context;
    }

    public void RegisterTeacher(TeacherDto teacherDto)
    {
        User teacher = new User()
        {
            Id=_context.Users.Any() ? _context.Users.Max(x => x.Id) + 1 : 1,
            Name = teacherDto.Name,
            RoleId=1,
            FirebaseId = teacherDto.FirebaseId,
            Email = teacherDto.Email

        };
        _context.Users.Add(teacher);
        _context.SaveChanges();

    }
}