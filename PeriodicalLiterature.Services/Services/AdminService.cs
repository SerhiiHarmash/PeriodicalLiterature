using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;

namespace PeriodicalLiterature.Services.Services
{
    public class AdminService :IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAdmin(Admin admin)
        {
            _unitOfWork.GetRepository<Admin>().Add(admin);
            _unitOfWork.Save();
        }
    }
}
