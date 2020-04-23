using System;
using System.Text;

namespace Persistencia
{
    public class SQLBuilder
    {
        #region Propriedades
        private readonly StringBuilder _where = new StringBuilder();

        public string OrderBy { get; set; } = String.Empty;
        
        public string Where
        {
            get
            {
                if (_where.Length > 0)
                {
                    return $"WHERE {_where.ToString()}";
                }
                return String.Empty;
            }
        }
        #endregion

        #region Metodos
        public void AddWhere(string condicao)
        {
            _where.Append(condicao);
        }

        public override string ToString()
        {
            var resultado = Where;
            if (!String.IsNullOrWhiteSpace(OrderBy))
            {
                resultado = String.Concat(resultado, $" ORDER BY {OrderBy}");
            }
            return resultado;
        }

        public void Clear()
        {
            OrderBy = String.Empty;
        }
        #endregion
    }
}
