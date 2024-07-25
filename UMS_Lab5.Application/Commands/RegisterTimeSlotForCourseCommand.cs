using MediatR;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Commands;

public record RegisterTimeSlotForCourseCommand(int courseId,int teacherId, int timeSlotId) : IRequest<TeacherPerCoursePerSessionTime>;