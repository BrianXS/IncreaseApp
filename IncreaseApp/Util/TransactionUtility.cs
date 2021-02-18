using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IncreaseApp.ViewModels.Incoming;

namespace IncreaseApp.Util
{
    public static class TransactionUtility
    {
        public static FileVm TransactionBatchSplitter(string file)
        {
            var splittedTransactionBatches = new List<List<string>>();
            var splittedString = file.Split(new string[] {"\n"}, StringSplitOptions.None);

            foreach (var singleLine in splittedString)
            {
                if (singleLine.Length == 78)
                {
                    splittedTransactionBatches.Add(new List<string>());
                    splittedTransactionBatches[^1].Add(singleLine);
                }

                if (singleLine.Length == 52 || singleLine.Length == 50 || singleLine.Length == 56)
                {
                    splittedTransactionBatches[^1].Add(singleLine);
                }
            }

            return ProcessTheFile(splittedTransactionBatches);
        }

        public static FileVm ProcessTheFile(List<List<string>> File)
        {
            var result = new FileVm();
            
            foreach (var document in File)
            {
                result.Transactions.Add(new TransactionVm());

                foreach (var line in document)
                {
                    switch (line[0])
                    {
                        case '1':
                            result.Transactions[^1].Header = HeaderVm.StringToHeaderVm(line);
                            break;
                        case '2':
                            result.Transactions[^1].Details.Add(TransactionDetailVm.StringToTransactionDetailVm(line));
                            break;
                        case '3':
                            result.Transactions[^1].Discounts.Add(TransactionDiscountVm.StringToDiscountVm(line));
                            break;
                        case '4':
                            result.Transactions[^1].Footer = FooterVm.StringToFooterVm(line);
                            break;
                    }
                }
            }
            
            return result;
        }
    }
}