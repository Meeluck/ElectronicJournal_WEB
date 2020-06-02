using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal_WEB.Models.ViewModel
{
	public class StudentsPerfomancesViemModel
	{
		public int UserId { get; set; }
		public string FullName { get; set; }
		public List<MarksDate> Marks { get; set; }
	}

	public class PerfomancesViewModel
	{
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
		public List<MarksDateNote> Marks { get; set; }
	}

	public class MarksDate
	{
		public string Mark { get; set; }
		public string Date { get; set; }
		public string LessonsType { get; set; }
	}
	public class MarksDateNote
	{
		public string Mark { get; set; }
		public string Date { get; set; }
		public string LessonsType { get; set; }
		public string Note { get; set; }
	}

	public class GroupPerfomanceViewModel
	{
		public int GroupId { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
		public List<StudentsPerfomancesUpdateViemModel> Perfomances { get; set; }
	}
	public class StudentsPerfomancesUpdateViemModel
	{
		public int UserId { get; set; }
		public string FullName { get; set; }
		public DateTime Date { get; set; }
		public string LessonDate { get; set; }
		public string LessonsType { get; set; }
		public List<string> Marks { get; set; }
	}
}
