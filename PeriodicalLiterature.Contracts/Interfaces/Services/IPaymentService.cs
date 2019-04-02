using PeriodicalLiterature.Models.Entities;
using System;

namespace PeriodicalLiterature.Contracts.Interfaces.Services
{
    public interface IPaymentService
    {
        void PayContractByCard(Guid subscriptionId, Card card, decimal sum);

        bool ConfirmPayContractByCard(Guid subscriptionId, string confirmationCode);
    }
}
