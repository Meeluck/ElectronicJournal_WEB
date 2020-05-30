using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal_WEB.Models.ViewModel
{
	public class IndexTeacherViewModel
	{
		public int LessonId { get; set; }
		public string SubjectName { get; set; }
		public string LessonType { get; set; }
		public string GroupName { get; set; }
		public string LessonDate { get; set; }
		public string LessonTime { get; set; }
		public string ClassroomName { get; set; }
	}
}
