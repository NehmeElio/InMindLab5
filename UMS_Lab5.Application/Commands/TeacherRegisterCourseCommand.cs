using MediatR;
using UMS_Lab5.Persistence.Models;


namespace UMS_Lab5.Application.Commands;

public record TeacherRegisterCourseCommand(int TeacherId, int CourseId) : IRequest<TeacherPerCourse>;