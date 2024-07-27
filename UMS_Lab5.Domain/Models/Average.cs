using System;
using System.Collections.Generic;

namespace UMS_Lab5.Persistence.Models;

public partial class Average
{
    public int Id { get; set; }

    public long? StudentId { get; set; }

    public decimal? AverageGrade { get; set; }

    public bool? CanGoToFrance { get; set; }

    public virtual User? Student { get; set; }
}
