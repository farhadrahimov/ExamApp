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
    public interface IStudentRepository
    {
        Task<IEnumerable<Students>> GetAllAsync();
        Task<Students> GetByIdAsync(Guid id);
        Task<ListResult<Students>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<Students>> GetFullSearchAsync(int offset, int limit, string search);
        Task<Guid> AddAsync(Students item);
        Task<Guid> UpdateAsync(Students item);
        Task<bool> DeleteAsync(Guid id);
    }
    public class StudentRepository : IStudentRepository
    {
        private readonly IStudentCommand _studentCommand;
        private readonly IStudentQuery _studentQuery;

        public StudentRepository(IStudentCommand studentCommand, IStudentQuery studentQuery)
        {
            _studentCommand = studentCommand;
            _studentQuery = studentQuery;
        }

        public async Task<Guid> AddAsync(Students item)
        {
            var result = await _studentCommand.AddAsync(item);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _studentCommand.DeleteAsync(id);
            return result;
        }

        public async Task<IEnumerable<Students>> GetAllAsync()
        {
            var result = await _studentQuery.GetAllAsync();
            return result;
        }

        public async Task<Students> GetByIdAsync(Guid id)
        {
            var result = await _studentQuery.GetByIdAsync(id);
            return result;
        }

        public async Task<ListResult<Students>> GetFullSearchAsync(int offset, int limit, string search)
        {
            var result = await _studentQuery.GetFullSearchAsync(offset, limit, search);
            return result;
        }

        public async Task<ListResult<Students>> GetPaginationAsync(int offset, int limit)
        {
            var result = await _studentQuery.GetPaginationAsync(offset, limit);
            return result;
        }

        public async Task<Guid> UpdateAsync(Students item)
        {
            var result = await _studentCommand.UpdateAsync(item);
            return result;
        }
    }
}
