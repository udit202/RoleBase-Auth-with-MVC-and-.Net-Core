using interview3core.Models;

namespace interview3core.Services.Interfaces
{
    public interface IStudents
    {
        Task<IEnumerable<Students>> GetStudentsAsync();
        Task<Students> GetStudentByID(int id);
        Task<bool> createStudentasyn(Students student);
        Task<bool> updateStudentasyn(Students student);
        Task<bool> deleteStudentasyn(int id);
    }
}
