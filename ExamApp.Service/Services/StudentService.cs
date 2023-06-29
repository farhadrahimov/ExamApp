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
        Task<IEnumerable<Students>> GetAllAsync();
        Task<Students> GetByIdAsync(Guid id);
        Task<ListResult<Students>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<Students>> GetFullSearchAsync(int offset, int limit, string search);
        Task<Guid> AddAsync(Students item);
        Task<Guid> UpdateAsync(Students item);
        Task<bool> DeleteAsync(Guid id);
    }
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(Students item)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.AddAsync(item);
                    tran.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.DeleteAsync(id);
                    tran.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<Students>> GetAllAsync()
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.GetAllAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<Students> GetByIdAsync(Guid id)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.GetByIdAsync(id);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<Students>> GetFullSearchAsync(int offset, int limit, string search)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.GetFullSearchAsync(offset, limit, search);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<Students>> GetPaginationAsync(int offset, int limit)
        {
            await using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.GetPaginationAsync(offset, limit);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<Guid> UpdateAsync(Students item)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _studentRepository.UpdateAsync(item);
                    tran.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
        }
    }
}
