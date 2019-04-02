using PeriodicalLiterature.Contracts.Interfaces.DAL;
using PeriodicalLiterature.Models.Entities;
using System;
using PeriodicalLiterature.Contracts.Interfaces.Services;
using PeriodicalLiterature.Models.Enums;

namespace PeriodicalLiterature.Services.Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PayContractByCard(Guid subscriptionId, Card card, decimal sum)
        {
            string callBackCode = PayByCard(card, sum);

            var paymentConfirmationCode = new PaymentConfirmationCode()
            {
                Id = Guid.NewGuid(),
                CallBackCode = callBackCode,
                Date = DateTime.UtcNow,
                SubscriptionId = subscriptionId,
                CardNumber = card.CardNumber
            };

            _unitOfWork.GetRepository<PaymentConfirmationCode>().Add(paymentConfirmationCode);
            _unitOfWork.Save();
        }

        public bool ConfirmPayContractByCard(Guid subscriptionId, string confirmationCode)
        {
            var paymentConfirmation = _unitOfWork.GetRepository<PaymentConfirmationCode>()
                .GetSingle(x => x.SubscriptionId == subscriptionId);

            var subscription = _unitOfWork.GetRepository<Subscription>().GetSingle(x => x.Id == subscriptionId);

            //var banckResult = BanckAPI.ApproveTranzaction(paymentConfirmationCode.CallBackCode, confirmationCode);

            var bankTransaction = new BankTransaction()
            {
                Date = DateTime.UtcNow,
                From = paymentConfirmation.CardNumber,
                Id = Guid.NewGuid(),
                Sum = subscription.Price,
                To = "CompanyAccount",
                Type = BankTransactionType.Payment,
                UserId = subscription.SubscriberId
            };

            _unitOfWork.GetRepository<BankTransaction>().Add(bankTransaction);
            _unitOfWork.Save();

            return true;
        }


        private string PayByCard(Card card, decimal sum)
        {
            //string companyAccount = "0437489623640";
            //string callBackCode =  BanckAPI.Tranzaction(
            //    card.CVV,
            //    card.CardNumber,
            //    card.CardHolderName, 
            //    card.DateOfExpiry,
            //    companyAccount,
            //    sum);

            string callBackCode = "013345";

            return callBackCode;
        }
    }
}
