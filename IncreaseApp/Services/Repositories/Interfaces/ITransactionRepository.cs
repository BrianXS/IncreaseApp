using System;
using System.Collections.Generic;
using IncreaseApp.Entities;
using IncreaseApp.ViewModels.Incoming;

namespace IncreaseApp.Services.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        bool DoesTransactionExist(Guid id);
        bool DoesTransactionDetailExist(Guid id);
        bool DoesTransactionDiscountExist(Guid id);
        
        Transaction FindTransactionById(Guid transactionId);
        TransactionDetail FindTransactionDetailById(Guid transactionDetailId);
        TransactionDiscount FindTransactionDiscountById(Guid transactionDiscountId);

        void SaveTransaction(HeaderVm header, DateTime date, Guid customerId);
        void SaveTransactionDetail(TransactionDetailVm detail, Guid transactionId);
        void SaveTransactionDiscount(TransactionDiscountVm discount, Guid transactionId);

        void SaveAllDetails(List<TransactionDetailVm> details, Guid transactionId);
        void SaveAllDiscounts(List<TransactionDiscountVm> discounts, Guid transactionId);
    }
}