using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ExamApp.Repository.CQRS.Commands
{
    public interface IStudentCommand
    {
        Task<Guid> AddAsync(Students item);
        Task<Guid> UpdateAsync(Students item);
        Task<bool> DeleteAsync(Guid id);
    }
    public class StudentCommand : IStudentCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Queries
        private const string AddQuery = $@"INSERT INTO [dbo].[Students]
                                                                ([Number]
                                                               ,[Name]
                                                               ,[SureName]
                                                               ,[ClassId])
                                                         OUTPUT INSERTED.ID
                                                         VALUES
                                                               (@{nameof(Students.Number)}
                                                              ,@{nameof(Students.Name)}
                                                              ,@{nameof(Students.SureName)}
                                                              ,@{nameof(Students.ClassId)})";

        private const string UpdateQuery = $@"UPDATE [dbo].[Students] SET
                                                               [Number] = @{nameof(Students.Number)}
                                                              ,[Name] = @{nameof(Students.Name)}
                                                              ,[SureName] = @{nameof(Students.SureName)}
                                                              ,[ClassId] = @{nameof(Students.ClassId)}
                                                               WHERE [Id] = @{nameof(Students.Id)}";

        private const string DeleteQuery = $@"UPDATE [dbo].[Students] SET
                                                              [DeleteStatus] = 1
                                                               WHERE [Id] = @{nameof(Students.Id)}";
        #endregion

        public StudentCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(Students item)
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

        public async Task<Guid> UpdateAsync(Students item)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync(UpdateQuery, item, _unitOfWork.GetTransaction());
                return item.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().ExecuteAsync(DeleteQuery, new { id }, _unitOfWork.GetTransaction());
                return result != 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
