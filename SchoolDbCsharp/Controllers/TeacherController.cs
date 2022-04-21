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
        /// <summary>
        /// returns a webpage of the Teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET : /Teacher/Edit/{id}
        public ActionResult Edit(int id)
        {
            TeacherDataController controller=new TeacherDataController();
            Teacher SelectedTeacher= controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        // POST : /Teacher/Update/{id}
        /// <summary>
        /// updates the teacher
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFName">The updated first name of the teacher</param>
        /// <param name="TeacherLName">The updated last name of the teacher</param>
        /// <param name="HireDate">The updated hiredate of the teacher.</param>
        /// <param name="EmployeeNumber">The updated employee nuber of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFName":"Ifeyinwa",
        ///	"TeacherLName":"joseph",
        ///	"HireDate":"2022-03-05",
        ///	"EmployeeNumber":"T500"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFName, string TeacherLName, string HireDate, string EmployeeNumber)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFName = TeacherFName;
            TeacherInfo.TeacherLName = TeacherLName;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }


    }
}