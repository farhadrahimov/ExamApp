﻿using Dapper;
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
    public interface IStudentQuery
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(Guid id);
        Task<ListResult<Student>> GetPagination(int offset, int limit);
        Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search);
    }
    public class StudentQuery : IStudentQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string GetAllQuery = @"SELECT S.[Id]
                                                      ,S.[Number]
                                                      ,S.[Name]
                                                      ,S.[SureName]
                                                      ,S.[ClassId]
	                                                  ,C.[Name] ClassName
                                                  FROM [ExamDB].[dbo].[Students] AS S
                                                  LEFT JOIN Classes AS C ON S.ClassId=C.Id WHERE S.[DeleteStatus] = 0";

        private readonly string GetByIdQuery = @"SELECT S.[Id]
                                                      ,S.[Number]
                                                      ,S.[Name]
                                                      ,S.[SureName]
                                                      ,S.[ClassId]
	                                                  ,C.[Name] ClassName
                                                  FROM [ExamDB].[dbo].[Students] AS S
                                                  LEFT JOIN Classes AS C ON S.ClassId=C.Id WHERE S.[Id] = @id";

        private readonly string GetPaginationQuery = @"SELECT   S.[Id]
		                                                ,S.[Number]
		                                                ,S.[Name]
		                                                ,S.[SureName]
		                                                ,S.[ClassId]
		                                                ,C.[Name] ClassName
	                                                FROM [ExamDB].[dbo].[Students] AS S
	                                                LEFT JOIN Classes AS C ON S.ClassId=C.Id WHERE S.[DeleteStatus] = 0
	                                                ORDER BY S.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY";

        private readonly string GetFullSearchQuery = @"DECLARE @SEARCHTEXT NVARCHAR(MAX) = '%' + @SEARCH + '%'
                                                SELECT   S.[Id]
		                                                ,S.[Number]
		                                                ,S.[Name]
		                                                ,S.[SureName]
		                                                ,S.[ClassId]
		                                                ,C.[Name] ClassName
	                                                FROM [ExamDB].[dbo].[Students] AS S
	                                                LEFT JOIN Classes AS C ON S.ClassId=C.Id WHERE S.[DeleteStatus] = 0 AND 
                                                    (S.[Name] LIKE @SEARCHTEXT OR S.[SureName] LIKE @SEARCHTEXT OR C.[Name] LIKE @SEARCHTEXT)
	                                                ORDER BY S.[RowNum] DESC OFFSET @OFFSET ROWS FETCH NEXT @LIMIT ROWS ONLY";

        public StudentQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryAsync<Student>(GetAllQuery, null, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Student> GetById(Guid id)
        {
            try
            {
                var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Student>(GetByIdQuery, new { id }, _unitOfWork.GetTransaction());
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search)
        {
            try
            {
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(GetFullSearchQuery, new { offset, limit, search }, _unitOfWork.GetTransaction());
                var result = new ListResult<Student>
                {
                    List = data.Read<Student>(),
                    TotalCount = data.ReadFirst<int>()
                };
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ListResult<Student>> GetPagination(int offset, int limit)
        {
            try
            {
                var data = await _unitOfWork.GetConnection().QueryMultipleAsync(GetAllQuery, new { offset, limit }, _unitOfWork.GetTransaction());
                var result = new ListResult<Student>
                {
                    List = data.Read<Student>(),
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