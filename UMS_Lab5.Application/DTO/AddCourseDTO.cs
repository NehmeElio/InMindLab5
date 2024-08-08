using NpgsqlTypes;

namespace UMS_Lab5.Application.DTO;

public class AddCourseDTO
{
    public string? Name { get; set; }

    public int? MaxStudentsNumber { get; set; }

    public NpgsqlRange<DateOnly>? EnrolmentDateRange { get; set; }
    
}