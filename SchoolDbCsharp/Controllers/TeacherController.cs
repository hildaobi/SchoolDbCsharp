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
        // action to list of all teacher
        [Route("Teacher/List/{SearchKey}")]
        public ActionResult List( string SearchKey = null)
        {
            //debugging to see if the search key is funtion
            Debug.WriteLine("The key is " + SearchKey);
            // connect to database access
            //get list of teachers from database
           
           TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            
            //display the list of teacher through /view/teacher/List.cshtml
            return View(Teachers);
        }

        //GET: /teacher/show/{id}
        //[Route("/Teacher/Show/{Teacherid}")]

        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
            // displays info to /view/teacher/show.cshtml
            return View(newTeacher);
        }

      
    }
}