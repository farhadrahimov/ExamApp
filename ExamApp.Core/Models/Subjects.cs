using ExamApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.Models
{
    public class Subjects : BaseModel
    {
        [StringLength(3)]
        public string Code { get; set; } = string.Empty;

        [Range(1, 12, ErrorMessage = "Value must be between 1 to 12")]
        public int Class { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }
}
