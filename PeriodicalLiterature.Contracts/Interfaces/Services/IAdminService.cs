using PeriodicalLiterature.Models.Entities;
using System;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IAdminService
    {
        void AddAdmin(Admin admin);

        Admin GetAdmin(Guid adminId);

        void EditAdmin(Admin admin);
    }
}
