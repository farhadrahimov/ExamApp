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
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(Guid id);
        Task<ListResult<Subject>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<Subject>> GetFullSearchAsync(int offset, int limit, string search);
        Task<Guid> AddAsync(Subject item);
    }

    public class SubjectRepository : ISubjectRepository
    {
        private readonly ISubjectCommand _subjectCommand;
        private readonly ISubjectQuery _subjectQuery;

        public SubjectRepository(ISubjectCommand subjectCommand, ISubjectQuery subjectQuery)
        {
            _subjectCommand = subjectCommand;
            _subjectQuery = subjectQuery;
        }

        public async Task<Guid> AddAsync(Subject item)
        {
            var result = await _subjectCommand.AddAsync(item);
            return result;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var result = await _subjectQuery.GetAllAsync();
            return result;
        }

        public async Task<Subject> GetByIdAsync(Guid id)
        {
            var result = await _subjectQuery.GetByIdAsync(id);
            return result;
        }

        public async Task<ListResult<Subject>> GetFullSearchAsync(int offset, int limit, string search)
        {
            var result = await _subjectQuery.GetFullSearchAsync(offset, limit, search);
            return result;
        }

        public async Task<ListResult<Subject>> GetPaginationAsync(int offset, int limit)
        {
            var result = await _subjectQuery.GetPaginationAsync(offset, limit);
            return result;
        }
    }
}
