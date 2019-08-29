using FluentValidator;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Dominio.Sistema.Padroes;
using LouveApp.Dominio.ValueObjects;

namespace LouveApp.Dominio.Entidades
{
    public class Dispositivo : Entidade
    {
        #region Propriedades

        public string Token { get; private set; }
        public Nome Nome { get; private set; }
        public Usuario Usuario { get; private set; }

        #endregion

        #region Construtores

        private Dispositivo()
        {

        }

        public Dispositivo(string token, Nome nome)
        {
            Token = token;
            Nome = nome;

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Nome.ToString();
        }

        protected override void InicializarColecoes()
        {
        }

        protected override void Validar()
        {
            if (Nome != null)
                AddNotifications(Nome);
            else
                AddNotification(new Notification(nameof(Nome)
                    , PadroesMensagens.PropriedadeNaoPodeSerNula));
        }

        #endregion
    }
}
