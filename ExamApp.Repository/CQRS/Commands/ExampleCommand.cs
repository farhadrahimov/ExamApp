
namespace ExamApp.Repository.CQRS.Commands
{
    public interface IExampleCommand
    {
        bool Add(int number);
    }

    public class ExampleCommand : IExampleCommand
    {
        public bool Add(int number)
        {
            if (number % 1 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
