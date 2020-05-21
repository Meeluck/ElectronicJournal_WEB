using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Buildings
    {
        public Buildings()
        {
            Classrooms = new HashSet<Classrooms>();
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; }

        public virtual ICollection<Classrooms> Classrooms { get; set; }
    }
}
