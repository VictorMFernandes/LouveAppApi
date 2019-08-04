using FluentValidator.Validation;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Compartilhado.Padroes;

namespace LouveApp.Dominio.ValueObjects
{
    public class Link : ValueObject
    {
        #region Propriedades

        public string Url { get; }

        #endregion

        #region Construtores

        public Link(string url)
        {
            Url = url;
            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Url;
        }

        protected override void Validar()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(Url, PadroesTamanho.MinUrl
                    , nameof(Url)
                    , string.Format(PadroesMensagens.UrlMinTamanho, PadroesTamanho.MinUrl))
                .HasMaxLen(Url, PadroesTamanho.MaxUrl
                    , nameof(Url)
                    , string.Format(PadroesMensagens.UrlMaxTamanho, PadroesTamanho.MaxUrl))
            );
        }

        #endregion
    }
}
