using Dapper;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;

namespace ExamApp.Repository.CQRS.Commands
{
    public interface IExamResultCommand
    {
        Task<Guid> AddAsync(ExamResult item);
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
                                                               (@{nameof(ExamResult.SubjectId)}
                                                              ,@{nameof(ExamResult.StudentId)}
                                                              ,@{nameof(ExamResult.Date)}
                                                              ,@{nameof(ExamResult.Grade)})";

        public ExamResultCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(ExamResult item)
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
