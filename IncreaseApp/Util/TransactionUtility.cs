using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IncreaseApp.Util
{
    public static class TransactionUtility
    {
        public static List<List<string>> TransactionBatchSplitter(string file)
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

            return splittedTransactionBatches;
        }
    }
}