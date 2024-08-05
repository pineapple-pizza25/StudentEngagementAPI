using System;
using System.Collections.Generic;

namespace StudentAttendanceAPI.Models;

public partial class StudentModule
{
    public int Id { get; set; }

    public string StudentId { get; set; } = null!;

    public string SubjectCode { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject SubjectCodeNavigation { get; set; } = null!;
}
