using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDbCsharp.Models;
using System.Diagnostics;

namespace SchoolDbCsharp.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        // Get:Teaccher/List
       
        public ActionResult List( string SearchKey = null)
        {
            //debugging to see if the search key is funtion
            //Debug.WriteLine("The key is " + SearchKey);
            // connect to database access
            //get list of teachers from database
           
           TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            
            //display the list of teacher through /view/teacher/List.cshtml
            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}
        

        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
            // displays info to /view/teacher/show.cshtml
            return View(newTeacher);
        }
        //Get: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
            // displays info to /view/teacher/show.cshtml
            return View(newTeacher);
        }
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNumber, string HireDate, int Salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFName);
            Debug.WriteLine(TeacherLName);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate ;
            NewTeacher.Salary = Salary;
           
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }



    }
}