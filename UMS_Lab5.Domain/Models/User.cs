using System;
using System.Collections.Generic;

namespace UMS_Lab5.Persistence.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long RoleId { get; set; }

    public string FirebaseId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SessionTime> SessionTimes { get; set; } = new List<SessionTime>();

    public virtual ICollection<TeacherPerCourse> TeacherPerCourses { get; set; } = new List<TeacherPerCourse>();
}
