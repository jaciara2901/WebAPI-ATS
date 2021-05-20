using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI_ATS.Models;

namespace WebAPI_ATS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public JobController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" SELECT JobId, JobDescription, JobRequirements FROM dbo.Job ";
            DataTable dt = new DataTable();
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ATSAppCon")))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    dt.Load(sr);
                    sr.Close();
                    conn.Close();
                }
            }

            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Job j)
        {
            string query = @" INSERT INTO dbo.Job VALUES (" +
                             "'" + j.JobDescription + "'," +
                             "'" + j.JobRequirements + "')";
            DataTable dt = new DataTable();
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ATSAppCon")))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    dt.Load(sr);
                    sr.Close();
                    conn.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Job j)
        {
            string query = @" UPDATE dbo.Job SET " +
                             " JobDescription = '" + j.JobDescription + "'," +
                             " JobRequirements = '" + j.JobRequirements + "'" +
                             " WHERE JobId = " + j.JobId;
            DataTable dt = new DataTable();
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ATSAppCon")))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    dt.Load(sr);
                    sr.Close();
                    conn.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @" DELETE FROM dbo.Job WHERE JobId = " + id;
            DataTable dt = new DataTable();
            SqlDataReader sr;
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("ATSAppCon")))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    sr = command.ExecuteReader();
                    dt.Load(sr);
                    sr.Close();
                    conn.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
