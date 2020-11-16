using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Scripts.Logics {

    interface IDBLogic {

        _Nodes GetNode(int id);
        string GetNodeContent(int id);
        string GetNodeName(int id);
        bool IsRootNameValid(string name);
        bool IsValidNodeName(string name);
        bool InsertRoot(string name);
        bool EnsureDatabase();
        bool InsertChild(int id, string name);
        void DeleteParentCascade(int parent);

    }
}
