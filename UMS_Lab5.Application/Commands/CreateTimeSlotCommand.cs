using MediatR;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application.Commands;

public record CreateTimeSlotCommand(int TeacherId, DateTime StartTime, DateTime EndTime):IRequest<SessionTime>;