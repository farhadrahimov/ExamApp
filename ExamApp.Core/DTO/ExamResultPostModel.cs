using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.DTO
{
    public class ExamResultPostModel
    {
        public Guid SubjectId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }

        [Range(1, 10, ErrorMessage = "Value must be between 1 to 10")]
        public int Grade { get; set; }
    }
}
