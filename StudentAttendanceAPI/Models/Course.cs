using System;
using System.Collections.Generic;

namespace StudentAttendanceAPI.Models;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public int Duration { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
