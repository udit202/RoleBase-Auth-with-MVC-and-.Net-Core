using interview3core.Models;
using interview3core.Services.Interfaces;
using interview3core.DBconnect;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace interview3core.Services.implementations
{
    public class StudentRepo : IStudents
    {
        private readonly DBconnectionInterview _interviewConnection;
        private readonly ILogger<StudentRepo> _logger;
        public StudentRepo(DBconnectionInterview connection, ILogger<StudentRepo> logger)
        {
            _interviewConnection = connection;
            _logger = logger;
        }
        public async Task<bool> createStudentasyn(Students student)
        {
            try
            {
                await _interviewConnection.Students.AddAsync(student);
                await _interviewConnection.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                _logger.LogError("Error while insert data", ex.Message);
                return false;
            }
        }

        public async Task<bool> deleteStudentasyn(int id)
        {
            try
            {
                var existdata = await GetStudentByID(id);
                if (existdata == null)
                {
                    return false;
                }
                else
                {
                    _interviewConnection.Remove(id);
                    await _interviewConnection.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Delete data", ex.Message);
                return false;
            }
        }

        public async Task<Students> GetStudentByID(int id)
        {
            try
            {
                var student= await _interviewConnection.Students.FirstOrDefaultAsync(x=>x.Id==id);
                if (student == null) {
                    return null;
                }
                else
                {
                    return student;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetch data with iD", ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Students>> GetStudentsAsync()
        {
            try
            {
                var student = await _interviewConnection.Students.OrderByDescending(x=>x.Id).ToListAsync();
                if (student == null)
                {
                    return null;
                }
                else
                {
                    return student;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while All fetch data ", ex.Message);
                return null;
            }
        }

        public async Task<bool> updateStudentasyn(Students student)
        {
            try
            {
                var existdata =await GetStudentByID(student.Id);
                if (existdata == null) {
                    return false;
                }
                else
                {
                    existdata.Name = student.Name;
                    existdata.FatherName= student.FatherName;
                    existdata.MotherName= student.MotherName;
                    await _interviewConnection.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while insert data", ex.Message);
                return false;
            }
        }
    }
}
