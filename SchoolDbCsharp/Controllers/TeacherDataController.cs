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
        //This Controller Will access the teachers table of our school database. Non-Deterministic.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of Teacher Objects with fields mapped to the database column values (first name, last name).
        /// </returns>
        /// <example>GET api/TeacherData/ListTeachers -> {Teacher Object, Teacher Object, Teacher Object...}</example>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();
            //Sql query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            // Debug.WriteLine("The query is:" + query);
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();



            MySqlDataReader ResultSet = cmd.ExecuteReader();


            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                int Teacherid = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
               

                Teacher NewTeacher = new Teacher();
                NewTeacher.Teacherid = Teacherid;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                
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
            cmd.Parameters.AddWithValue("@id", id);
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
        /// <summary>
        /// Deletes a teacher from the connected MySQL Database if the ID of that Teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="Teacherid">The ID of the Teacher.</param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int Teacherid)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id",Teacherid);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
      /// <summary>
             /// Adds an Teacher to the MySQL Database.
             /// </summary>
             /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table. Non-Deterministic.</param>
             /// <example>
             /// POST api/TeacherData/AddTeacher 
             /// FORM DATA / POST DATA / REQUEST BODY 
             /// {
             ///	"TeacherFName":"Christine",
             ///	"TeacherLName":"Bittle",
             ///	"EmployeeNumber":"T600",
             ///	"HireDate":"2016-06-13"
             ///	"Salary":"90.77"
             /// }
             /// </example>
    [HttpPost]
    
    public void AddTeacher([FromBody]Teacher NewTeacher)
    {
        //Create an instance of a connection
        MySqlConnection Conn = School.AccessDatabase(); 

        Debug.WriteLine(NewTeacher.TeacherFName);

        //Open the connection between the web server and database
        Conn.Open();

        //Establish a new command (query) for our database
        MySqlCommand cmd = Conn.CreateCommand();

        //SQL QUERY
        cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFName,@TeacherLName,@EmployeeNumber, CURRENT_DATE(), @Salary)";
        cmd.Parameters.AddWithValue("@TeacherFName", NewTeacher.TeacherFName);
        cmd.Parameters.AddWithValue("@TeacherLName", NewTeacher.TeacherLName);
        cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
        cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
        cmd.Prepare();

        cmd.ExecuteNonQuery();

        Conn.Close();



    }
        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <param name="id">primary key of the teacher that is being updated</param>
        
        [HttpPost]
        
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Debug.WriteLine(TeacherInfo.TeacherFName);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFName, teacherlname=@TeacherLName, hiredate=@HireDate, employeenumber=@EmployeeNumber  where teacherid=@Teacherid";
            cmd.Parameters.AddWithValue("@TeacherFName", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLName", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Teacherid", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}