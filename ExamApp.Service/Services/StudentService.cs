using ExamApp.Core.Helpers;
using ExamApp.Core.Models;
using ExamApp.Repository.Infrastructure;
using ExamApp.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Service.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(Guid id);
        Task<ListResult<Student>> GetPagination(int offset, int limit);
        Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search);
        Task<Guid> Add(Student item);
        Task<Guid> Update(Student item);
        Task<bool> Delete(Guid id);
    }
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Add(Student item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResult<Student>> GetFullSearch(int offset, int limit, string search)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResult<Student>> GetPagination(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Update(Student item)
        {
            throw new NotImplementedException();
        }
    }
}
