using MediatR;
using UMS_Lab5.Application.Notifications;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Handler;

public class GoToFranceHandler:INotificationHandler<GradeAddedNotification>
{
    private readonly UMSContext _context;

    public GoToFranceHandler(UMSContext context)
    {
        _context = context;
    }

    public Task Handle(GradeAddedNotification notification, CancellationToken cancellationToken)
    {
        /*var average = _context.Averages
            .FirstOrDefault(a => a.StudentId == notification.grade.StudentId);
        average.CanGoToFrance = average.AverageGrade > 15;
        _context.SaveChanges();
        return Task.CompletedTask;*/
        Console.WriteLine("Can go to France");
        return Task.CompletedTask;
    }
}