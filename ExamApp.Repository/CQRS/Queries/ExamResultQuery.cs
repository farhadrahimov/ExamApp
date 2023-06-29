using Dapper;
using ExamApp.Core.DTO;
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
    public interface IExamResultQuery
    {
        Task<IEnumerable<ExamResults>> GetAllAsync();
        Task<ExamResults> GetByIdAsync(Guid id);
        Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel);
    }

    public class ExamResultQuery : IExamResultQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string GetAllQuery = @"SELECT 
                                                       ER.[Id]
                                                      ,ER.[SubjectId]
													  ,Sub.Name SubjectName
                                                      ,ER.[StudentId]
													  ,St.Name StudentName
                                                      ,ER.[Date]
                                                      ,ER.[Grade]
                                                  FROM [ExamDB].[dbo].[ExamResults] AS ER
                                                  LEFT JOIN Subjects AS Sub ON ER.SubjectId=Sub.Id
												  LEFT JOIN Students AS St ON ER.StudentId=St.Id WHERE ER.[DeleteStatus] = 0";

        private readonly string GetByIdQuery = @"SELECT 
                                                       ER.[Id]
                                                      ,ER.[SubjectId]
													  ,Sub.Name SubjectName
                                                      ,ER.[StudentId]
													  ,St.Name StudentName
                                                      ,ER.[Date]
                                                      ,ER.[Grade]
                                                  FROM [ExamDB].[dbo].[ExamResults] AS ER
                                                  LEFT JOIN Subjects AS Sub ON ER.SubjectId=Sub.Id
												  LEFT JOIN Students AS St ON ER.StudentId=St.Id WHERE ER.[Id] = @id";

        private readonly string GetPaginationQuery = @"SELECT 
                                                       ER.[Id]
                                                      ,ER.[SubjectId]
													  ,Sub.Name SubjectName
                                                      ,ER.[StudentId]
													  ,St.Name StudentName
                                                      ,ER.[Date]
                                                      ,ER.[Grade]
                                                  FROM [ExamDB].[dbo].[ExamResults] AS ER
                                                  LEFT JOIN Subjects AS Sub ON ER.SubjectId=Sub.Id
												  LEFT JOIN Students AS St ON ER.StudentId=St.Id WHERE ER.[DeleteStatus] = 0
												  ORDER BY ER.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY

                                                  SELECT COUNT([Id]) AS TotalCount FROM [ExamDB].[dbo].[ExamResults] WHERE DeleteStatus = 0";

        private string GetFullSearchQuery = @"DECLARE @SEARCHTEXT NVARCHAR(MAX) = '%' + {0} + '%'
                                              DECLARE @SearchGrade INT = @Graduate
                                              DECLARE @SearchFirstDate Date = @StartDate
                                              DECLARE @SearchSecondDate Date = @EndDate
                                                    SELECT
                                                    ER.[Id]
                                                    ,ER.[SubjectId]
													,Sub.Name SubjectName
                                                    ,ER.[StudentId]
													,St.Name StudentName
                                                    ,ER.[Date]
                                                    ,ER.[Grade]
	                                                FROM [ExamDB].[dbo].[ExamResults] AS ER
                                                    LEFT JOIN Subjects AS Sub ON ER.SubjectId=Sub.Id
	                                                LEFT JOIN Students AS St ON ER.StudentId=St.Id 
													WHERE 
													ER.[DeleteStatus] = 0 AND
													ER.[Grade] = @SearchGrade AND
													(ER.[Date] >= @SearchFirstDate AND ER.[Date] <= @SearchSecondDate) AND
                                                    (Sub.[Name] LIKE @SEARCHTEXT OR St.[Name] LIKE @SEARCHTEXT)
	                                                ORDER BY ER.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY

                                                    SELECT
                                                    COUNT(ER.[Id]) AS TotalCount
	                                                FROM [ExamDB].[dbo].[ExamResults] AS ER
                                                    LEFT JOIN Subjects AS Sub ON ER.SubjectId=Sub.Id
	                                                LEFT JOIN Students AS St ON ER.StudentId=St.Id 
													WHERE 
													ER.[DeleteStatus] = 0 AND
													ER.[Grade] = @SearchGrade AND
													(ER.[Date] >= @SearchFirstDate AND ER.[Date] <= @SearchSecondDate) AND
                                                    (Sub.[Name] LIKE @SEARCHTEXT OR St.[Name] LIKE @SEARCHTEXT)";

        public ExamResultQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExamResults>> GetAllAsync()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<ExamResults>(GetAllQuery, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ExamResults> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<ExamResults>(GetByIdQuery, new { id }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel)
        {
            try
            {
                var searchStr = string.Format(GetFullSearchQuery, $"N'{requestModel.Search}'");
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(searchStr, requestModel, _unitOfWork.GetTransaction());
                var result = new ListResult<ExamResults>
                {
                    List = data.Read<ExamResults>(),
                    TotalCount = data.ReadFirst<int>()
                };
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit)
        {
            try
            {
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(GetPaginationQuery, new { offset, limit }, _unitOfWork.GetTransaction());
                var result = new ListResult<ExamResults>
                {
                    List = data.Read<ExamResults>(),
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
