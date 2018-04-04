using ChatService.Handlers;
using Messages;
using Messages.Database;
using Messages.DataTypes;
using Messages.DataTypes.Database.Chat;
using Messages.NServiceBus.Commands;
using Messages.NServiceBus.Events;
using Messages.ServiceBusRequest.Echo.Requests;

using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Database
{
    /// <summary>
    /// This portion of the class contains methods and functions
    /// </summary>
    public partial class ChatServiceDatabase : AbstractDatabase
    {

        // Constructor 
        private ChatServiceDatabase()
        {
        }

        public static ChatServiceDatabase getInstance()
        {
            if(instance == null)
            {
                Debug.consoleMsg("Creating instance of the ChatServiceDatabase");
                instance = new ChatServiceDatabase();
            }
            
            else
            {
                return instance;
            }
        }

        public void saveMessage(ChatMessage message)
        {
            if (openConnection() == true)
            {
                string query = "INSERT INTO chatHistory(sender,receiver,timestamp,message)" +
                    "VALUES('" + message.sender + "','" + message.receiver + "','" + message.unix_timestamp + "','" + message.message + "');";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                closeConnection();

            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
            }
        }

        public GetChatContacts getChatContacts(string userName)
        {
            if (openConnection() == true)
            {
                string query = "SELECT DISTINCT receiver FROM " + dbname + ".chatHistory" + " WHERE sender ='" + userName + "';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                GetChatContacts ret = new GetChatContacts();
                ret.usersname = userName;

                if (reader.Read())
                    do
                    {
                        ret.contactNames.Add(reader.GetString("receiver"));
                    } while (reader.Read());
                else
                {
                    Debug.consoleMsg("Error: No such user: '" + userName + "' in database");
                    return null;
                }

                closeConnection();
                return ret;
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return null;
            }
        }

        public string getChatHistory()
        {
            return "Test return";
        }
    }



    public partial class ChatServiceDatabase : AbstractDatabase
    {
        /// <summary>
        /// The name of the database.
        /// Both of these properties are required in order for both the base class and the
        /// table definitions below to have access to the variable.
        /// </summary>
        private const String dbname = "chatservicedb";
        public override string databaseName { get; } = dbname;

        /// <summary>
        /// The singleton isntance of the database
        /// </summary>
        protected static ChatServiceDatabase instance = null;

        /// <summary>
        /// This property represents the database schema, and will be used by the base class
        /// to create and delete the database.
        /// </summary>
        protected override Table[] tables { get; } =
        {
            new Table
            (
                dbname,
                "chatHistory",
                new Column[]
                {
                    
                    new Column
                    (
                        "sender", "VARCHAR(40)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "receiver", "VARCHAR(40)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "timestamp", "VARCHAR(10)",
                        new string[]
                        {
                            "NOT NULL",
                        }, false
                    ),
                    new Column
                    (
                        "message", "VARCHAR(140)",
                        new string[]
                        {
                            "NOT NULL"
                        }, false
                    )
                }
            )
        };
    }
}
