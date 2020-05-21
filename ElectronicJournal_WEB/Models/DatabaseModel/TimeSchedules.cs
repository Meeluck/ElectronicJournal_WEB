using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class TimeSchedules
    {
        public TimeSchedules()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int TimeScheduleId { get; set; }
        public string TimeInterval { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
