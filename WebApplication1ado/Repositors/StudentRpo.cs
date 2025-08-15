using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1ado.Models;

namespace WebApplication1ado.Repositors
{
    public class StudentRpo : IStudents
    {
        private readonly string cs;
        SqlConnection SqlConnection;
        public StudentRpo() {
            //cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        }
        public Task<bool> DeleteStudent(int id)
        {
            bool deleted = false;
            using (SqlConnection = new SqlConnection(cs))
            {
                try
                {
                    SqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteStudent", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id",id);
                    int delete = cmd.ExecuteNonQuery();

                    if (delete > 0)
                    {
                        deleted = true;
                    }
                    else
                    {
                        deleted = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    SqlConnection.Close();
                }
                return Task.FromResult(deleted);
            }
        }

        public Task<IEnumerable<Student>> GetAll()
        {
            using (SqlConnection = new SqlConnection(cs))
            {
                List<Student> Studentlist = new List<Student>();
                try
                {
                    SqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetStudents", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            FatherName = Convert.ToString(reader["FatherName"]),
                            MotherName = Convert.ToString(reader["MotherName"]),
                        };
                        Studentlist.Add(student);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    SqlConnection.Close();
                }
                return Task.FromResult<IEnumerable<Student>>(Studentlist);
            }
        }

        public Task<Student> GetStudentById(int id)
        {
            using (SqlConnection = new SqlConnection(cs))
            {
                Student Student = null;
                try
                {
                    SqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("GetById", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student = new Student
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            FatherName = Convert.ToString(reader["FatherName"]),
                            MotherName = Convert.ToString(reader["MotherName"]),
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    SqlConnection.Close();
                }
                return Task.FromResult(Student);
            }
        }

        public Task<bool> InsertStudent(Student student)
        {
            bool inserted = false;
            using (SqlConnection = new SqlConnection(cs))
            {
                try
                {
                    SqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("InsertStudent", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@Fathername", student.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", student.MotherName);
                    int insert = cmd.ExecuteNonQuery();

                    if (insert > 0) {
                        inserted=true;
                    }
                    else
                    {
                        inserted = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    SqlConnection.Close();
                }
                return Task.FromResult(inserted);
            }
        }

        public Task<bool> Updatestudent(Student student)
        {

            bool inserted = false;
            using (SqlConnection = new SqlConnection(cs))
            {
                try
                {
                    SqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UpdateStudent", SqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", student.Id);
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@Fathername", student.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", student.MotherName);
                    int insert = cmd.ExecuteNonQuery();

                    if (insert > 0)
                    {
                        inserted = true;
                    }
                    else
                    {
                        inserted = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    SqlConnection.Close();
                }
                return Task.FromResult(inserted);
            }
        }
    }
}