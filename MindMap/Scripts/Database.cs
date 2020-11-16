using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MindMap.Scripts.Entities;
using System.Windows;

namespace MindMap.Scripts {

    class Database {

        public static string Name = "MindMap";
        private static string Title = "Error";
        public static Action<Exception> OnException = new Action<Exception>((ex) => {
            if (Title == null) Title = "Error";
            MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            Title = "Error";
        });
        public static string Server, User, Password;

        private static Nodes nodes = new Nodes();
        private static Roots roots = new Roots();
        private static Children children = new Children();

        public static MySqlH sql;
        public static MySqlH SQL {
            get {
                if (sql == null) sql = EnsureDatabase();
                return sql;
            }
        }

        public static MySqlH EnsureDatabase() {
            Logger logger = new Logger("Database_EnsureDatabase.txt");
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder() {
                UserID = User,
                Password = Password,
                Server = Server,
                ConnectionTimeout = 10
            };
            logger.Info($"Connection string: " + sb.ToString());
            var sql = new MySqlH(sb);
            logger.Info($"Creating database '{Name}' if not exists.");
            sql.NonQuery($"create database if not exists {Name};", (ex) => {
                OnException.Invoke(ex);
                logger.Error($"Error: {ex.Message}");
                Environment.Exit(Environment.ExitCode);
            });
            sb.Database = Name;
            logger.Info($"New connection string: {sb.ToString()}");
            sql = new MySqlH(sb);
            sql.OnException = OnException;
            logger.Info($"End");
            return sql;
        }

        public static void InsertRoot(int id) {
            roots.Insert(id);
        }

        public static List<_Nodes> SelectRoots(object nome) {
            return roots.Select(nome);
        }

        public static List<_Nodes> SelectNodes(object nome) {
            return nodes.Select(nome);
        }

        public static List<_Children> SelectChildren(object parent, object child) {
            return children.Select(parent, child);
        }

        public static void InsertNode (params object[] para) {
            nodes.Insert(para);
        }

        public static void InsertChild (int parent, int child) {
            children.Insert(parent, child);
        }

        public static void UpdateNode(params object[] para) {
            nodes.Update(para);
        }

        public static void DeleteNode(params object[] para) {
            nodes.Delete(para);
        }

        public static void Operation(string title, Action action) {
            Title = title;
            action.Invoke();
        }

        public static void QueryRLoop(string title, MySqlCommand c, Action<MySqlDataReader> action, Action<Exception> onError = null, bool tryAgain = false) {
            Operation(title, () => SQL.QueryRLoop(c, action, onError ?? OnException, tryAgain));
        }

        public static void NonQuery(string title, MySqlCommand c, Action<Exception> onError = null, bool tryAgain = false) {
            Operation(title, () => SQL.NonQuery(c, onError ?? OnException, tryAgain));
        }

        public static void NonQuery(string title, string q, Action<Exception> onError = null, bool tryAgain = false) {
            Operation(title, () => SQL.NonQuery(q, onError ?? OnException, tryAgain));
        }

    }
}
