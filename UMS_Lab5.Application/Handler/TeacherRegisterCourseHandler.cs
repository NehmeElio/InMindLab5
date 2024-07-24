using MediatR;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;

namespace UMS_Lab5.Application.Handler;

public class TeacherRegisterCourseHandler : IRequestHandler<TeacherRegisterCourseCommand, TeacherPerCourse>
{
    private readonly UMSContext _context;

    public TeacherRegisterCourseHandler(UMSContext context)
    {
        _context = context;
    }

    public Task<TeacherPerCourse> Handle(TeacherRegisterCourseCommand request, CancellationToken cancellationToken)
    {
        var coursesId = _context.TeacherPerCourses
            .FirstOrDefault(tpc => tpc.CourseId == request.CourseId);
        if (coursesId != null)
        {
            throw new InvalidOperationException("Course already registered");
        }
        var teacherPerCourse = new TeacherPerCourse
        {
            Id = IdGenerator.GenerateNewId<TeacherPerCourse>(_context),
            CourseId = request.CourseId,
            TeacherId = request.TeacherId
        };

        _context.TeacherPerCourses.Add(teacherPerCourse);
        _context.SaveChanges();
        return Task.FromResult(teacherPerCourse);
    }
}