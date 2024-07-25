using MediatR;
using UMS_Lab5.Application.Commands;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application.Handler;

public class CreateTimeSlotHandler:IRequestHandler<CreateTimeSlotCommand,SessionTime>
{
    private readonly UMSContext _context;

    public CreateTimeSlotHandler(UMSContext context)
    {
        _context = context;
    }

    public Task<SessionTime> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
    {
        var duration = request.EndTime - request.StartTime;
        var sessionTime=new SessionTime()
        {
            Id = IdGenerator.GenerateNewId<SessionTime>(_context),
            TeacherId = request.TeacherId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Duration = (int)duration.TotalMinutes
        };
        _context.SessionTimes.Add(sessionTime);
        _context.SaveChanges();
        return Task.FromResult(sessionTime);
    }
            
        
    
}