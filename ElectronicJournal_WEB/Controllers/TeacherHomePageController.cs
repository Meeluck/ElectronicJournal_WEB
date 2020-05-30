﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicJournal_WEB.Context;
using ElectronicJournal_WEB.Models;
using ElectronicJournal_WEB.Models.DatabaseModel;
using ElectronicJournal_WEB.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectronicJournal_WEB.Controllers
{
    public class TeacherHomePageController : Controller
    {
        Users _user;
        ElectronicalJournalContext db;

        public TeacherHomePageController()
        {
            _user = new Users();
            db = new ElectronicalJournalContext();
        }

        public IActionResult Index()
        {
            _user = db.Users.Find(UserSession.GetUserId);
            List<IndexTeacherViewModel> lessonList = new List<IndexTeacherViewModel>();

            lessonList = (from tch in db.Teachers
                          join tc_ls in db.TeacherLessons on tch.TeacherId equals tc_ls.TeacherId
                          into tc_lsDatails
                          from tc_lsDat in tc_lsDatails.DefaultIfEmpty()
                          join ls in db.Lessons on tc_lsDat.LessonId equals ls.LessonId
                          into lsDatails
                          from lsDat in lsDatails.DefaultIfEmpty()
                          join sbj in db.Subjects on lsDat.SubjectId equals sbj.SubjectId
                          join lt in db.LessonTypes on lsDat.LessonTypeId equals lt.LessonTypeId
                          into ltDatails
                          from ltDat in ltDatails.DefaultIfEmpty()
                          join ts in db.TimeSchedules on lsDat.TimeScheduleId equals ts.TimeScheduleId
                          into tsDatails
                          from tsDat in tsDatails.DefaultIfEmpty()
                          join cls in db.Classrooms on lsDat.ClassroomId equals cls.ClassroomId
                          into clsDatail
                          from clsDat in clsDatail.DefaultIfEmpty()
                          join build in db.Buildings on clsDat.BuildingId equals build.BuildingId
                          into buildDatail
                          from buidDat in buildDatail.DefaultIfEmpty()
                          join gr_ls in db.GroupLessons on lsDat.LessonId equals gr_ls.LessonId
                          join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                          where tch.UserId == UserSession.GetUserId
                          where lsDat.Date > DateTime.Today
                          orderby lsDat.Date
                          select new IndexTeacherViewModel
                          {
                              LessonId = lsDat.LessonId,
                              GroupName = gr.GroupName,
                              LessonDate = lsDat.Date.ToLongDateString(),
                              LessonTime = tsDat.TimeInterval,
                              SubjectName = sbj.SubjectName,
                              LessonType = ltDat.LessonTypeName,
                              ClassroomName = buidDat.BuildingName + " " + clsDat.ClassroomName
                          }).ToList();

            return View(lessonList);
        }
        [HttpGet]
        public IActionResult StudentAssessment()
        {
            List<Groups> groups = new List<Groups>();
            groups = (from tch in db.Teachers
                      join tc_ls in db.TeacherLessons on tch.TeacherId equals tc_ls.TeacherId
                      into tc_lsDatails
                      from tc_lsDat in tc_lsDatails.DefaultIfEmpty()
                      join ls in db.Lessons on tc_lsDat.LessonId equals ls.LessonId
                      into lsDatails
                      from lsDat in lsDatails.DefaultIfEmpty()
                      join gr_ls in db.GroupLessons on lsDat.LessonId equals gr_ls.LessonId
                      join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                      select new Groups
                      {
                          GroupId = gr.GroupId,
                          GroupName = gr.GroupName,
                          YearFormationGroup = gr.YearFormationGroup
                      }).Distinct().ToList();

            return View(groups);
        }

        public IActionResult GroupsLessons(int id) //на входе получаем id группы
        {
            List<IndexTeacherViewModel> lessonList = new List<IndexTeacherViewModel>();

            lessonList = (from tch in db.Teachers
                          join tc_ls in db.TeacherLessons on tch.TeacherId equals tc_ls.TeacherId
                          into tc_lsDatails
                          from tc_lsDat in tc_lsDatails.DefaultIfEmpty()
                          join ls in db.Lessons on tc_lsDat.LessonId equals ls.LessonId
                          into lsDatails
                          from lsDat in lsDatails.DefaultIfEmpty()
                          join sbj in db.Subjects on lsDat.SubjectId equals sbj.SubjectId
                          join lt in db.LessonTypes on lsDat.LessonTypeId equals lt.LessonTypeId
                          into ltDatails
                          from ltDat in ltDatails.DefaultIfEmpty()
                          join ts in db.TimeSchedules on lsDat.TimeScheduleId equals ts.TimeScheduleId
                          into tsDatails
                          from tsDat in tsDatails.DefaultIfEmpty()
                          join gr_ls in db.GroupLessons on lsDat.LessonId equals gr_ls.LessonId
                          join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                          where tch.UserId == UserSession.GetUserId
                          where gr.GroupId == id
                          where lsDat.Date <= DateTime.Today
                          orderby lsDat.Date
                          select new IndexTeacherViewModel
                          {
                              LessonId = lsDat.LessonId,
                              GroupName = gr.GroupName,
                              LessonDate = lsDat.Date.ToLongDateString(),
                              LessonTime = tsDat.TimeInterval,
                              SubjectName = sbj.SubjectName + "\n" + $"({ltDat.LessonTypeName})",
                              LessonType = ltDat.LessonTypeName
                          }).ToList();

            return View(lessonList);
        }

        public IActionResult AcademicPerfomance(int id)
        {
            List<StudentsPerfomanceViewModel> studentsGroups;
            var groupName = (from ls in db.Lessons
                             join sbj in db.Subjects on ls.SubjectId equals sbj.SubjectId
                             join gr_ls in db.GroupLessons on ls.LessonId equals gr_ls.LessonId
                             join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                             where ls.LessonId == id
                             select new
                             {
                                 GroupName = gr.GroupName,
                                 SubjectName = sbj.SubjectName,
                                 Date = ls.Date.ToShortDateString()

                             }).FirstOrDefault();
            ViewBag.SubjectName = groupName.SubjectName;
            ViewBag.GroupName = groupName.GroupName;
            ViewBag.Date = groupName.Date;

            List<string> marks = new List<string>
            {
                "+","-","1","2","3","4","5","Зачет","Незачет"
            };
            ViewBag.Marks = new SelectList(marks);
            List<AcademicPerformances> studentPerfomance = db.AcademicPerformances.Where(p => p.LessonId == id).ToList();
            //если есть записи, то всем уже были проставлены оценки
            if (studentPerfomance.Count == 0)
            {
                studentsGroups = (from ls in db.Lessons
                                  join sbj in db.Subjects on ls.SubjectId equals sbj.SubjectId
                                  join gr_ls in db.GroupLessons on ls.LessonId equals gr_ls.LessonId
                                  join gr in db.Groups on gr_ls.GroupId equals gr.GroupId
                                  join st_gr in db.StudentGroups on gr.GroupId equals st_gr.GroupId
                                  join us in db.Users on st_gr.UserId equals us.UserId
                                  where ls.LessonId == id
                                  select new StudentsPerfomanceViewModel
                                  {
                                      UserId = us.UserId,
                                      LessonId = id,
                                      FullName = us.LastName + " " + us.FirstName + " "
                                                 + (string.IsNullOrEmpty(us.MiddleName) ? string.Empty : us.MiddleName),
                                  }).ToList();
                return View("AcademicPerfomanceNew",studentsGroups);
            }
            else
            {
                studentsGroups = (from ls in db.Lessons
                                  join ap in db.AcademicPerformances on ls.LessonId equals ap.LessonId
                                  join us in db.Users on ap.UserId equals us.UserId
                                  where ls.LessonId == id
                                  select new StudentsPerfomanceViewModel
                                  {
                                      UserId = us.UserId,
                                      AcademicPerformanceId=ap.AcademicPerformanceId,
                                      LessonId = id,
                                      FullName = us.LastName + " " + us.FirstName + " "
                                                 + (string.IsNullOrEmpty(us.MiddleName) ? string.Empty : us.MiddleName),
                                      Mark = ap.Mark
                                  }).ToList();
                return View("AcademicPerfomanceUpdate",studentsGroups);
            }
        }

        [HttpPost]
        public IActionResult AcademicPerfomance(List<StudentsPerfomanceViewModel> studentPerfomance)
        {
            List<AcademicPerformances> assessments = new List<AcademicPerformances>();

            foreach(StudentsPerfomanceViewModel item in studentPerfomance)
            {
                assessments.Add(new AcademicPerformances
                {
                    UserId = item.UserId,
                    LessonId = item.LessonId,
                    Mark = item.Mark
                });
            }
            db.AcademicPerformances.AddRange(assessments);
            db.SaveChanges();

            return new RedirectToActionResult("StudentAssessment", "TeacherHomePage", null);
        }

        [HttpPost]
        public IActionResult AcademicPerfomanceUpdate(List<StudentsPerfomanceViewModel> studentsPerfomances)
        {
            List<AcademicPerformances> assessments = new List<AcademicPerformances>();

            foreach (StudentsPerfomanceViewModel item in studentsPerfomances)
            {
                assessments.Add(new AcademicPerformances
                {
                    AcademicPerformanceId = item.AcademicPerformanceId,
                    UserId = item.UserId,
                    LessonId = item.LessonId,
                    Mark = item.Mark
                });
            }
            db.AcademicPerformances.UpdateRange(assessments);
            db.SaveChanges();
            return new RedirectToActionResult("StudentAssessment", "TeacherHomePage", null);
        }
    }
}