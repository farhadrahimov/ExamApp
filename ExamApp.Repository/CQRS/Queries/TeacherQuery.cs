using Dapper;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.CQRS.Queries
{
    public interface ITeacherQuery
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
    }

    public class TeacherQuery : ITeacherQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string GetAllSql = @"SELECT  [Id]
                                                  ,[Number]
                                                  ,[Name]
                                                  ,[SureName]
                                              FROM [ExamDB].[dbo].[Teachers] WHERE [DeleteStatus] = 0";

        public TeacherQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Teachers>(GetAllSql,null,_unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
