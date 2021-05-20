using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_ATS.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirements { get; set; }
    }
}
