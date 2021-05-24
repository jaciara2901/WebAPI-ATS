using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_ATS.Model;

namespace WebAPI_ATS.DAL
{
    public class JobDAL
    {
        public String _conn;

        public List<JobInfo> Get()
        {
            List<JobInfo> lst = new List<JobInfo>();
            string query = @" SELECT JobId, JobDescription, JobRequirements FROM dbo.Job ";

            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    while (sr.Read())
                    {
                        JobInfo info = new JobInfo();
                        info.JobId = int.Parse(sr["JobId"].ToString());
                        info.JobDescription = sr["JobDescription"].ToString();
                        info.JobRequirements = sr["JobRequirements"].ToString();
                        lst.Add(info);
                    }
                    sr.Close();
                    conn.Close();
                }
            }

            return lst;
        }

        public void Post(JobInfo j)
        {
            string query = @" INSERT INTO dbo.Job VALUES (" +
                             "'" + j.JobDescription + "'," +
                             "'" + j.JobRequirements + "')";
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    sr.Close();
                    conn.Close();
                }
            }
        }

        public void Put(JobInfo j)
        {
            string query = @" UPDATE dbo.Job SET " +
                             " JobDescription = '" + j.JobDescription + "'," +
                             " JobRequirements = '" + j.JobRequirements + "'" +
                             " WHERE JobId = " + j.JobId;
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    sr.Close();
                    conn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            string query = @" DELETE FROM dbo.Job WHERE JobId = " + id;
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    sr.Close();
                    conn.Close();
                }
            }
        }
    }
}
