using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace MindMap.Scripts.Logics {

    class DBLogic : IDBLogic {

        Errors Errors;

        public DBLogic() {
            Errors = new Errors();
        }

        public bool EnsureDatabase() {
            return Errors.SafeExecute(() => {
                Database.EnsureDatabase();
            });
        }

        public _Nodes GetNode(int id) {
            var r = Database.SelectNodes(id);
            if (r.Count == 1) {
                return r[0];
            }
            return null;
        }

        public string GetNodeContent(int id) {
            return GetNode(id)?.Content;
        }

        public string GetNodeName(int id) {
            return GetNode(id)?.Nome;
        }

        public bool InsertChild(int id, string name) {
            return Errors.SafeExecute(() => {
                Database.InsertNode(name);
                var Gid = Database.SelectNodes(name).Last().ID;
                Database.InsertChild(id, Gid);
            });
        }

        public bool InsertRoot(string name) {
            if (IsRootNameValid(name)) {
                Database.InsertNode(name);
                var nodes = Database.SelectNodes(name);
                if (nodes.Count == 0) {
                    Errors.last_error = new Exception("O identificador do nó não pôde ser encontrado.");
                    return false;
                }
                Database.InsertRoot(nodes.Last().ID);
                return true;
            } else {
                Errors.last_error = new InvalidOperationException("Nome para raiz deve ser único.");
                return false;
            }
        }

        public bool IsRootNameValid(string name) {
            if (!IsValidNodeName(name)) return false;
            var r = Database.SelectRoots(name);
            return r.Count == 0;
        }

        public bool IsValidNodeName(string name) {
            if (name is null) return false;
            string a = name.Replace(" ", "");
            if (a.Length == 0 || a.Contains("[") || a.Contains("]")) {
                return false;
            } else {
                return true;
            }
        }

        public void DeleteParentCascade(int parent) {
            var children = Database.SelectChildren(parent, null).Select(x => x.Child);
            foreach (var item in children) {
                DeleteParentCascade(item.ID);
            }
            Database.DeleteNode(parent);
        }

    }
}
