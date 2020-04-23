using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Persistencia
{
    public class ConexaoBD : IDisposable
    {
        #region Propriedades privadas
        private SqlConnection _cnx;
        private AnuncioRepositorio _anuncios;


        private String ConnectionStringBD
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
            }
        }

        private bool EstaConectado
        {
            get { return _cnx.State == System.Data.ConnectionState.Open; }
        }

        #endregion

        #region Construtor
        public ConexaoBD(bool criarConectado = true)
        {
            _cnx = new SqlConnection(ConnectionStringBD);
            if (criarConectado)
            {
                AbrirConexao();
            }
        }
        #endregion

        #region Metodos privados
        private void AbrirConexao()
        {
            if (!EstaConectado)
            {
                _cnx.Open();
            }
        }

        private void FechaConexao()
        {
            if (EstaConectado)
            {
                _cnx.Close();
            }
        }
        #endregion

        #region Metodos publicos
        public AnuncioRepositorio Anuncios
        {
            get
            {
                if (_anuncios == null)
                {
                    _anuncios = new AnuncioRepositorio(_cnx);
                }
                return _anuncios;
            }
        }

        public void Dispose()
        {
            FechaConexao();
        }
        #endregion
    }
}
