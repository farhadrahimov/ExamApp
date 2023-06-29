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
    public interface IClassQuery
    {
        Task<IEnumerable<Classes>> GetAllAsync();
    }

    public class ClassQuery : IClassQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string GetAllSql = @"SELECT  [Id]
                                                  ,[Code]
                                                  ,[Name]
                                              FROM [ExamDB].[dbo].[Classes] WHERE [DeleteStatus] = 0";

        public ClassQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Classes>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Classes>(GetAllSql, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
