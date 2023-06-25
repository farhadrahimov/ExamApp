using ExamApp.Core.Models;
using ExamApp.Repository.CQRS.Commands;
using ExamApp.Repository.CQRS.Queries;

namespace ExamApp.Repository.Repositories
{
    public interface IExampleRepository
    {
        ExampleModel GetAll();
        bool Add(int number);
    }

    public class ExampleRepository : IExampleRepository
    {
        private readonly IExampleQuery _exampleQuery;
        private readonly IExampleCommand _exampleCommand;

        public ExampleRepository(IExampleQuery exampleQuery, IExampleCommand exampleCommand)
        {
            _exampleQuery = exampleQuery;
            _exampleCommand = exampleCommand;
        }

        public bool Add(int number)
        {
            return _exampleCommand.Add(number);
        }

        public ExampleModel GetAll()
        {
            return _exampleQuery.GetAll();
        }
    }
}
