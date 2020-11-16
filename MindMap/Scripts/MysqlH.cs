using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

public class MySqlH {

    public string CS;
    public bool ThrowExceptions { get; set; } = false;
    public Action<Exception> OnException = new Action<Exception>((ex) => {
        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    });

    public MySqlConnection Connection {
        get => new MySqlConnection(CS);
    }

    public MySqlH(MySqlConnectionStringBuilder sb) {
        CS = sb.ToString();
    }

    public MySqlH(string cs) {
        CS = cs;
    }

    public void NonQuery(MySqlCommand command, Action<Exception> onError = null, bool tryAgain = false) {
        Run((con) => {
            command.Connection = con;
            command.ExecuteNonQuery();
        }, onError, tryAgain);
    }

    public void NonQuery(string a, Action<Exception> onError = null, bool tryAgain = false) {
        Run(a, (command) => command.ExecuteNonQuery(), onError, tryAgain);
    }

    public void QueryR(MySqlCommand command, Action<MySqlDataReader> action, Action<Exception> onError = null, bool tryAgain = false) {
        Run((con) => {
            command.Connection = con;
            var r = command.ExecuteReader();
            action.Invoke(r);
        }, onError, tryAgain);
    }

    public void QueryR(string q, Action<MySqlDataReader> action, Action<Exception> onError = null, bool tryAgain = false) {
        QueryR(new MySqlCommand(q), action, onError, tryAgain);
    }

    public void QueryRLoop(MySqlCommand c, Action<MySqlDataReader> action, Action<Exception> onError = null, bool tryAgain = false) {
        QueryR(c, (r) => {
            while (r.Read()) {
                action.Invoke(r);
            }
        }, onError, tryAgain);
    }

    public void QueryRLoop(string c, Action<MySqlDataReader> action, Action<Exception> onError = null, bool tryAgain = false) {
        MySqlCommand c1 = new MySqlCommand(c);
        QueryRLoop(c1, action, onError, tryAgain);
    }

    private void Run(Action<MySqlConnection> action, Action<Exception> onError = null, bool tryAgain = false) {
        int a = 0;
        A:
        try {
            using (var connection = new MySqlConnection(CS)) {
                connection.Open();
                action.Invoke(connection);
            }
        } catch (Exception ex) {
            if (onError != null && a++ == 0) {
                onError.Invoke(ex);
                if (tryAgain) goto A;
            }
            OnException.Invoke(ex);
            //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (ThrowExceptions) throw ex; 
        }
    }

    private void Run(Action<MySqlCommand> a, Action<Exception> onError = null, bool tryAgain = false) {
        Run((c) => {
            var command = c.CreateCommand();
            a.Invoke(command);
        }, onError, tryAgain);
    }

    private void Run(string b, Action<MySqlCommand> a, Action<Exception> onError = null, bool tryAgain = false) {
        Run((c) => {
            var command = c.CreateCommand();
            command.CommandText = b;
            a.Invoke(command);
        }, onError, tryAgain);
    }

}