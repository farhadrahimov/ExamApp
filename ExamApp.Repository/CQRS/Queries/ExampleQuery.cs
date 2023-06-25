using ExamApp.Core.Models;

namespace ExamApp.Repository.CQRS.Queries
{
    public interface IExampleQuery
    {
        ExampleModel GetAll();
    }

    public class ExampleQuery : IExampleQuery
    {
        public ExampleModel GetAll()
        {
            ExampleModel model = new ExampleModel()
            {
                Id = 1,
                Name = "Test",
                DeleteStatus = false,
                RowNum = 1
            };
            return model;
        }
    }
}
