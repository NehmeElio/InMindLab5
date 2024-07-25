using System;
using System.Collections.Generic;

namespace UMS_Lab5.Persistence.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public long? StudentId { get; set; }

    public long? CourseId { get; set; }

    public decimal? Grade1 { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? Student { get; set; }
}
