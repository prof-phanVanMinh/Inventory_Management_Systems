using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess.SqlServer
{
    public abstract class ConnectionToSQL
    {
        //field
        private readonly string connectionString;
        //Constructor
        public ConnectionToSQL()
        {
            connectionString = "Server = LAPTOP-P2LDQHQM; Database = SaleStoreDB; integrated security = true";
        }
        //method
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
