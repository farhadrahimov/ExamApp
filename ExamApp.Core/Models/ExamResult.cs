using ExamApp.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Core.Models
{
    public class ExamResult
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid StudentId { get; set; }
        public string StudentName { get; set;} = string.Empty;
        public DateTime Date { get; set; }
        public int Grade { get; set; }
    }
}
