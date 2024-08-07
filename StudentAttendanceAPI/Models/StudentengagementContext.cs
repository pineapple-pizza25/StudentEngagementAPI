using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentAttendanceAPI.Models;

public partial class StudentengagementContext : DbContext
{
    public StudentengagementContext()
    {
    }

    public StudentengagementContext(DbContextOptions<StudentengagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=studentengagement;Username=postgres;Password=M@zdarx7fd;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdministratorId).HasName("administrator_pkey");

            entity.ToTable("administrator");

            entity.Property(e => e.AdministratorId)
                .HasMaxLength(50)
                .HasColumnName("administrator_id");
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Campus).WithMany(p => p.Administrators)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("administrator_campus_id_fkey");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .HasColumnName("student_id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attendance_lesson_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attendance_student_id_fkey");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("campus_pkey");

            entity.ToTable("campus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.CampusName)
                .HasMaxLength(100)
                .HasColumnName("campus_name");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classroom_pkey");

            entity.ToTable("classroom");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(10)
                .HasColumnName("room_number");

            entity.HasOne(d => d.Campus).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classroom_campus_id_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.Deprecated).HasColumnName("deprecated");
            entity.Property(e => e.Duration).HasColumnName("duration");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.LecturerId).HasName("lecturer_pkey");

            entity.ToTable("lecturer");

            entity.Property(e => e.LecturerId)
                .HasMaxLength(50)
                .HasColumnName("lecturer_id");
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Campus).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lecturer_campus_id_fkey");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lesson_pkey");

            entity.ToTable("lesson");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClassroomId).HasColumnName("classroom_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.LecturerId)
                .HasMaxLength(50)
                .HasColumnName("lecturer_id");
            entity.Property(e => e.LessonDate).HasColumnName("lesson_date");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(8)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lesson_classroom_id_fkey");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LecturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lesson_lecturer_id_fkey");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.SubjectCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lesson_subject_code_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .HasColumnName("student_id");
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Campus).WithMany(p => p.Students)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_campus_id_fkey");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_course_pkey");

            entity.ToTable("student_course");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(8)
                .HasColumnName("module_code");
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .HasColumnName("student_id");

            entity.HasOne(d => d.ModuleCodeNavigation).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.ModuleCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_course_module_code_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_course_student_id_fkey");
        });

        modelBuilder.Entity<StudentSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_subject_pkey");

            entity.ToTable("student_subject");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .HasColumnName("student_id");
            entity.Property(e => e.SubjectCode)
                .HasMaxLength(8)
                .HasColumnName("subject_code");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_subject_student_id_fkey");

            entity.HasOne(d => d.SubjectCodeNavigation).WithMany(p => p.StudentSubjects)
                .HasForeignKey(d => d.SubjectCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_subject_subject_code_fkey");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectCode).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.SubjectCode)
                .HasMaxLength(8)
                .HasColumnName("subject_code");
            entity.Property(e => e.CourseId)
                .HasMaxLength(50)
                .HasColumnName("course_id");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.Deprecated).HasColumnName("deprecated");
            entity.Property(e => e.NqfLevel).HasColumnName("nqf_level");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.Course).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subject_course_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
