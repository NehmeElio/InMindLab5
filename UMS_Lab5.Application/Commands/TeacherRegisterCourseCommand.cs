using MediatR;
using UMS_Lab5.Persistence.UMS_Lab5.Domain.Models;

namespace UMS_Lab5.Application.Commands;

public record TeacherRegisterCourseCommand(int TeacherId, int CourseId) : IRequest<TeacherPerCourse>;