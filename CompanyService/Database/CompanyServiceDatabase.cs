using CompanyService.Handlers;
using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.Echo.Requests;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyService.Database
{
    /// <summary>
    /// This portion of the class contains methods and functions
    /// </summary>
    public partial class CompanyServiceDatabase : AbstractDatabase
    {
        private CompanyServiceDatabase() { }

        public static CompanyServiceDatabase getInstance()
        {
            if (instance == null)
            {
                instance = new CompanyServiceDatabase();
            }
            return instance;
        }

        //WRITE QUERY 
        public void saveAccount(AccountCreated account)
        {
            if (openConnection() == true)
            {
                string query = "";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                closeConnection();
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        }
    }

    public partial class CompanyServiceDatabase : AbstractDatabase
    {
        /// <summary>
        /// The name of the database.
        /// Both of these properties are required in order for both the base class and the
        /// table definitions below to have access to the variable.
        /// </summary>
        private const String dbname = "companyservicedb";
        public override string databaseName { get; } = dbname;

        /// <summary>
        /// The singleton isntance of the database
        /// </summary>
        protected static CompanyServiceDatabase instance = null;

        /// <summary>
        /// This property represents the database schema, and will be used by the base class
        /// to create and delete the database.
        /// </summary>
        protected override Table[] tables { get; } =
        {
            
            
        };
    }



}
