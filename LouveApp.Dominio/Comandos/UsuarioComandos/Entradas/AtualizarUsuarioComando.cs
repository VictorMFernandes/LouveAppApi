using LouveApp.Compartilhado.Comandos;
using FluentValidator;
using System.Collections.Generic;
using System.Linq;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Dominio.Sistema.Padroes;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Entradas
{
    public class AtualizarUsuarioComando : IComando
    {
        #region Propriedades Api

        internal string UsuarioLogadoId { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public IEnumerable<string> InstrumentosIds { get; set; }

        #endregion

        #region IComando

        public bool FoiValidado { get; set; }

        public bool Validar()
        {
            FoiValidado = true;
            var resultado = true;

            if (InstrumentosIds.Count() != InstrumentosIds.Distinct().Count())
            {
                _notificacoes.Add(new Notification("InstrumentosIds", "Não é possível vincular o mesmo instrumento mais de uma vez ao usuário"));
                resultado = false;
            }

            if (Nome != null)
            {
                NomeVo = new Nome(Nome);
                _notificacoes.AddRange(NomeVo.Notifications);
                resultado &= NomeVo.Valid;
            }

            if (Email != null)
            {
                EmailVo = new Email(Email);
                _notificacoes.AddRange(EmailVo.Notifications);
                resultado &= EmailVo.Valid;
            }

            if (Senha != null)
            {
                AutenticacaoVo = new Autenticacao(PadroesString.UsuarioLogin1, Senha, ConfirmacaoSenha);
                _notificacoes.AddRange(AutenticacaoVo.Notifications);
                resultado &= AutenticacaoVo.Valid;
            }

            return resultado;
        }

        private readonly List<Notification> _notificacoes = new List<Notification>();

        public IReadOnlyCollection<Notification> PegarNotificacoes() => _notificacoes;

        #endregion

        #region Value Objects

        internal Nome NomeVo { get; private set; }
        internal Email EmailVo { get; set; }
        internal Autenticacao AutenticacaoVo { get; set; }

        #endregion

        public void PegarUsuarioLogadoId(string usuarioLogadoId)
        {
            UsuarioLogadoId = usuarioLogadoId;
        }
    }
}
