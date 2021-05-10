using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Repository
{
    public interface IStudentRepository 
    {
        void SaveStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(long id);
        void DeleteStudent(long id);
        void UpdateStudent(Student student);
    }
}
