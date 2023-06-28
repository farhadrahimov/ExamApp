using ExamApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.DTO
{
    public class ExamResultsRequestModel
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Search { get; set; } = string.Empty;
        public GraduateEnum Graduate { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
