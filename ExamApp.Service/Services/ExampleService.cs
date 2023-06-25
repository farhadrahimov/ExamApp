using ExamApp.Core.Models;
using ExamApp.Repository.Repositories;

namespace ExamApp.Service.Services
{
    public interface IExampleService
    {
        ExampleModel GetAll();
        bool Add(int number);
    }

    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public bool Add(int number)
        {
            return _exampleRepository.Add(number);
        }

        public ExampleModel GetAll()
        {
            return _exampleRepository.GetAll();
        }
    }
}
