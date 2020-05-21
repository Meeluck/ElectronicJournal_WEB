using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class GroupLessons
    {
        public int GroupLessonId { get; set; }
        public int LessonId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Lessons Lesson { get; set; }
    }
}
