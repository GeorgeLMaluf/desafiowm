using Modelos.Interfaces;
using System.Collections.Generic;

namespace Persistencia.Interfaces
{
    public interface IRepositorio<T> where T: IEntidade
    {
        #region Metodos
        T Consultar(SQLBuilder condicao);
        IList<T> Listar(SQLBuilder condicao);
        bool Excluir(T entidade);
        bool Salvar(T entidade);
        #endregion
    }
}
