using ExamApp.Core.DTO;
using ExamApp.Core.Helpers;
using ExamApp.Core.Models;
using ExamApp.Repository.CQRS.Commands;
using ExamApp.Repository.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Repository.Repositories
{
    public interface IExamResultRepository
    {
        Task<Guid> AddAsync(ExamResults item);
        Task<IEnumerable<ExamResults>> GetAllAsync();
        Task<ExamResults> GetByIdAsync(Guid id);
        Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel);
    }

    public class ExamResultRepository : IExamResultRepository
    {
        private readonly IExamResultQuery _examResultQuery;
        private readonly IExamResultCommand _examResultCommand;

        public ExamResultRepository(IExamResultQuery examResultQuery, IExamResultCommand examResultCommand)
        {
            _examResultQuery = examResultQuery;
            _examResultCommand = examResultCommand;
        }

        public async Task<Guid> AddAsync(ExamResults item)
        {
            var result = await _examResultCommand.AddAsync(item);
            return result;
        }

        public async Task<IEnumerable<ExamResults>> GetAllAsync()
        {
            var result = await _examResultQuery.GetAllAsync();
            return result;
        }

        public async Task<ExamResults> GetByIdAsync(Guid id)
        {
            var result = await _examResultQuery.GetByIdAsync(id);
            return result;
        }

        public async Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel)
        {
            var result = await _examResultQuery.GetFullSearchAsync(requestModel);
            return result;
        }

        public async Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit)
        {
            var result = await _examResultQuery.GetPaginationAsync(offset, limit);
            return result;
        }
    }
}
