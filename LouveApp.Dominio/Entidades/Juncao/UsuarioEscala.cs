using System.Collections.Generic;

namespace LouveApp.Dominio.Entidades.Juncao
{
    public sealed class UsuarioEscala : EntidadeJuncaoComUsuario
    {
        #region Propriedades

        public string EscalaId { get; private set; }
        private Escala _escala;
        public Escala Escala
        {
            get => _escala;
            private set
            {
                if (value != null) EscalaId = value.Id;
                _escala = value;
            }
        }

        public ICollection<UsuarioEscalaInstrumento> Instrumentos;

        #endregion

        #region Construtores

        private UsuarioEscala() { }

        public UsuarioEscala(string usuarioId, IEnumerable<string> instrumentosIds)
        {
            UsuarioId = usuarioId;

            Instrumentos = new List<UsuarioEscalaInstrumento>();

            foreach (var instrumentoId in instrumentosIds)
            {
                Instrumentos.Add(new UsuarioEscalaInstrumento(instrumentoId));
            }
        }

        #endregion
    }
}
