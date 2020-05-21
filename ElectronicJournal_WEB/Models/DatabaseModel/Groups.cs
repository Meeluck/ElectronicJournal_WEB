using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Groups
    {
        public Groups()
        {
            GroupLessons = new HashSet<GroupLessons>();
            StudentGroups = new HashSet<StudentGroups>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime YearFormationGroup { get; set; }

        public virtual ICollection<GroupLessons> GroupLessons { get; set; }
        public virtual ICollection<StudentGroups> StudentGroups { get; set; }
    }
}
