using MediatR;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Application.Notifications;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class SetGradeHandler: IRequestHandler<SetGradeCommand, Grade>
{
    private readonly UMSContext _context;
    private readonly IPublisher _publisher;
    
    public SetGradeHandler(UMSContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }
    
    public Task<Grade> Handle(SetGradeCommand request, CancellationToken cancellationToken)
    {
        var enrollments=_context.ClassEnrollments
            .FirstOrDefault(e=>e.StudentId==request.setGradeDto.StudentId && e.ClassId==request.setGradeDto.Teacherpercourseid);
        if (enrollments == null)
        {
            throw new InvalidOperationException("Student is not enrolled in this course");
        }
        else
        {
            var newGradeId = IdGenerator.GenerateNewId<Grade>(_context);
            var newGrade = new Grade
            {
                GradeId = newGradeId,
                StudentId = request.setGradeDto.StudentId,
                Teacherpercourseid = request.setGradeDto.Teacherpercourseid,
                Grade1 = request.setGradeDto.Grade1
            };
            _context.Grades.Add(newGrade);
            _context.SaveChanges();
            _publisher.Publish(new GradeAddedNotification(newGrade),cancellationToken );
            return Task.FromResult(newGrade);
        }
        
        
    }
}