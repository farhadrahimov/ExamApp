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
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(Guid id);
        Task<ListResult<Student>> GetPagination(int offset, int limit);
        Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search);
        Task<Guid> Add(Student item);
        Task<Guid> Update(Student item);
        Task<bool> Delete(Guid id);
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

        public async Task<Guid> Add(Student item)
        {
            var result = await _studentCommand.Add(item);
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _studentCommand.Delete(id);
            return result;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var result = await _studentQuery.GetAll();
            return result;
        }

        public async Task<Student> GetById(Guid id)
        {
            var result = await _studentQuery.GetById(id);
            return result;
        }

        public async Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search)
        {
            var result = await _studentQuery.GetFullSearch(offset, limit, search);
            return result;
        }

        public async Task<ListResult<Student>> GetPagination(int offset, int limit)
        {
            var result = await _studentQuery.GetPagination(offset, limit);
            return result;
        }

        public async Task<Guid> Update(Student item)
        {
            var result = await _studentCommand.Update(item);
            return result;
        }
    }
}
