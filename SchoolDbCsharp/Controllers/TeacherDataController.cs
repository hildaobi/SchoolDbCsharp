using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolDbCsharp.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace SchoolDbCsharp.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public List<Teacher> ListTeachers(string SearchKey)
        {
            if (SearchKey == null)
            {
                Debug.WriteLine("The search key is" + SearchKey);
            }
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();
            //Sql query
            string query = "Select * from teachers";
            if (SearchKey != null)
            {

                query = query + "where lower(teacherfname)=lower(@key)";
                cmd.Parameters.AddWithValue("@key", SearchKey);
                cmd.Prepare();
            }
            Debug.WriteLine("The query is:" + query);
            cmd.CommandText = query;

            MySqlDataReader ResultSet = cmd.ExecuteReader();


            List<Teacher> Teachers = new List<Teacher>{};

            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();

                NewTeacher.Teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = ResultSet["hiredate"].ToString();
                NewTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);

                Teachers.Add(NewTeacher);


            }
            Conn.Close();

            return Teachers;

        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();
           
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id" , id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

           

            while (ResultSet.Read())
            { 


                int Teacherid = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                int Salary = Convert.ToInt32(ResultSet["salary"]);

                newTeacher.Teacherid = Teacherid;
                newTeacher.TeacherFName = TeacherFName;
                newTeacher.TeacherLName = TeacherLName;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

            }
            Conn.Close();

            return newTeacher;
        }
    } 
}
