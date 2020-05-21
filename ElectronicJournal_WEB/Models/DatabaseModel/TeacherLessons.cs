using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class TeacherLessons
    {
        public int TeacherLessonId { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}
