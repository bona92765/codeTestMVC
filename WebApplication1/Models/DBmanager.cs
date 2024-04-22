using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DBmanager
    {
        private readonly string ConnString = "Data Source=.;Initial Catalog=Testfor;Integrated Security=True;";

        public List<NBandAccoData> GetUserData()
        {
            List<NBandAccoData> list = new List<NBandAccoData>();
            SqlConnection sqlConnection = new SqlConnection(ConnString);

            string query = "SELECT NBank.NationalID, Account.BranchID, Account.AcctSerialID " +
                           "FROM NetBankUsers NBank " +
                           "JOIN Accounts Account " +
                           "ON NBank.UserGuid = Account.OwnerGuid " +
                           "WHERE NBank.NationalID = @NationalID";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@NationalID", "K18888886");
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                NBandAccoData data = new NBandAccoData
                {
                    NationalID = reader["NationalID"].ToString(),
                    BranchID = reader["BranchID"].ToString(),
                    AcctSerialID = reader["AcctSerialID"].ToString(),
                };

                list.Add(data);
            }
            sqlConnection.Close();
            return list;
        }
    }
}