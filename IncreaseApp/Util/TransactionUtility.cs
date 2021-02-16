using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IncreaseApp.ViewModels.Incoming;

namespace IncreaseApp.Util
{
    public static class TransactionUtility
    {
        public static FileVM TransactionBatchSplitter(string file)
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

        public static FileVM ProcessTheFile(List<List<string>> File)
        {
            var result = new FileVM();
            
            foreach (var document in File)
            {
                result.Transactions.Add(new TransactionVM());

                foreach (var line in document)
                {
                    switch (line[0])
                    {
                        case '1':
                            result.Transactions[^1].Header = HeaderVM.StringToHeaderVm(line);
                            break;
                        case '2':
                            result.Transactions[^1].Details.Add(TransactionDetailVM.StringToTransactionDetailVM(line));
                            break;
                        case '3':
                            result.Transactions[^1].Discounts.Add(DiscountVM.StringToDiscountVM(line));
                            break;
                        case '4':
                            result.Transactions[^1].Footer = FooterVM.StringToFooterVM(line);
                            break;
                    }
                }
            }
            
            return result;
        }
    }
}