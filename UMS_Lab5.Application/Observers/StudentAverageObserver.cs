using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Domain.Interfaces;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application.Observers;

public class StudentAverageObserver : IObserver
{
    private readonly UMSContext _dbContext;

    public StudentAverageObserver(UMSContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Grade grade)
    {
        if (grade.StudentId.HasValue)
        {
            var studentId = grade.StudentId.Value;
            var studentGrades = _dbContext.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => g.Grade1 ?? 0)
                .ToList();

            if (studentGrades.Any())
            {
                var averageGrade = studentGrades.Average();

                var studentAverage = _dbContext.Averages.SingleOrDefault(a => a.StudentId == studentId);

                if (studentAverage == null)
                {
                    studentAverage = new Average
                    {
                        Id = IdGenerator.GenerateNewId<Average>(_dbContext),
                        StudentId = studentId,
                        AverageGrade = averageGrade,
                        CanGoToFrance = averageGrade > 15
                    };
                    _dbContext.Averages.Add(studentAverage);
                }
                else
                {
                    studentAverage.AverageGrade = averageGrade;
                    studentAverage.CanGoToFrance = averageGrade > 15;
                }

                _dbContext.SaveChanges();
            }
        }
    }
}