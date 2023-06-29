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
    public interface ISubjectService
    {
        Task<IEnumerable<Subjects>> GetAllAsync();
        Task<Subjects> GetByIdAsync(Guid id);
        Task<ListResult<Subjects>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<Subjects>> GetFullSearchAsync(int offset, int limit, string search);
        Task<Guid> AddAsync(Subjects item);
    }
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork)
        {
            _subjectRepository = subjectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(Subjects item)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _subjectRepository.AddAsync(item);
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

        public async Task<IEnumerable<Subjects>> GetAllAsync()
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _subjectRepository.GetAllAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<Subjects> GetByIdAsync(Guid id)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _subjectRepository.GetByIdAsync(id);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<Subjects>> GetFullSearchAsync(int offset, int limit, string search)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _subjectRepository.GetFullSearchAsync(offset, limit, search);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<Subjects>> GetPaginationAsync(int offset, int limit)
        {
            await using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _subjectRepository.GetPaginationAsync(offset, limit);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
