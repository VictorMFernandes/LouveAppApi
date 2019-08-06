using LouveApp.Compartilhado.Comandos;
using LouveApp.Dominio.ValueObjects;
using FluentValidator;
using System.Collections.Generic;

namespace LouveApp.Dominio.Comandos.UsuarioComandos.Entradas
{
    public class RegistrarUsuarioComando : IComando
    {
        #region Propriedades Api

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }

        #endregion

        #region Construtores

        public RegistrarUsuarioComando(string nome, string email, string senha, string confirmacaoSenha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }

        #endregion

        #region IComando

        public bool FoiValidado { get; private set; }

        public bool Validar()
        {
            NomeVo = new Nome(Nome);
            EmailVo = new Email(Email);
            AutenticacaoVo = new Autenticacao(Email, Senha, ConfirmacaoSenha);

            FoiValidado = true;

            return NomeVo.Valid && EmailVo.Valid && AutenticacaoVo.Valid;
        }

        public IReadOnlyCollection<Notification> PegarNotificacoes()
        {
            var resultado = new List<Notification>();

            resultado.AddRange(NomeVo.Notifications);
            resultado.AddRange(EmailVo.Notifications);
            resultado.AddRange(AutenticacaoVo.Notifications);

            return resultado;
        }

        #endregion

        #region Value Objects

        internal Nome NomeVo { get; set; }
        internal Email EmailVo { get; set; }
        internal Autenticacao AutenticacaoVo { get; set; }

        #endregion
    }
}
