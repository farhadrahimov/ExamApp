using System.ComponentModel.DataAnnotations;

namespace ExamApp.Core.Helpers
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
