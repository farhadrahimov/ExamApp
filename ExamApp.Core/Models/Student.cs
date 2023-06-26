using ExamApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.Models
{
    public class Student : BaseModel
    {
        [Range(1, 99999, ErrorMessage = "Value must be between 1 to 99999")]
        public int Number { get; set; }

        [StringLength(50)]
        public string SureName { get; set; } = string.Empty;
        public Guid ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }
}
