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
    public interface IClassService
    {
        Task<IEnumerable<Classes>> GetAllAsync();
    }

    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClassService(IClassRepository classRepository, IUnitOfWork unitOfWork)
        {
            _classRepository = classRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Classes>> GetAllAsync()
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _classRepository.GetAllAsync();
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
