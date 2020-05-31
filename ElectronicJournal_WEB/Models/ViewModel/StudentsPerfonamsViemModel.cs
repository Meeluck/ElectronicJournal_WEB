using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal_WEB.Models.ViewModel
{
	public class StudentsPerfonamsViemModel
	{
		public int UserId { get; set; }
		public string FullName { get; set; }
		public List<MarksDate> Marks { get; set; }
	}
	public class MarksDate
	{
		public string Mark { get; set; }
		public string Date { get; set; }
		public string LessonsType { get; set; }
	}
}
