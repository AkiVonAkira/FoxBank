﻿namespace FoxBank
{
    internal class BankLoanModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string interest_rate { get; set; }

        public int user_id { get; set; }
    }
}