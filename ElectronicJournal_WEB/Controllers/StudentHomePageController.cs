using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicJournal_WEB.Context;
using ElectronicJournal_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using ElectronicJournal_WEB.Models.ViewModel;
using ElectronicJournal_WEB.Models.DatabaseModel;
using System.Text.RegularExpressions;

namespace ElectronicJournal_WEB.Controllers
{
    public class StudentHomePageController : Controller
    {
        ElectronicalJournalContext db;

        public StudentHomePageController()
        {
            db = new ElectronicalJournalContext();
        }

        public IActionResult Index()
        {
            ViewBag.AccessLevelId = UserSession.AccessLevelId;
            List<StudentScheldueViewModel> lessonList;

            var group = from us in db.Users
                             join st_gr in db.StudentGroups on us.UserId equals st_gr.UserId
                             join gr in db.Groups on st_gr.GroupId equals gr.GroupId
                             where us.UserId == UserSession.GetUserId
                             select new Groups
                             {
                                 GroupId = gr.GroupId
                             };
            int grId= 0;
            foreach (var item in group)
                grId = item.GroupId;

            lessonList = (from ls in db.Lessons
                          join sub in db.Subjects on ls.SubjectId equals sub.SubjectId
                          join lt in db.LessonTypes on ls.LessonTypeId equals lt.LessonTypeId
                          into ltDatails
                          from ltDat in ltDatails.DefaultIfEmpty()
                          join ts in db.TimeSchedules on ls.TimeScheduleId equals ts.TimeScheduleId
                          into tsDatails
                          from tsDat in tsDatails.DefaultIfEmpty()
                          join cls in db.Classrooms on ls.ClassroomId equals cls.ClassroomId
                          into clsDatail
                          from clsDat in clsDatail.DefaultIfEmpty()
                          join build in db.Buildings on clsDat.BuildingId equals build.BuildingId
                          into buildDatail
                          from buidDat in buildDatail.DefaultIfEmpty()
                          join gr_ls in db.GroupLessons on ls.LessonId equals gr_ls.LessonId
                          join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                          join tl in db.TeacherLessons on ls.LessonId equals tl.LessonId
                          join tch in db.Teachers on tl.TeacherId equals tch.TeacherId
                          join us in db.Users on tch.UserId equals us.UserId
                          where gr.GroupId == grId
                          where ls.Date >= DateTime.Today
                          orderby ls.Date
                          select new StudentScheldueViewModel
                          {
                              TeacherName = us.LastName + " " + us.LastName + " " + (!string.IsNullOrEmpty(us.MiddleName) ? us.MiddleName : string.Empty),
                              SubjectName = sub.SubjectName,
                              LessonType = ltDat.LessonTypeName,
                              Data = ls.Date.ToLongDateString(),
                              Time = !string.IsNullOrEmpty(tsDat.TimeInterval) ? tsDat.TimeInterval : string.Empty,
                              ClassroomName = buidDat.BuildingName + " " + clsDat.ClassroomName
                          }).ToList();
            return View(lessonList);
        }
        //личная успеваемость студента по всем предметам
        public IActionResult StudentPerfomance()
        {
            ViewBag.AccessLevelId = UserSession.AccessLevelId;
            List<PerfomancesViewModel> perfomances = new List<PerfomancesViewModel>();

            var lessons = (from us in db.Users
                          join stgr in db.StudentGroups on us.UserId equals stgr.UserId
                          join gr in db.Groups on stgr.GroupId equals gr.GroupId
                          join grls in db.GroupLessons on gr.GroupId equals grls.GroupId
                          join ls in db.Lessons on grls.LessonId equals ls.LessonId
                          join sub in db.Subjects on ls.SubjectId equals sub.SubjectId
                          where us.UserId == UserSession.GetUserId
                          where ls.Date <= DateTime.Today
                          select new
                          {
                              SubjectId = ls.SubjectId,
                              SubjectName = sub.SubjectName,
                          }).Distinct();
            foreach (var item in lessons)
            {
                perfomances.Add(new PerfomancesViewModel
                {
                    SubjectId = item.SubjectId,
                    SubjectName = item.SubjectName,
                    Marks = new List<MarksDateNote>()
                });
            }
            foreach(PerfomancesViewModel item in perfomances)
            {
                item.Marks = (from ls in db.Lessons
                              join lt in db.LessonTypes on ls.LessonTypeId equals lt.LessonTypeId
                              into ltDatail
                              from ltDat in ltDatail.DefaultIfEmpty()
                              join ap in db.AcademicPerformances on ls.LessonId equals ap.LessonId
                              where ls.SubjectId == item.SubjectId
                              where ap.UserId == UserSession.GetUserId
                              orderby ls.Date
                              select new MarksDateNote
                              {
                                  Mark = ap.Mark,
                                  Date = ls.Date.ToShortDateString(),
                                  LessonsType = ltDat.LessonTypeName,
                                  Note = ls.Notes
                              }).ToList();
            }
            return View(perfomances);
        }
        //выбор предмета, для просмотра успеваемости всей группы
        public IActionResult SubjectSelection()
        {
            ViewBag.AccessLevelId = UserSession.AccessLevelId;
            var group = from us in db.Users
                        join st_gr in db.StudentGroups on us.UserId equals st_gr.UserId
                        join gr in db.Groups on st_gr.GroupId equals gr.GroupId
                        where us.UserId == UserSession.GetUserId
                        select new Groups
                        {
                            GroupId = gr.GroupId
                        };
            int grId = 0;
            foreach (var item in group)
                grId = item.GroupId;

            List<Subjects> subjects = (from gr in db.Groups
                                       join grls in db.GroupLessons on gr.GroupId equals grls.GroupId
                                       join ls in db.Lessons on grls.LessonId equals ls.LessonId
                                       join sub in db.Subjects on ls.SubjectId equals sub.SubjectId
                                       where gr.GroupId == grId
                                       select new Subjects
                                       {
                                           SubjectId = sub.SubjectId,
                                           SubjectName = sub.SubjectName
                                       }).Distinct().ToList();

            return View(subjects);
        }
        //успеваемость группы по выбранному предмету
        public IActionResult GroupPerfomance(int id)
        {
            ViewBag.AccessLevelId = UserSession.AccessLevelId;

            var group = from us in db.Users
                        join st_gr in db.StudentGroups on us.UserId equals st_gr.UserId
                        join gr in db.Groups on st_gr.GroupId equals gr.GroupId
                        where us.UserId == UserSession.GetUserId
                        select new Groups
                        {
                            GroupId = gr.GroupId,
                            GroupName = gr.GroupName
                        };
            int grId = 0;
            foreach (var item in group)
                grId = item.GroupId;

            List<StudentsPerfomancesViemModel> studentPerfonams = new List<StudentsPerfomancesViemModel>();

            ViewBag.SubjectName = db.Subjects.Find(id).SubjectName;
            ViewBag.GroupName = db.Groups.Find(grId).GroupName;

            var studnetGroupd = from gr in db.Groups
                                join st_gr in db.StudentGroups on gr.GroupId equals st_gr.GroupId
                                join us in db.Users on st_gr.UserId equals us.UserId
                                where gr.GroupId == grId
                                select new
                                {
                                    UserId = us.UserId,
                                    FullName = us.LastName + " " + us.FirstName + (string.IsNullOrEmpty(us.MiddleName) ? string.Empty : us.MiddleName)
                                };
            foreach (var item in studnetGroupd)
            {
                studentPerfonams.Add(new StudentsPerfomancesViemModel
                {
                    UserId = item.UserId,
                    FullName = item.FullName,
                    Marks = new List<MarksDate>()
                });
            }

            foreach (StudentsPerfomancesViemModel item in studentPerfonams)
            {
                item.Marks = (from ls in db.Lessons
                              join lt in db.LessonTypes on ls.LessonTypeId equals lt.LessonTypeId
                              into ltDatail
                              from ltDat in ltDatail.DefaultIfEmpty()
                              join ap in db.AcademicPerformances on ls.LessonId equals ap.LessonId
                              where ls.SubjectId == id
                              where ap.UserId == item.UserId
                              orderby ls.Date
                              select new MarksDate
                              {
                                  Date = ls.Date.ToShortDateString(),
                                  Mark = ap.Mark,
                                  LessonsType = ltDat.LessonTypeName
                              }).ToList();
            }


            return View(studentPerfonams);
        }

        public IActionResult LogOut()
        {
            UserSession.LogOut();
            return Redirect("/Authorization/Index");
        }
    }
}