using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MindMap.Scripts.Entities {
    class Children : Template<_Children> {

        public Children() {
            GetCT();
        }

        public override string GetTableName() {
            return "children";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="para">int[,(int|null)]</param>
        public override void Delete(params object[] para) {
            var c = new MySqlCommand();
            if (para.Length == 1) {
                if (para[0] is int parent) {
                    c.CommandText = $"delete from {GetTableName()} where parent = {parent};";
                } else {
                    throw new ArgumentException($"O primeiro argumento deve ser inteiro. ({ErrorCodes.O005})");
                }
            } else if (para.Length == 2) {
                if (para[0] is int parent && para[1] is int child) {
                    c.CommandText = $"delete from {GetTableName()} where parent = {parent} and child = {child};";
                } else if (para[0] is int parent1 && para[1] is null) {
                    c.CommandText = $"delete from {GetTableName()} where parent = {parent1};";
                } else {
                    throw new ArgumentException("O primeiro argumento deve ser inteiro, e o segundo deve ser (null|int)");
                }
            } else {
                throw new ArgumentException("O funcão aceita até 2 argumentos.");
            }
            NonQuery($"Erro ao deletar relação entre nós. ({ErrorCodes.DB017})", c);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="para">Parent,Child</param>
        public override void Insert(params object[] para) {
            if (para.Length == 2) {
                if (para[0] is int parent && para[1] is int child) {
                    var c = new MySqlCommand();
                    c.CommandText = $"insert into {GetTableName()} values ({parent}, {child});";
                    NonQuery($"Erro ao inserir relação de pai e filho. ({ErrorCodes.DB015})", c);
                } else {
                    throw new ArgumentException($"É preciso que os dois argumentos sejam inteiros. ({ErrorCodes.DB014})");
                }
            } else {
                throw new ArgumentException($"É preciso ter 2 argumentos, não opcionais. ({ErrorCodes.DB013})");
            }
        }

        public override List<_Children> Select(params object[] para) {
            var n = new Nodes();
            var c = new MySqlCommand();
            c.CommandText =
                $"select " +
                $"  a.parent," +
                $"  b.nome," +
                $"  a.child," +
                $"  c.nome " +
                $"from {GetTableName()} as a " +
                $"inner join {n.GetTableName()} as b on a.parent = b.id " +
                $"inner join {n.GetTableName()} as c on a.child = c.id ";
            if (para.Length == 1) {
                if (para[0] is int parent) {
                    c.CommandText += $"where a.parent = {parent};";
                } else {
                    throw new ArgumentException($"O primeiro argumento deve ser inteiro, correspondente ao nó pai. ({ErrorCodes.O002})");
                }
            } else if (para.Length == 2) {
                c.CommandText += "where ";
                if (para[0] is int parent && para[1] is int child) {
                    c.CommandText += $"a.parent = {parent} and a.child = {child};";
                } else if (para[0] is null && para[1] is int child1) {
                    c.CommandText += $"a.child = {child1};";
                } else if (para[0] is int parent1 && para[1] is null) {
                    c.CommandText += $"a.parent = {parent1};";
                } else {
                    throw new ArgumentException($"Os argumentos devem ser inteiros, correspondente a pai e filho. ({ErrorCodes.O003})");
                }
            } else {
                throw new ArgumentException($"O número máximo de argumentos é 2. ({ErrorCodes.O004})");
            }
            var list = new List<_Children>();
            QueryRLoop($"Erro ao obter associação de nós. ({ErrorCodes.DB016})", c, (r) => {
                list.Add(new _Children() {
                    Parent = new _Nodes() {
                        ID = r.GetInt32(0),
                        Nome = r.GetString(1)
                    },
                    Child = new _Nodes() {
                        ID = r.GetInt32(2),
                        Nome = r.GetString(3)
                    }
                });
            });
            return list;
        }

        public override void Update(params object[] para) {
            MessageBox.Show($"Não é possível atualizar relação de pai e filho. Delete e insira para obter o resultado desejado. ({ErrorCodes.O001})");
        }

        protected override Action GetCT() {
            CT = new Action(() => {
                var n = new Nodes();
                n.CreateTable();
                Database.NonQuery($"Erro ao criar tebela '{GetTableName()}'",
                    $"create table if not exists {GetTableName()} (" +
                    $"  parent int," +
                    $"  child int," +
                    $"  primary key (child)," +
                    $"  foreign key (parent) references {n.GetTableName()} (id) on delete cascade on update cascade," +
                    $"  foreign key (child) references {n.GetTableName()} (id) on delete cascade on update cascade" +
                    $");");
            });
            return CT;
        }
    }
}
