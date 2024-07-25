using MediatR;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class StudentEnrollInClassHandler:IRequestHandler<StudentEnrollInClassCommand,ClassEnrollment>
{
    private readonly UMSContext _context;

    public StudentEnrollInClassHandler(UMSContext context)
    {
        _context = context;
    }

    public Task<ClassEnrollment> Handle(StudentEnrollInClassCommand request, CancellationToken cancellationToken)
    {
        var currentDate = request.currentDate;
        var existingClassEnrollment = _context.ClassEnrollments
            .FirstOrDefault(e => e.StudentId == request.studentId && e.ClassId == request.teacherPerCourseId);
        if (existingClassEnrollment != null)
        {
            throw new InvalidOperationException("Student is already enrolled in this course.");
        }
        var existingTeacherPerCourse = _context.TeacherPerCourses
            .FirstOrDefault(tpc => tpc.Id == request.teacherPerCourseId);
        if (existingTeacherPerCourse == null)
        {
            throw new InvalidOperationException("Teacher is not registered for this course.");
        }
        var courseId=existingTeacherPerCourse.CourseId;
        var course = _context.Courses.Find(courseId);
        var dateRange = course.EnrolmentDateRange;

        if (currentDate < dateRange.Value.LowerBound|| currentDate > dateRange.Value.UpperBound)
        {
            throw new InvalidOperationException("Course is not open for enrollment.");
        }
        else
        {
            var classEnrollment = new ClassEnrollment
            {
                Id=IdGenerator.GenerateNewId<ClassEnrollment>(_context), 
                ClassId= request.teacherPerCourseId,
                StudentId = request.studentId
            };
            _context.ClassEnrollments.Add(classEnrollment);
            _context.SaveChanges();
            return Task.FromResult(classEnrollment);
        }
        
    }
}