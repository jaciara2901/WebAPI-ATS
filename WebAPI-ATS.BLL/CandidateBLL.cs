using System;
using System.Collections.Generic;
using WebAPI_ATS.DAL;
using WebAPI_ATS.Model;

namespace WebAPI_ATS.BLL
{
    public class CandidateBLL
    {
        public String _conn;
        public List<CandidateInfo> Get()
        {
            CandidateDAL dal = new CandidateDAL();
            dal._conn = this._conn;
            return dal.Get();
        }

        public void Post(CandidateInfo c)
        {
            CandidateDAL dal = new CandidateDAL();
            dal._conn = this._conn;
            dal.Post(c);
        }

        public void Put(CandidateInfo c)
        {
            CandidateDAL dal = new CandidateDAL();
            dal._conn = this._conn;
            dal.Put(c);
        }
        public void Delete(int id)
        {
            CandidateDAL dal = new CandidateDAL();
            dal._conn = this._conn;
            dal.Delete(id);
        }

        public List<string> GetAllNames()
        {
            CandidateDAL dal = new CandidateDAL();
            dal._conn = this._conn;
            return dal.GetAllNames();
        }
    }
}
