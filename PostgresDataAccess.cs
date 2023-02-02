using Dapper;
using FoxBank;
using Npgsql;
using System.Configuration;
using System.Data;

public class PostgresDataAccess
{
    private static string LoadConnectionString(string id = "fox_test")
    {
        return ConfigurationManager.ConnectionStrings[id].ConnectionString;
    }

    internal static List<UserModel> LoadUserModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<UserModel>("SELECT * FROM bank_user", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<AccountModel> LoadAccountModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<AccountModel>("SELECT * FROM bank_account", new DynamicParameters());
            return output.ToList();
        }
    }


    internal static List<AccountModel> LoadUserAccount(int userID)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            {
                var output = cnn.Query<AccountModel>($@"
                    SELECT * FROM bank_account WHERE user_id={userID}", new DynamicParameters());
                return output.ToList();
            }
        }
    }

    internal static List<TransactionModel> LoadTransactionModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<TransactionModel>("SELECT * FROM bank_transaction", new DynamicParameters());
            return output.ToList();
        }
    }
    public static bool MoneyTransfer(int user_id, int from_account_id, int to_account_id, decimal amount)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                
                UPDATE bank_account SET balance = CASE
                    WHEN id = {from_account_id} AND balance >= {amount} THEN balance - {amount}
                    WHEN id = {to_account_id} THEN balance + {amount}

                END
                WHERE id IN ({from_account_id},{to_account_id})", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
                return false;
            }
            return true;
        }
    }

    public static bool AccountWithdraw(int user_id, int from_account_id, decimal amount)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            {
                try
                {
                    var output = cnn.Query($@"
                
                UPDATE bank_account SET balance = CASE
                    WHEN id = {from_account_id} AND balance>={amount} THEN balance-{amount}
                END
                WHERE id ={from_account_id}", new DynamicParameters());

                }
                catch (Npgsql.PostgresException e)
                {
                    Console.WriteLine(e.MessageText);
                    return false;
                }
                return true;
            }
        }
    }


    internal static List<BankBranchModel> LoadBankBranchModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<BankBranchModel>("SELECT * FROM bank_branch", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<BankCurrencyModel> LoadBankCurrencyModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<BankCurrencyModel>("SELECT * FROM bank_currency", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<BankLoanModel> LoadBankLoanModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<BankLoanModel>("SELECT * FROM bank_loan", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<BankRoleModel> LoadBankRoleModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<BankRoleModel>("SELECT * FROM bank_role", new DynamicParameters());
            return output.ToList();
        }
    }
}
