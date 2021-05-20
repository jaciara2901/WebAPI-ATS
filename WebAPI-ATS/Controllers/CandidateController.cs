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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace WebAPI_ATS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public CandidateController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @" SELECT CandidateId, CandidateName, CandidateAge, YearsOfExperience, ResumePath " +
                             " FROM dbo.Candidate ";
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
        public JsonResult Post (Candidate c)
        {
            string query = @" INSERT INTO dbo.Candidate VALUES (" +
                             "'" + c.CandidateName + "'," +
                             c.CandidateAge + "," +
                             c.YearsOfExperience + "," +
                             "'" + c.ResumePath + "')";
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
        public JsonResult Put(Candidate c)
        {
            string query = @" UPDATE dbo.Candidate SET " +
                             " CandidateName = '" + c.CandidateName + "'," +
                             " CandidateAge = " + c.CandidateAge + "," +
                             " YearsOfExperience = " + c.YearsOfExperience + "," +
                             " ResumePath = '" + c.ResumePath + "'" +
                             " WHERE CandidateId = " + c.CandidateId;
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
            string query = @" DELETE FROM dbo.Candidate WHERE CandidateId = " + id;
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0]; //Recupera primeiro arquivo anexado na requisição
                string filename = postedFile.FileName;
                var physicalPath = _environment.ContentRootPath + "/Resumes/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("api/Candidate/GetAllNames")]
        [HttpGet]
        public JsonResult GetAllNames()
        {
            string query = @" SELECT CandidateName FROM dbo.Candidate ";
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

    }
}
