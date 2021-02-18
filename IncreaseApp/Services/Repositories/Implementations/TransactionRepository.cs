using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IncreaseApp.Entities;
using IncreaseApp.Services.Database;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.ViewModels.Incoming;

namespace IncreaseApp.Services.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IncreaseDbContext _dbContext;
        private readonly IMapper _mapper;

        public TransactionRepository(IncreaseDbContext dbContext, 
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool DoesTransactionExist(Guid id)
        {
            return _dbContext.Transactions.Any(x => x.Id == id);
        }

        public bool DoesTransactionDetailExist(Guid id)
        {
            return _dbContext.Details.Any(x => x.Id == id);
        }

        public bool DoesTransactionDiscountExist(Guid id)
        {
            return _dbContext.Discounts.Any(x => x.Id == id);
        }

        public Transaction FindTransactionById(Guid transactionId)
        {
            return _dbContext.Transactions.FirstOrDefault(x => x.Id == transactionId);
        }

        public TransactionDetail FindTransactionDetailById(Guid transactionDetailId)
        {
            return _dbContext.Details.FirstOrDefault(x => x.Id == transactionDetailId);
        }

        public TransactionDiscount FindTransactionDiscountById(Guid transactionDiscountId)
        {
            return _dbContext.Discounts.FirstOrDefault(x => x.Id == transactionDiscountId);
        }

        public void SaveTransaction(HeaderVm header, DateTime date, Guid customerId)
        {
            if (!DoesTransactionExist(header.Id))
            {
                var transaction = _mapper.Map<Transaction>(header);
                transaction.CustomerId = customerId;
                transaction.Date = date;

                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
            }
        }

        public void SaveTransactionDetail(TransactionDetailVm detail, Guid transactionId)
        {
            if (!DoesTransactionDetailExist(detail.Id))
            {
                var transactionDetail = _mapper.Map<TransactionDetail>(detail);
                transactionDetail.TransactionId = transactionId;

                _dbContext.Details.Add(transactionDetail);
                _dbContext.SaveChanges();
            }
        }

        public void SaveTransactionDiscount(TransactionDiscountVm discount, Guid transactionId)
        {
            if (!DoesTransactionDiscountExist(discount.Id))
            {
                var transactionDiscount = _mapper.Map<TransactionDiscount>(discount);
                transactionDiscount.TransactionId = transactionId;

                _dbContext.Discounts.Add(transactionDiscount);
                _dbContext.SaveChanges();
            }
        }

        public void SaveAllDetails(List<TransactionDetailVm> details, Guid transactionId)
        {
            foreach (var detail in details)
            {
                SaveTransactionDetail(detail, transactionId);
            }
        }

        public void SaveAllDiscounts(List<TransactionDiscountVm> discounts, Guid transactionId)
        {
            foreach (var discount in discounts)
            {
                SaveTransactionDiscount(discount, transactionId);
            }
        }
    }
}