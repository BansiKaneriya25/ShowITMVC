using DotNetCoreSampleApp.Model;
using System.Collections.Generic;

namespace DotNetCoreSampleApp.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
    }
}
