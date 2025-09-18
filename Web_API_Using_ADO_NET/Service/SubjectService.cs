using Web_API_Using_ADO_NET.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Web_API_Using_ADO_NET.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly string connectionString;
        public SubjectService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public List<Subject> GetAll()
        {
            var subjects = new List<Subject>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, [Desc], Code FROM Subjects", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Subject sub = new Subject()
                    {
                        Id = reader.GetInt32(0),
                        Desc = reader.GetString(1),
                        Code = reader.GetString(2)
                    };

                    subjects.Add(sub);
                }
                connection.Close();
            }
            return subjects;
        }

        public Subject GetById(int id)
        {
            Subject subject = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, [Desc], Code FROM Subjects WHERE Id=@Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    subject = new Subject
                    {
                        Id = reader.GetInt32(0),
                        Desc = reader.GetString(1),
                        Code = reader.GetString(2)
                    };
                }
                connection.Close();
            }
            return subject;
        }


        public bool Add(Subject subject)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Subjects WHERE Code=@Code", connection);
                checkCmd.Parameters.AddWithValue("@Code", subject.Code);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    return false;
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Subjects ([Desc], Code) VALUES (@Desc, @Code)", connection);
                cmd.Parameters.AddWithValue("@Desc", subject.Desc);
                cmd.Parameters.AddWithValue("@Code", subject.Code);
                cmd.ExecuteNonQuery();
            }
            return true;
        }


        public bool Update(Subject subject)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Subjects WHERE Id=@Id", connection);
                checkCmd.Parameters.AddWithValue("@Id", subject.Id);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    return false;
                }

                SqlCommand cmd = new SqlCommand("UPDATE Subjects SET [Desc]=@Desc, Code=@Code WHERE Id=@Id", connection);
                cmd.Parameters.AddWithValue("@Id", subject.Id);
                cmd.Parameters.AddWithValue("@Desc", subject.Desc);
                cmd.Parameters.AddWithValue("@Code", subject.Code);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Subjects WHERE Id=@Id", connection);
                checkCmd.Parameters.AddWithValue("@Id", id);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    return false;
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM Subjects WHERE Id=@Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

    }

}

