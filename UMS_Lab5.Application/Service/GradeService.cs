using UMS_Lab5.Application.Observers;
using UMS_Lab5.Common.Helpers;
using UMS_Lab5.Persistence.Models;

namespace UMS_Lab5.Application;

public class GradeService:IGradeService
{
    private readonly UMSContext _dbContext;
    private readonly StudentAverageObserver _studentAverageObserver;

    public GradeService(UMSContext dbContext)
    {
        _dbContext = dbContext;
        _studentAverageObserver = new StudentAverageObserver(dbContext); // Injecting dbContext
    }

    public void SetGrade(long studentId, long courseId, decimal grade)
    {
        var gradeEntity = new Grade
        {
            GradeId = IdGenerator.GenerateNewId<Grade>(_dbContext),
            StudentId = studentId,
            Teacherpercourseid = courseId,
            Grade1 = grade
        };

        gradeEntity.RegisterObserver(_studentAverageObserver);
        gradeEntity.SetGrade(grade);

        _dbContext.Grades.Add(gradeEntity);
        _dbContext.SaveChanges();
    }
}