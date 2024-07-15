using DotNetCoreSampleApp.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetCoreSampleApp.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository()
        {
            string filePath = "C:\\Users\\BANSIDHARA\\source\\repos\\MVC Repo\\ShowITMVC\\DotNetCoreSampleApp\\DotNetCoreSampleApp\\Log.txt";

            string text = "StudentRepository Object Created:" + DateTime.Now.ToString();

            using (StreamWriter sw = new StreamWriter(filePath, true))
            { sw.WriteLine(text); }
        }

        public List<Student> GetAllStudents()
        {
            //DB Connection to get data
            return new List<Student>();
        }

        public Student GetStudentById(int id)
        {
            //DB Connection to get data
            return new Student();
        }
    }
}
