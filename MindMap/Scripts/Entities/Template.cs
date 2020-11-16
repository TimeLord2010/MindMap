using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Scripts.Entities {

    abstract class Template<T> {

        protected Action CT = null;

        public abstract string GetTableName();

        protected abstract Action GetCT();

        public void CreateTable() {
            CT.Invoke();
        }

        private void Create (Exception ex) {
            if (ex.Message.Contains("doesn't exist")) {
                CreateTable();
            }
        }

        protected void QueryRLoop (string title, MySqlCommand c, Action<MySqlDataReader> action) {
            Database.QueryRLoop(title, c, action, Create, true);
        }

        protected void NonQuery (string title, MySqlCommand c) {
            Database.NonQuery(title, c, Create, true);
        }

        public void NonQuery (string title, string q) {
            Database.NonQuery(title, q, Create, true);
        }

        public abstract void Insert(params object[] para);

        public abstract void Update(params object[] para);

        public abstract void Delete(params object[] para);

        public abstract List<T> Select(params object[] para);

    }
}
