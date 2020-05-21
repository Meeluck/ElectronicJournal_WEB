using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class LessonTypes
    {
        public LessonTypes()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int LessonTypeId { get; set; }
        public string LessonTypeName { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
