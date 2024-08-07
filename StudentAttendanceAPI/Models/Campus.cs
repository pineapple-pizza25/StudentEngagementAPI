using System;
using System.Collections.Generic;

namespace StudentAttendanceAPI.Models;

public partial class Campus
{
    public int Id { get; set; }

    public string CampusName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
