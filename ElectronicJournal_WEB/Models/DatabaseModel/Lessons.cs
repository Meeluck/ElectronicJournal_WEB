using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Lessons
    {
        public Lessons()
        {
            AcademicPerformances = new HashSet<AcademicPerformances>();
            GroupLessons = new HashSet<GroupLessons>();
            TeacherLessons = new HashSet<TeacherLessons>();
        }

        public int LessonId { get; set; }
        public DateTime Date { get; set; }
        public int? TimeScheduleId { get; set; }
        public int SubjectId { get; set; }
        public int? LessonTypeId { get; set; }
        public int? ClassroomId { get; set; }
        public string Notes { get; set; }

        public virtual Classrooms Classroom { get; set; }
        public virtual LessonTypes LessonType { get; set; }
        public virtual Subjects Subject { get; set; }
        public virtual TimeSchedules TimeSchedule { get; set; }
        public virtual ICollection<AcademicPerformances> AcademicPerformances { get; set; }
        public virtual ICollection<GroupLessons> GroupLessons { get; set; }
        public virtual ICollection<TeacherLessons> TeacherLessons { get; set; }
    }
}
