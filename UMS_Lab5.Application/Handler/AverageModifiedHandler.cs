using MediatR;
using UMS_Lab5.Application.Notifications;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class AverageModifiedHandler : INotificationHandler<GradeAddedNotification>
{
    private readonly UMSContext _context;

    public AverageModifiedHandler(UMSContext context)
    {
        _context = context;
    }

    public Task Handle(GradeAddedNotification notification, CancellationToken cancellationToken)
    {
        var grades = _context.Grades
            .Where(g => g.StudentId == notification.grade.StudentId)
            .ToList();


        var average = grades.Average(g=>g.Grade1);

        var averageToBeChanged = _context.Averages
                .FirstOrDefault(a=>a.StudentId==notification.grade.StudentId);
        if (averageToBeChanged == null)
        {
            var averageToBeAdded = new Average
            {
                Id = IdGenerator.GenerateNewId<Average>(_context),
                StudentId = notification.grade.StudentId,
                AverageGrade = average,
                CanGoToFrance = average>15
            };
            _context.Averages.Add(averageToBeAdded);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        else
        {
            averageToBeChanged.AverageGrade = average;
            averageToBeChanged.CanGoToFrance = average>15;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    }


}