using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI_ATS.BLL;
using WebAPI_ATS.Model;
using WebAPI_ATS.Models;

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
            CandidateBLL bll = new CandidateBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            List<CandidateInfo> lst = bll.Get();

            return new JsonResult(lst);
        }

        [HttpPost]
        public JsonResult Post (CandidateInfo c)
        {
            CandidateBLL bll = new CandidateBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Post(c);

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(CandidateInfo c)
        {
            CandidateBLL bll = new CandidateBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Put(c);

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            CandidateBLL bll = new CandidateBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Delete(id);

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

        [Route("GetAllNames")]
        [HttpGet]
        public JsonResult GetAllNames()
        {
            CandidateBLL bll = new CandidateBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            List<string> lst = bll.GetAllNames();

            return new JsonResult(lst);
        }

    }
}
