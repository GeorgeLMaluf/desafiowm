using Modelos.Interfaces;

namespace Modelos
{
    public class Anuncio : IEntidade
    {
        #region Propriedades
        public int ID { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string versao { get; set; }
        public int ano { get; set; }
        public int quilometragem { get; set; }
        public string observacao { get; set; }
        #endregion

        #region Metodos
        public bool IsValid()
        {
            return ((marca != null) && (marca.Length <= 45)) &&
                   ((modelo != null) && (modelo.Length <= 45)) && 
                   ((versao != null) && (versao.Length <= 45)) &&
                   ((observacao != null));
        }
        #endregion
    };
}
