using ExamApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.Models
{
    public class Teachers : BaseModel
    {
        public int Number { get; set; }
        public string SureName { get; set; } = string.Empty;
    }
}
