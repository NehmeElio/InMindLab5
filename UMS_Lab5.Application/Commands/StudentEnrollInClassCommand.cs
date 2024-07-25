using MediatR;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Commands;

public record StudentEnrollInClassCommand(int studentId, int teacherPerCourseId,DateOnly currentDate): IRequest<ClassEnrollment>;