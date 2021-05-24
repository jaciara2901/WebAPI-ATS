using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using WebAPI_ATS.Model;

namespace WebAPI_ATS.DAL
{
    public class CandidateDAL
    {
        public String _conn;

        public List<CandidateInfo> Get()
        {
            List<CandidateInfo> lst = new List<CandidateInfo>();

            string query = @" SELECT CandidateId, CandidateName, CandidateAge, YearsOfExperience, ResumePath " +
                                 " FROM dbo.Candidate ";
            SqlDataReader dr;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    dr = command.ExecuteReader();

                    while(dr.Read())
                    {
                        CandidateInfo info = new CandidateInfo();
                        info.CandidateId = int.Parse(dr["CandidateId"].ToString());
                        info.CandidateName = dr["CandidateName"].ToString();
                        info.CandidateAge = int.Parse(dr["CandidateAge"].ToString());
                        info.YearsOfExperience = int.Parse(dr["YearsOfExperience"].ToString());
                        info.ResumePath = dr["ResumePath"].ToString();
                        lst.Add(info);
                    }

                    dr.Close();
                    conn.Close();
                }
            }

            return lst;
        }

        public void Post(CandidateInfo c)
        {
            string query = @" INSERT INTO dbo.Candidate VALUES (" +
                             "'" + c.CandidateName + "'," +
                             c.CandidateAge + "," +
                             c.YearsOfExperience + "," +
                             "'" + c.ResumePath + "')";
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
        public void Put(CandidateInfo c)
        {
            string query = @" UPDATE dbo.Candidate SET " +
                             " CandidateName = '" + c.CandidateName + "'," +
                             " CandidateAge = " + c.CandidateAge + "," +
                             " YearsOfExperience = " + c.YearsOfExperience + "," +
                             " ResumePath = '" + c.ResumePath + "'" +
                             " WHERE CandidateId = " + c.CandidateId;
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
            string query = @" DELETE FROM dbo.Candidate WHERE CandidateId = " + id;
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

        public List<string> GetAllNames()
        {
            List<string> lst = new List<string>();

            string query = @" SELECT CandidateName FROM dbo.Candidate ";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    SqlDataReader sr = command.ExecuteReader();
                     
                    while (sr.Read())
                        lst.Add(sr["CandidateName"].ToString());

                    sr.Close();
                    conn.Close();
                }
            }

            return lst;
        }
    }
}
