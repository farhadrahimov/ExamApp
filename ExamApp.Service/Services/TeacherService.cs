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
    public interface ITeacherService
    {
        Task<IEnumerable<Teachers>> GetAllAsync();
    }

    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Teachers>> GetAllAsync()
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var result = await _teacherRepository.GetAllAsync();
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
