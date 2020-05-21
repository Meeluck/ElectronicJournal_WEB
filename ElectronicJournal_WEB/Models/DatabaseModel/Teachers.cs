using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Teachers
    {
        public Teachers()
        {
            TeacherLessons = new HashSet<TeacherLessons>();
        }

        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public int? PositionId { get; set; }

        public virtual Positions Position { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<TeacherLessons> TeacherLessons { get; set; }
    }
}
