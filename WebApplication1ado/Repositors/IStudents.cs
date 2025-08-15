using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1ado.Models;

namespace WebApplication1ado.Repositors
{
    internal interface IStudents
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetStudentById(int id);
        Task<bool> InsertStudent(Student student);
        Task<bool> DeleteStudent(int id);

        Task<bool> Updatestudent(Student student);
    }
}
