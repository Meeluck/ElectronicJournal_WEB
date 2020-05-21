using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Positions
    {
        public Positions()
        {
            Teachers = new HashSet<Teachers>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }

        public virtual ICollection<Teachers> Teachers { get; set; }
    }
}
