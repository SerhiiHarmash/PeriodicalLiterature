using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Entities;
using System;
using AutoMapper;

namespace PeriodicalLiterature.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAdmin(Admin admin)
        {
            if (_unitOfWork.GetRepository<Admin>().Any(x => x.Id == admin.Id))
            {
                throw new Exception($"Admin with id:{admin.Id} already exist");
            }

            _unitOfWork.GetRepository<Admin>().Add(admin);

            _unitOfWork.Save();
        }

        public Admin GetAdmin(Guid adminId)
        {
            var admin = _unitOfWork.GetRepository<Admin>().GetSingle(x => x.Id == adminId);

            return admin;
        }

        public void EditAdmin(Admin admin)
        {
            var adminEntity = _unitOfWork.GetRepository<Admin>().GetSingle(x => x.Id == admin.Id);

            if (adminEntity == null)
            {
                throw new Exception($"Admin with id:{admin.Id} doesn't exist");
            }

            Mapper.Map(admin, adminEntity);


            _unitOfWork.GetRepository<Admin>().Update(adminEntity);

            _unitOfWork.Save();
        }
    }
}
