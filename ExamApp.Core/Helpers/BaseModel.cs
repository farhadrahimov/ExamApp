namespace ExamApp.Core.Helpers
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RowNum { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
