using Dapper;
using FoxBank.Models;
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
            var output = cnn.Query<UserModel>("SELECT * FROM user", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<AccountModel> LoadAccountModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<AccountModel>("SELECT * FROM account", new DynamicParameters());
            return output.ToList();
        }
    }

    internal static List<TransactionModel> LoadTransactionModel()
    {
        using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        {
            var output = cnn.Query<TransactionModel>("SELECT * FROM transaction", new DynamicParameters());
            return output.ToList();
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
