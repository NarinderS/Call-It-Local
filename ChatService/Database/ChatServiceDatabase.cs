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
                return instance;
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
                string query = "INSERT INTO chathistory(sender,receiver,timestamp,message)" +
                    "VALUES('" + message.sender + "','" + message.receiver + "','" + message.unix_timestamp + "','" + message.messageContents + "');";

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
                string query = "SELECT DISTINCT receiver FROM " + dbname + ".chathistory" + " WHERE sender ='" + userName + "';";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                List<string> companies = new List<string>();

                if (reader.Read())
                    do
                    {
                        companies.Add(reader.GetString("receiver"));
                    } while (reader.Read());
                else
                {
                    Debug.consoleMsg("Error: No such user: '" + userName + "' in database");
                }

                GetChatContacts ret = new GetChatContacts()
                {
                    usersname = userName,
                    contactNames = companies
                };

                closeConnection();
                return ret;
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return null;
            }
        }

        public GetChatHistory getChatHistory(string userName, string compName)
        {
            if (openConnection() == true)
            {
                string query = "SELECT * FROM " + dbname + ".chathistory" + " WHERE ((sender ='" + userName + "' AND receiver='" + compName + "') OR (sender ='" + compName +"' AND receiver ='" + userName + "')) ORDER BY timestamp;";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                List<ChatMessage> messageList = new List<ChatMessage>();

                if (reader.Read())
                    do
                    {
                        ChatMessage temp = new ChatMessage()
                        {
                            sender = reader.GetString("sender"),
                            receiver = reader.GetString("receiver"),
                            unix_timestamp = reader.GetInt32("timestamp"),
                            messageContents = reader.GetString("message")
                        };

                        messageList.Add(temp);
                    } while (reader.Read());
                else
                {
                    Debug.consoleMsg("Error: No conversation between user: '" + userName + "' and company: '" + compName + "' in database");
                }

                ChatHistory hist = new ChatHistory()
                {
                    messages = messageList,
                    user1 = userName,
                    user2 = compName
                };

                GetChatHistory ret = new GetChatHistory()
                {
                    history = hist
                };

                closeConnection();
                return ret;
            }
            else
            {
                Debug.consoleMsg("Unable to connect to database");
                return null;
            }
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
                "chathistory",
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
                        "timestamp", "INT(10)",
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
