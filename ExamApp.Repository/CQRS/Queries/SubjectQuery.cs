using Dapper;
using ExamApp.Core.Helpers;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.CQRS.Queries
{
    public interface ISubjectQuery
    {
        Task<IEnumerable<Subjects>> GetAllAsync();
        Task<Subjects> GetByIdAsync(Guid id);
        Task<ListResult<Subjects>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<Subjects>> GetFullSearchAsync(int offset, int limit, string search);
    }
    public class SubjectQuery : ISubjectQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string GetAllQuery = @"SELECT S.[Id]
                                                      ,S.[Code]
                                                      ,S.[Name]
                                                      ,S.[Class]
                                                      ,S.[TeacherId]
	                                                  ,T.[Name] TeacherName
                                                  FROM [ExamDB].[dbo].[Subjects] AS S
                                                  LEFT JOIN Teachers AS T ON S.TeacherId=T.Id WHERE S.[DeleteStatus] = 0";

        private readonly string GetByIdQuery = @"SELECT S.[Id]
                                                      ,S.[Code]
                                                      ,S.[Name]
                                                      ,S.[Class]
                                                      ,S.[TeacherId]
	                                                  ,T.[Name] TeacherName
                                                  FROM [ExamDB].[dbo].[Subjects] AS S
                                                  LEFT JOIN Teachers AS T ON S.TeacherId=T.Id WHERE S.[Id] = @id";

        private readonly string GetPaginationQuery = @"SELECT S.[Id]
                                                      ,S.[Code]
                                                      ,S.[Name]
                                                      ,S.[Class]
                                                      ,S.[TeacherId]
	                                                  ,T.[Name] TeacherName
                                                  FROM [ExamDB].[dbo].[Subjects] AS S
                                                  LEFT JOIN Teachers AS T ON S.TeacherId=T.Id WHERE S.[DeleteStatus] = 0
												  ORDER BY S.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY

                                                  SELECT COUNT([Id]) AS TotalCount FROM [ExamDB].[dbo].[Subjects] WHERE DeleteStatus = 0";

        private string GetFullSearchQuery = @"DECLARE @SEARCHTEXT NVARCHAR(MAX) = '%' + {0} + '%' 
                                                       SELECT
                                                       S.[Id]
		                                              ,S.[Code]
                                                      ,S.[Name]
                                                      ,S.[Class]
                                                      ,S.[TeacherId]
	                                                  ,T.[Name] TeacherName
	                                                FROM [ExamDB].[dbo].[Subjects] AS S
	                                                LEFT JOIN Teachers AS T ON S.TeacherId=T.Id WHERE S.[DeleteStatus] = 0 AND 
                                                    (S.[Name] LIKE @SEARCHTEXT OR S.[Code] LIKE @SEARCHTEXT OR T.[Name] LIKE @SEARCHTEXT)
	                                                ORDER BY S.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY

                                                    SELECT COUNT(S.[Id]) AS TotalCount
	                                                FROM [ExamDB].[dbo].[Subjects] AS S
	                                                LEFT JOIN Teachers AS T ON S.TeacherId=T.Id WHERE S.[DeleteStatus] = 0 AND 
                                                    (S.[Name] LIKE @SEARCHTEXT OR S.[Code] LIKE @SEARCHTEXT OR T.[Name] LIKE @SEARCHTEXT)";

        public SubjectQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Subjects>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Subjects>(GetAllQuery, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Subjects> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Subjects>(GetByIdQuery, new { id }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<Subjects>> GetFullSearchAsync(int offset, int limit, string search)
        {
            try
            {
                var searchStr = string.Format(GetFullSearchQuery, $"N'{search}'");
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(searchStr, new { offset, limit }, _unitOfWork.GetTransaction());
                var result = new ListResult<Subjects>
                {
                    List = data.Read<Subjects>(),
                    TotalCount = data.ReadFirst<int>()
                };
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<Subjects>> GetPaginationAsync(int offset, int limit)
        {
            try
            {
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(GetPaginationQuery, new { offset, limit }, _unitOfWork.GetTransaction());
                var result = new ListResult<Subjects>
                {
                    List = data.Read<Subjects>(),
                    TotalCount = data.ReadFirst<int>()
                };
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
