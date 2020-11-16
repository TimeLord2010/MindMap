using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMap.Scripts.Entities {

    class Nodes : Template<_Nodes> {

        public Nodes() {
            GetCT();
        }

        public override string GetTableName() {
            return "nodes";
        }

        protected override Action GetCT() {
            CT = new Action(() => {
                Database.NonQuery($"Erro ao criar tabela de nós. ({ErrorCodes.DB002})",
                    $"create table if not exists {GetTableName()} (" +
                    $"  id int auto_increment," +
                    $"  nome nvarchar(100) not null," +
                    $"  content longtext," +
                    $"  primary key (id)" +
                    $");");
            });
            return CT;
        }

        /// <summary>
        /// Deleta um nó, apartir de um identificador
        /// </summary>
        /// <param name="para">ID</param>
        public override void Delete(params object[] para) {
            if (para.Length == 1) {
                if (para[0] is int id) {
                    NonQuery($"Erro ao deletar nó. ({ErrorCodes.DB003})", $"delete from {GetTableName()} where id = {id};");
                } else {
                    throw new ArgumentException("O primeiro argumento deve ser do tipo int, correspondente ao identificador do nó.");
                }
            } else {
                throw new ArgumentException("Deve ser fornecido ao menos um argumento para deletar nós.");
            }
        }

        /// <summary>
        /// <para>Insere um nó no banco de dados.</para>
        /// </summary>
        /// <param name="para">Nome[,Conteúdo]</param>
        public override void Insert(params object[] para) {
            var c = new MySqlCommand();
            c.CommandText = $"insert into {GetTableName()} values (null, @nome";
            if (para.Length == 0) throw new NotImplementedException("É preciso informar ao menos o nome do nó.");
            if (para.Length == 1) {
                if (para[0] is string nome) {
                    c.CommandText += ", null";
                    c.Parameters.AddWithValue("@nome", nome);
                } else {
                    throw new ArgumentException("O primeiro argumento deve ser do tipo string, correspondente ao nome do nó.");
                }
            } else if (para.Length == 2) {
                if (para[0] is string nome && para[1] is string content) {
                    c.CommandText += $",@content";
                    c.Parameters.AddWithValue("@nome", nome);
                    c.Parameters.AddWithValue("@content", content);
                } else {
                    throw new NotImplementedException("O segundo argumento deve ser do tipo string, correspondente ao conteúdo do nó.");
                }
            } else {
                throw new NotImplementedException("Muitos argumentos para inserir na tabela nó.");
            }
            c.CommandText += ");";
            NonQuery($"Erro ao inserir nó. ({ErrorCodes.DB001})", c);
        }

        public override List<_Nodes> Select(params object[] para) {
            var c = new MySqlCommand();
            c.CommandText = $"select * from {GetTableName()} ";
            if (para.Length == 1) {
                c.CommandText += "where ";
                if (para[0] is int id) {
                    c.CommandText += "id = @id ";
                    c.Parameters.AddWithValue("@id", id);
                } else if (para[0] is string nome) {
                    c.CommandText += $"nome like concat(@nome, '%') ";
                    c.Parameters.AddWithValue("@nome", nome);
                } else {
                    throw new ArgumentException("O primeiro argumento deve ser int (identificador) ou string (nome).");
                }
            } else {
                throw new ArgumentException("Um nó só pode ser buscado por id ou nome. Um de cada vez.");
            }
            c.CommandText += ";";
            var lista = new List<_Nodes>();
            QueryRLoop($"Erro ao obter nós. ({ErrorCodes.DB006})", c, (r) => {
                lista.Add(new _Nodes() {
                    ID = r.GetInt32(0),
                    Nome = r.GetString(1),
                    Content = r.IsDBNull(2) ? null : r.GetString(2)
                });
            });
            return lista;
        }

        /// <summary>
        /// Atualiza um nó no banco de dados.
        /// </summary>
        /// <param name="para">ID, Nome (string|null), Conteúdo(string, null)</param>
        public override void Update(params object[] para) {
            if (para.Length == 3) {
                var c = new MySqlCommand();
                c.CommandText = $"update {GetTableName()} set ";
                if (!(para[0] is int id)) throw new ArgumentException("É preciso ter o identificador com tipo int no primeiro argumento.");
                if (para[1] is string nome && para[2] is string conteudo) {
                    c.CommandText += "nome = @nome, content = @content ";
                    c.Parameters.AddWithValue("@nome", nome);
                    c.Parameters.AddWithValue("@content", conteudo);
                } else if (para[1] is string nome1) {
                    c.CommandText += "nome = @nome ";
                    c.Parameters.AddWithValue("@nome", nome1);
                } else if (para[2] is string conteudo1) {
                    c.CommandText += "content = @content ";
                    c.Parameters.AddWithValue("@content", conteudo1);
                } else {
                    throw new ArgumentException("Os argumento não eram do tipo esperado.");
                }
                c.CommandText += $"where id = {id};";
                NonQuery($"Erro ao atualizar nó. ({ErrorCodes.DB005})", c);
            } else {
                throw new ArgumentException("Para atualizar um nó, é preciso ter exatamente 3 argumentos.");
            }
        }
    }
}
