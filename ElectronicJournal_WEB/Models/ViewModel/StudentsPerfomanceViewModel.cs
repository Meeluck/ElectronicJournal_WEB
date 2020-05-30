using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal_WEB.Models.ViewModel
{
	public class StudentsPerfomanceViewModel
	{
		public int UserId { get; set; }
		public int AcademicPerformanceId { get; set; }
		public int LessonId { get; set; }
		public string FullName { get; set; }
		public string Mark { get; set; }
	}
}
