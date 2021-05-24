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
using WebAPI_ATS.BLL;
using WebAPI_ATS.Model;

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
            JobBLL bll = new JobBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            List<JobInfo> lst = bll.Get();

            return new JsonResult(lst);
        }

        [HttpPost]
        public JsonResult Post(JobInfo j)
        {
            JobBLL bll = new JobBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Post(j);

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(JobInfo j)
        {
            JobBLL bll = new JobBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Put(j);

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            JobBLL bll = new JobBLL();
            bll._conn = _configuration.GetConnectionString("ATSAppCon");
            bll.Delete(id);

            return new JsonResult("Deleted Successfully");
        }
    }
}
