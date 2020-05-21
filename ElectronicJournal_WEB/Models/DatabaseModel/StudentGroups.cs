using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class StudentGroups
    {
        public int StudentGroupId { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Users User { get; set; }
    }
}
