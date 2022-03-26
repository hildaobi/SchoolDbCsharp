using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDbCsharp.Models
{
    public class Teacher
    {

        public int Teacherid { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public string HireDate { get; set; }
        public int Salary { get; set; }
    }
}