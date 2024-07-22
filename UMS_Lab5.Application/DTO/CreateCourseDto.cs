namespace UMS_Lab5.Application.DTO;

public class CreateCourseDto
{
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int MaxNumberOfStudents { get; set; }
}
