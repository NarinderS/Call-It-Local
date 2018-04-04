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
        private ChatServiceDatabase()
        {
        }

        public string getChatContacts()
        {
            return "Test return";
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
                        "user1", "VARCHAR(40)",
                        new string[]
                        {
                            "NOT NULL",
                        }, true
                    ),
                    new Column
                    (
                        "user2", "VARCHAR(40)",
                        new string[]
                        {
                            "NOT NULL",
                        }, true
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
