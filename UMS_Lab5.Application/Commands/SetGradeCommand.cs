using System.Windows.Input;
using MediatR;
using UMS_Lab5.Application.DTO;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Commands;

public record SetGradeCommand(SetGradeDto setGradeDto):IRequest<Grade>;