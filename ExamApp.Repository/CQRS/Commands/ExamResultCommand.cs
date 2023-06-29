using Dapper;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;

namespace ExamApp.Repository.CQRS.Commands
{
    public interface IExamResultCommand
    {
        Task<Guid> AddAsync(ExamResults item);
    }
    public class ExamResultCommand : IExamResultCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string AddQuery = $@"INSERT INTO [dbo].[ExamResults]
                                                                ([SubjectId]
                                                               ,[StudentId]
                                                               ,[Date]
                                                               ,[Grade])
                                                         OUTPUT INSERTED.ID
                                                         VALUES
                                                               (@{nameof(ExamResults.SubjectId)}
                                                              ,@{nameof(ExamResults.StudentId)}
                                                              ,@{nameof(ExamResults.Date)}
                                                              ,@{nameof(ExamResults.Grade)})";

        public ExamResultCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(ExamResults item)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QuerySingleAsync<Guid>(AddQuery, item, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
