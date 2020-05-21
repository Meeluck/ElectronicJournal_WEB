using System;
using System.Collections.Generic;

namespace ElectronicJournal_WEB.Models.DatabaseModel
{
    public partial class Users
    {
        public Users()
        {
            AcademicPerformances = new HashSet<AcademicPerformances>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int? AccessLevelId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual AccessLevels AccessLevel { get; set; }
        public virtual StudentGroups StudentGroups { get; set; }
        public virtual Teachers Teachers { get; set; }
        public virtual ICollection<AcademicPerformances> AcademicPerformances { get; set; }
    }
}
