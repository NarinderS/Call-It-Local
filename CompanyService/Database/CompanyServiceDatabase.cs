using CompanyService.Handlers;
using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.DataTypes.Database.CompanyDirectory;
using Messages.NServiceBus.Commands;
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

        public void saveAccount(AccountCreated account)
        {
            if(openConnection() == true)
            {
                string query = "INSERT INTO accounts(username, password, address, phonenumber, email, type)" +
                    "VALUES('" + account.username + "','" + account.password + "','" + account.address + "','" + account.phonenumber + "','" + account.email + "','" + account.type.ToString() + "');";
                System.Diagnostics.Debug.WriteLine(query);
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                closeConnection();
            } else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        }

        // will throw error if it cannot save company
        public void saveCompany(CompanyInstance company)
        {
            if (openConnection() == true)
            {
                for (int i = 0; i < company.locations.Length; i++)
                {
                    string query = "INSERT INTO companies(companyName,phoneNumber,email,locations)" +
                        "VALUES('" + company.companyName + "','" + company.phoneNumber + "','" + company.email + "','" + company.locations[i] + "');";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }

                closeConnection();
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        }

        // will return null if cannot get company info
        public CompanyInstance getCompanyInfo(string companyName)
        {
            if (openConnection() == true)
            {
                string query = "SELECT * FROM " + dbname + ".companies"+" WHERE companyName='"+companyName+"';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                CompanyInstance ret = new CompanyInstance(companyName);
                List<string> locs = new List<string>();
                if (reader.Read())
                    do
                    {
                        ret.email = reader.GetString("email");
                        locs.Add(reader.GetString("locations"));
                        ret.phoneNumber = reader.GetString("phonenumber");
                    } while (reader.Read());
                else
                {
                    Debug.consoleMsg("Error: No such company: '" + companyName + "' in database");
                    return null;
                }
                ret.locations = locs.ToArray<string>();
                
                closeConnection();
                return ret;
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return null;
            }
        }

        // will return null if unable to connect to database,
        // CompanyList.companyNames will be empty if could not find any company
        public CompanyList searchCompanies(string searchString)
        {
            if (openConnection() == true)
            {
                string query = "SELECT * FROM " + dbname + ".companies" + " WHERE companyName LIKE '%" + searchString + "%';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                CompanyList ret = new CompanyList();
                List<string> compNames = new List<string>();

                while (reader.Read())
                {
                    string toAdd = reader.GetString("companyName");
                    if(!compNames.Contains(toAdd))
                        compNames.Add(toAdd);
                }
                ret.companyNames = compNames.ToArray<string>();

                closeConnection();
                return ret;
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return null;
            }
        }

        public static string companyInstanceToString(CompanyInstance company)
        {
            string ret = company.companyName + ";" + company.phoneNumber + ";" + company.email + ";";
            foreach (string i in company.locations)
                ret += i + ";";
            return ret.Substring(0, ret.Length-1);
        }

        public static string companyListToString(CompanyList companyList)
        {
            if (companyList.companyNames.Length == 0)
                return "";

            string ret = "";
            foreach (string i in companyList.companyNames)
                ret += i + ";";
            return ret.Substring(0, ret.Length - 1);
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
            new Table
            (
                dbname,
                "companies",
                new Column[]
                {
                    new Column
                    (
                        "companyName", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "phoneNumber", "VARCHAR(10)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "email", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "locations", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE"
                        }, true
                    )
                }
            ),

            new Table
            (
                dbname,
                "accounts",
                new Column[]
                {
                    new Column
                    (
                        "username", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                            "UNIQUE"
                        }, true
                    ),
                    new Column
                    (
                        "password", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "address", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "phonenumber", "VARCHAR(10)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "email", "VARCHAR(100)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "type", "ENUM('business','notspecified','user')",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    )
                }
            )
        };
    }



}
