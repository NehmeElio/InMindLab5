using MediatR;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class RegisterTimeSlotForCourseHandler:IRequestHandler<RegisterTimeSlotForCourseCommand,TeacherPerCoursePerSessionTime>
{
    private readonly UMSContext _context;

    public RegisterTimeSlotForCourseHandler(UMSContext context)
    {
        _context = context;
    }

    public Task<TeacherPerCoursePerSessionTime> Handle(RegisterTimeSlotForCourseCommand request, CancellationToken cancellationToken)
    {
        var matchingTeacherPerCourse=_context.TeacherPerCourses
            .FirstOrDefault(tpc => tpc.TeacherId==request.teacherId && tpc.CourseId == request.courseId);
        if(matchingTeacherPerCourse == null)
        {
            throw new InvalidOperationException("Teacher is not registered for this course");
        }
        var matchingTeacherTimeSlot=_context.SessionTimes
            .FirstOrDefault(st=>st.TeacherId == request.teacherId && st.Id==request.timeSlotId);
        if (matchingTeacherTimeSlot == null)
        {
            throw new InvalidOperationException("Time slot does not exist for this teacher");
        }
        var existingTeacherPerCoursePerSessionTime = _context.TeacherPerCoursePerSessionTimes
            .FirstOrDefault(tpcpst=>tpcpst.TeacherPerCourseId == matchingTeacherPerCourse.Id && tpcpst.SessionTimeId == request.timeSlotId);
        if (existingTeacherPerCoursePerSessionTime != null)
        {
            throw new InvalidOperationException("Time slot is already assigned to this teacher");
        }
        var teacherPerCoursePerSessionTime = new TeacherPerCoursePerSessionTime
        {
            Id = IdGenerator.GenerateNewId<TeacherPerCoursePerSessionTime>(_context),
            TeacherPerCourseId =matchingTeacherPerCourse.Id,
            SessionTimeId = request.timeSlotId
        };
        _context.TeacherPerCoursePerSessionTimes.Add(teacherPerCoursePerSessionTime);
        _context.SaveChanges();
        return Task.FromResult(teacherPerCoursePerSessionTime);
    }
}