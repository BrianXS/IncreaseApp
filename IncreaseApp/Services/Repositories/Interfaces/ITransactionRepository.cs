using System;

namespace IncreaseApp.Services.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void DoesTransactionExist(Guid id);
        void DoesTransactionDetailExist(Guid id);
        void DoesTransactionDiscountExist(Guid id);
    }
}