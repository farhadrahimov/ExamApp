using Dapper;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.CQRS.Commands
{
    public interface ISubjectCommand
    {
        Task<Guid> AddAsync(Subjects item);
    }
    public class SubjectCommand : ISubjectCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string AddQuery = $@"INSERT INTO [dbo].[Subjects]
                                                                ([Code]
                                                               ,[Name]
                                                               ,[Class]
                                                               ,[TeacherId])
                                                         OUTPUT INSERTED.ID
                                                         VALUES
                                                               (@{nameof(Subjects.Code)}
                                                              ,@{nameof(Subjects.Name)}
                                                              ,@{nameof(Subjects.Class)}
                                                              ,@{nameof(Subjects.TeacherId)})";
        public async Task<Guid> AddAsync(Subjects item)
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
