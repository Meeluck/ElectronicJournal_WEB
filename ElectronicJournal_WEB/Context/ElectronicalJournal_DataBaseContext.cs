using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ElectronicJournal_WEB.Models.DatabaseModel;

namespace ElectronicJournal_WEB.Context
{
    public partial class ElectronicalJournalContext : DbContext
    {
        public ElectronicalJournalContext()
        {
        }

        public ElectronicalJournalContext(DbContextOptions<ElectronicalJournalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicPerformances> AcademicPerformances { get; set; }
        public virtual DbSet<AccessLevels> AccessLevels { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<GroupLessons> GroupLessons { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<LessonTypes> LessonTypes { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<StudentGroups> StudentGroups { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<TeacherLessons> TeacherLessons { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<TimeSchedules> TimeSchedules { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ElectronicalJournal_DataBase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicPerformances>(entity =>
            {
                entity.HasKey(e => e.AcademicPerformanceId);

                entity.HasIndex(e => e.LessonId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.AcademicPerformances)
                    .HasForeignKey(d => d.LessonId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AcademicPerformances)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AccessLevels>(entity =>
            {
                entity.HasKey(e => e.AccessLevelId);
            });

            modelBuilder.Entity<Buildings>(entity =>
            {
                entity.HasKey(e => e.BuildingId);

                entity.HasIndex(e => e.BuildingName)
                    .HasName("AK_Buildings_BuildingName")
                    .IsUnique();

                entity.Property(e => e.BuildingName).IsRequired();
            });

            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.HasKey(e => e.ClassroomId);

                entity.HasIndex(e => e.BuildingId);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<GroupLessons>(entity =>
            {
                entity.HasKey(e => e.GroupLessonId);

                entity.HasIndex(e => e.GroupId);

                entity.HasIndex(e => e.LessonId);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupLessons)
                    .HasForeignKey(d => d.GroupId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.GroupLessons)
                    .HasForeignKey(d => d.LessonId);
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.HasIndex(e => e.GroupName)
                    .HasName("AK_Groups_GroupName")
                    .IsUnique();

                entity.Property(e => e.GroupName).IsRequired();
            });

            modelBuilder.Entity<LessonTypes>(entity =>
            {
                entity.HasKey(e => e.LessonTypeId);

                entity.HasIndex(e => e.LessonTypeName)
                    .HasName("AK_LessonTypes_LessonTypeName")
                    .IsUnique();

                entity.Property(e => e.LessonTypeName).IsRequired();
            });

            modelBuilder.Entity<Lessons>(entity =>
            {
                entity.HasKey(e => e.LessonId);

                entity.HasIndex(e => e.ClassroomId);

                entity.HasIndex(e => e.LessonTypeId);

                entity.HasIndex(e => e.SubjectId);

                entity.HasIndex(e => e.TimeScheduleId);

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.LessonType)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.LessonTypeId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SubjectId);

                entity.HasOne(d => d.TimeSchedule)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.TimeScheduleId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.HasIndex(e => e.PositionName)
                    .HasName("AK_Positions_PositionName")
                    .IsUnique();

                entity.Property(e => e.PositionName).IsRequired();
            });

            modelBuilder.Entity<StudentGroups>(entity =>
            {
                entity.HasKey(e => e.StudentGroupId);

                entity.HasIndex(e => e.GroupId);

                entity.HasIndex(e => e.UserId)
                    .HasName("AK_StudentGroups_UserId")
                    .IsUnique();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.StudentGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.StudentGroups)
                    .HasForeignKey<StudentGroups>(d => d.UserId);
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectId);

                entity.HasIndex(e => e.SubjectName)
                    .HasName("AK_Subjects_SubjectName")
                    .IsUnique();

                entity.Property(e => e.SubjectName).IsRequired();
            });

            modelBuilder.Entity<TeacherLessons>(entity =>
            {
                entity.HasKey(e => e.TeacherLessonId);

                entity.HasIndex(e => e.LessonId);

                entity.HasIndex(e => e.TeacherId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.TeacherLessons)
                    .HasForeignKey(d => d.LessonId);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherLessons)
                    .HasForeignKey(d => d.TeacherId);
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.HasIndex(e => e.PositionId);

                entity.HasIndex(e => e.UserId)
                    .HasName("AK_Teachers_UserId")
                    .IsUnique();

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Teachers)
                    .HasForeignKey<Teachers>(d => d.UserId);
            });

            modelBuilder.Entity<TimeSchedules>(entity =>
            {
                entity.HasKey(e => e.TimeScheduleId);

                entity.HasIndex(e => e.TimeInterval)
                    .HasName("AK_TimeSchedules_TimeInterval")
                    .IsUnique();

                entity.Property(e => e.TimeInterval).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.AccessLevelId);

                entity.HasIndex(e => e.Login)
                    .HasName("AK_Users_Login")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.HasOne(d => d.AccessLevel)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccessLevelId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
