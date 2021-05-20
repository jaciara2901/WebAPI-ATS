using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_ATS.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public int CandidateAge { get; set; }
        public int YearsOfExperience { get; set; }
        public string ResumePath { get; set; }
        
    }
}
