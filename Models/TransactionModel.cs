﻿namespace FoxBank.Models
{
    internal class TransactionModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public DateTime timestamp { get; set; }
        public decimal amount { get; set; }

        public string GetSignedAmount(int account_id)
        {

            if (account_id == from_account_id)
            {
                return $"-{amount}";
            }
            return amount.ToString();
        }

    }
}
