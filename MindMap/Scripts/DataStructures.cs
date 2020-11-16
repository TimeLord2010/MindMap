using MindMap.Scripts.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Scripts {
    
    public class _Nodes {

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Content { get; set; }

        public _Nodes() { }

        public _Nodes(int id) {
            ID = id;
            var node = Logic.DBLogic.GetNode(id);
            Nome = node.Nome;
            Content = node.Content;
        }

    }

    public class _Children {

        public _Nodes Parent { get; set; }
        public _Nodes Child { get; set; }

    }

    public struct Node {

        public int ID;
        public string Name;
        public string Content;

    }

    public class Tree {

        public Tree Parent;
        public _Nodes Current;
        public List<Tree> Children = new List<Tree>();

        public Tree() { }

        public Tree(_Nodes node) {
            Current = node;
        }

        public List<_Nodes> GetLeafs () {
            var leafs = new List<_Nodes>();
            ForEach(this, (x) => { 
                if (x.Children.Count == 0) {
                    leafs.Add(x.Current);
                }
            });
            return leafs;
        }

        public List<_Nodes> GetRowAt (int i) {
            var row = new List<_Nodes>();
            ForEachRow(this, (nodes, j) => { 
                if (j == i) {
                    nodes.ForEach(x => row.Add(x));
                }
            });
            return row;
        }

        public List<_Nodes> GetLastRow () {
            var lasts = new List<_Nodes>();
            var rc = GetDepth();
            ForEachRow(this, (nodes, row) => { 
                if (row == rc) {
                    nodes.ForEach(x => lasts.Add(x));
                }
            });
            return lasts;
        }

        public static void ForEachWithParent(Tree tree, Action<Tree> action) {
            action.Invoke(tree.Parent);
            ForEach(tree, action);
        }

        public static void ForEach (Tree tree, Action<Tree> action) {
            action.Invoke(tree);
            foreach (var child in tree.Children) {
                ForEach(child, action);
            }
        }

        public int GetDepth () {
            int max = 0;
            ForEachRow(this, (nodes, i) => { 
                if (max < i) max = i;
            });
            return max;
        }

        public static void ForEachRow (Tree tree, Action<List<_Nodes>, int> action) {
            ForEachRow(tree, 0, action);
        }

        private static void ForEachRow (Tree tree, int i, Action<List<_Nodes>, int> action) {
            var children = tree.Children.Select(x => x.Current).ToList();
            action.Invoke(children, i+1);
            foreach (var child in tree.Children) {
                ForEachRow(child, i + 1, action);
            }
        }

        public static Tree Build (int id, int rows = 4) {
            return Build(new _Nodes(id), rows);
        }

        public static Tree Build (_Nodes node, int rows = 4) {
            var root = new Tree(node);
            if (rows == 0) return root;
            var children = Database.SelectChildren(node.ID, null).Select(item => item.Child);
            foreach (var child in children) {
                var item = Build(child, rows - 1);
                item.Parent = root;
                root.Children.Add(item);
            }
            return root;
        }

    }

}
