using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_ATS.DAL;
using WebAPI_ATS.Model;

namespace WebAPI_ATS.BLL
{
    public class JobBLL
    {
        public String _conn;
        public List<JobInfo> Get()
        {
            JobDAL dal = new JobDAL();
            dal._conn = this._conn;
            return dal.Get();
        }

        public void Post(JobInfo j)
        {
            JobDAL dal = new JobDAL();
            dal._conn = this._conn;
            dal.Post(j);
        }

        public void Put(JobInfo j)
        {
            JobDAL dal = new JobDAL();
            dal._conn = this._conn;
            dal.Put(j);
        }
        public void Delete(int id)
        {
            JobDAL dal = new JobDAL();
            dal._conn = this._conn;
            dal.Delete(id);
        }
    }
}
