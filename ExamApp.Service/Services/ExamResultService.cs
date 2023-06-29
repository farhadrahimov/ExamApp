using ExamApp.Core.DTO;
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
    public interface IExamResultService
    {
        Task<Guid> AddAsync(ExamResults item);
        Task<IEnumerable<ExamResults>> GetAllAsync();
        Task<ExamResults> GetByIdAsync(Guid id);
        Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit);
        Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel);
    }
    public class ExamResultService : IExamResultService
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExamResultService(IExamResultRepository examResultRepository, IUnitOfWork unitOfWork)
        {
            _examResultRepository = examResultRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(ExamResults item)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _examResultRepository.AddAsync(item);
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

        public async Task<IEnumerable<ExamResults>> GetAllAsync()
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _examResultRepository.GetAllAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ExamResults> GetByIdAsync(Guid id)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _examResultRepository.GetByIdAsync(id);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<ExamResults>> GetFullSearchAsync(ExamResultsRequestModel requestModel)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _examResultRepository.GetFullSearchAsync(requestModel);
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<ListResult<ExamResults>> GetPaginationAsync(int offset, int limit)
        {
            await using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _examResultRepository.GetPaginationAsync(offset, limit);
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
