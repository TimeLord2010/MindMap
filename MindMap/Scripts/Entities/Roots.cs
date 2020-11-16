using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MindMap.Scripts.Entities {

    class Roots : Template<_Nodes> {

        public Roots() {
            GetCT();
        }

        protected override Action GetCT() {
            CT = new Action(() => {
                var n = new Nodes();
                n.CreateTable();
                Database.NonQuery($"Erro ao criar tabela de raizes. ({ErrorCodes.DB004})",
                    $"create table if not exists {GetTableName()} (" +
                    $"  id int," +
                    $"  primary key (id)," +
                    $"  foreign key (id) references {n.GetTableName()} (id) on delete cascade on update cascade" +
                    $");");
            });
            return CT;
        }

        public override string GetTableName() {
            return "roots";
        }

        public override void Delete(params object[] para) {
            ValidateInput(para);
            NonQuery($"Erro ao deletar raiz. ({ErrorCodes.DB008})", $"delete from {GetTableName()} where id = {(int)para[0]}");
        }

        public override void Insert(params object[] para) {
            ValidateInput(para);
            NonQuery($"Erro ao inserir raiz. ({ErrorCodes.DB007})", $"insert into {GetTableName()} values ({(int)para[0]});");
        }

        public override List<_Nodes> Select(params object[] para) {
            var c = new MySqlCommand();
            if (para.Length == 1) {
                if (para[0] is int id) {
                    c.CommandText += $"select * from {GetTableName()} where id = {id};";
                } else if (para[0] is string nome) {
                    var nodes = new Nodes();
                    c.CommandText +=
                        $"select * " +
                        $"from {nodes.GetTableName()} as a " +
                        $"inner join {GetTableName()} as b on a.id = b.id " +
                        $"where nome like concat(@nome, '%');";
                    c.Parameters.AddWithValue("@nome", nome);
                } else {
                    throw new ArgumentException($"O primeiro argumento deve ser int (identificador) ou string (nome). ({ErrorCodes.DB010})");
                }
            }
            var lista = new List<_Nodes>();
            QueryRLoop($"Erro ao obter raizes. ({ErrorCodes.DB009})", c, (r) => {
                lista.Add(new _Nodes() {
                    ID = r.GetInt32(0),
                    Nome = r.IsDBNull(1)? null : r.GetString(1),
                    Content = r.IsDBNull(2)? null : r.GetString(2)
                });
            });
            return lista;
        }

        public override void Update(params object[] para) {
            MessageBox.Show($"Não é possível atualizar raiz. ({ErrorCodes.DB011})");
        }

        private void ValidateInput(object[] para) {
            if (para.Length == 1) {
                if (!(para[0] is int)) throw new ArgumentException($"O primeiro argumento deve ser do tipo inteiro, correspondente ao identificador do nó. ({ErrorCodes.DB012})");
            } else {
                throw new ArgumentException("O método para inserir raiz pode ter somente um argumento do tipo inteiro.");
            }
        }
    }
}