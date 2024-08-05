using System;
using System.Collections.Generic;

namespace StudentAttendanceAPI.Models;

public partial class Classroom
{
    public int Id { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int CampusId { get; set; }

    public virtual Campus Campus { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
