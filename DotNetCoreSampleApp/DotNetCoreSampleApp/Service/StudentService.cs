using DotNetCoreSampleApp.Model;
using DotNetCoreSampleApp.Repository;
using System.Collections.Generic;

namespace DotNetCoreSampleApp.Service
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> GetStudents()
        {
            return _studentRepository.GetAllStudents();
        }

        public Student GetStudent(int id)
        {
            return _studentRepository.GetStudentById(id);
        }
    }
}
