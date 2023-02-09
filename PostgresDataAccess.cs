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

    internal static void CreateUserModel(string firstName, string lastName, string pinCode, int roleId, int branchId, string bankEmail)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Execute($@"
                INSERT INTO bank_user (first_name, last_name, pin_code, role_id, branch_id, bank_email)
                VALUES ('{firstName}', '{lastName}', '{pinCode}', '{roleId}', '{branchId}', '{bankEmail}')", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
            }
        }
    }

    internal static void CreateAccountModel(string accountTypeName, decimal balance, decimal interestRate, int userId, int currencyId)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Execute($@"
                INSERT INTO bank_account (name, balance, interest_rate, user_id, currency_id)
                VALUES ('{accountTypeName}', '{balance}', '{interestRate}', '{userId}', '{currencyId}')", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
            }
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


    internal static List<BankCurrencyModel> LoadCurrencyModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<BankCurrencyModel>("SELECT * FROM bank_currency", new DynamicParameters());
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
    public static bool MoneyTransfer(int from_account_id, int to_account_id, decimal amount)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                BEGIN TRANSACTION;
                UPDATE bank_account SET balance = CASE
                    WHEN id = {from_account_id} AND balance >= {amount} THEN balance - {amount}
                    WHEN id = {to_account_id} THEN balance + {amount}                   
                END
                WHERE id IN ({from_account_id},{to_account_id});
                INSERT INTO bank_transaction (name, from_account_id, to_account_id,amount) VALUES('Överföring',{from_account_id},{to_account_id},{amount});
                COMMIT;", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
                return false;
            }
            return true;
        }
    }
    public static bool MoneyTransferOther(int from_account_id, int to_account_id, decimal amount)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            try
            {
                var output = cnn.Query($@"
                BEGIN TRANSACTION;
                UPDATE bank_account SET balance = CASE
                    WHEN id = {from_account_id} AND balance >= {amount} THEN balance - {amount}
                    WHEN id = {to_account_id} THEN balance + {amount}                   
                END
                WHERE id IN ({from_account_id},{to_account_id});
                INSERT INTO bank_transaction (name, from_account_id, to_account_id,amount) VALUES('Överföring',{from_account_id},{to_account_id},{amount});
                COMMIT;", new DynamicParameters());
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.MessageText);
                return false;
            }
            return true;
        }
    }


    public static bool AccountWithdraw(int from_account_id, decimal amount)
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
                WHERE id ={from_account_id};
                INSERT INTO bank_transaction (name, from_account_id, amount) VALUES('Uttag',{from_account_id},{amount});", new DynamicParameters());


                }
                catch (Npgsql.PostgresException e)
                {
                    return false;
                }
                return true;
            }
        }
    }

    internal static List<TransactionModel> TransactionHistory(int account_id)
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            {
                var output = cnn.Query<TransactionModel>($@"SELECT * FROM bank_transaction  WHERE from_account_id = {account_id} OR to_account_id = {account_id} ORDER BY timestamp DESC", new DynamicParameters());
                return output.ToList();
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
