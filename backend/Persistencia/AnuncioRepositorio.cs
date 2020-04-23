using Modelos;
using Persistencia.Extensions;
using Persistencia.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class AnuncioRepositorio: IRepositorio<Anuncio>
    {
        #region Campos privados
        private SqlConnection _cnx;

        #endregion

        #region Construtor
        public AnuncioRepositorio(SqlConnection cnx)
        {
            _cnx = cnx;
        }
        #endregion

        #region IRepositorio Implementation
        public Anuncio Consultar(SQLBuilder condicao)
        {
            return Listar(condicao).FirstOrDefault();
        }

        public bool Excluir(Anuncio entidade)
        {
            var SQL = new StringBuilder();
            SQL.Append("DELETE FROM tb_AnuncioWebMotors WHERE ID = @id");

            var cmd = _cnx.CreateCommand();
            cmd.CommandText = SQL.ToString();
            cmd.Parameters.Add(new SqlParameter("@id", entidade.ID));

            return cmd.ExecuteNonQuery() > 0;
        }

        public IList<Anuncio> Listar(SQLBuilder condicao)
        {
            var SQL = new StringBuilder();
            SQL.Append("SELECT ID, marca, modelo, versao, ano, quilometragem, observacao ");
            SQL.Append("FROM tb_AnuncioWebmotors ");
            SQL.Append(condicao);

            var retorno = new List<Anuncio>();
            var cmd = _cnx.CreateCommand();
            cmd.CommandText = SQL.ToString();
            using (var reader  = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var anuncio = new Anuncio()
                    {
                        ID = reader.GetInt("ID"),
                        marca = reader.GetString("marca"),
                        modelo = reader.GetString("modelo"),
                        versao = reader.GetString("versao"),
                        ano = reader.GetInt("ano"),
                        quilometragem = reader.GetInt("quilometragem"),
                        observacao = reader.GetString("observacao")
                    };
                    retorno.Add(anuncio);
                }
            }
            return retorno;
        }

        public bool Salvar(Anuncio entidade)
        {
            if (entidade.IsValid())
            {
                var SQL = new StringBuilder();

                if (entidade.ID.Equals(0))
                {
                    SQL.Append("INSERT INTO tb_AnuncioWebmotors (marca, modelo, versao, ano, quilometragem, observacao) ");
                    SQL.Append("OUTPUT INSERTED.ID ");
                    SQL.Append("VALUES (@marca, @modelo, @versao, @ano, @quilometragem, @observacao) ");                    
                }
                else
                {
                    SQL.Append("UPDATE tb_AnuncioWebmotors SET marca = @marca, versao = @versao, ano = @ano, ");
                    SQL.Append("quilometragem = @quilometragem, observacao = @observacao ");
                    SQL.Append("WHERE ID = @id");
                }

                var cmd = _cnx.CreateCommand();
                cmd.CommandText = SQL.ToString();
                cmd.Parameters.Add(new SqlParameter("@marca", entidade.marca));
                cmd.Parameters.Add(new SqlParameter("@versao", entidade.versao));
                cmd.Parameters.Add(new SqlParameter("@modelo", entidade.modelo));
                cmd.Parameters.Add(new SqlParameter("@ano", entidade.ano));
                cmd.Parameters.Add(new SqlParameter("@quilometragem", entidade.quilometragem));
                cmd.Parameters.Add(new SqlParameter("@observacao", entidade.observacao));
                if (!entidade.ID.Equals(0))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", entidade.ID));
                    return cmd.ExecuteNonQuery() > 0;
                }
                else
                {
                    entidade.ID = (int)cmd.ExecuteScalar();
                    return entidade.ID > 0;
                }
            }
            return false;
        }
        #endregion
    }
}
