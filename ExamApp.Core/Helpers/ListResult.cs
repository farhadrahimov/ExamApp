namespace ExamApp.Core.Helpers
{
    public class ListResult<T> where T : class
    {
        public IEnumerable<T>? List { get; set; }
        public int TotalCount { get; set; } = 0;
    }
}
