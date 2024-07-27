using MediatR;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Notifications;

public record GradeAddedNotification(Grade grade):INotification;