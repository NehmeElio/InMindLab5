namespace UMS_Lab5.Application;

public interface IGradeService
{
    public void SetGrade(long studentId, long courseId, decimal grade);
}