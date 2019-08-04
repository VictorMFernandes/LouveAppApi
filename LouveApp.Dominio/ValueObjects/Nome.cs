using LouveApp.Compartilhado.Entidades;
using LouveApp.Compartilhado.Padroes;
using FluentValidator.Validation;

namespace LouveApp.Dominio.ValueObjects
{
    public sealed class Nome : ValueObject
    {
        #region Propriedades

        public string Texto { get; }

        #endregion

        #region Construtores

        public Nome(string texto)
        {
            Texto = texto;
            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Texto;
        }

        protected override void Validar()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(Texto, PadroesTamanho.MinNome
                    , nameof(Nome)
                    , string.Format(PadroesMensagens.NomeMinTamanho, PadroesTamanho.MinNome))
                .HasMaxLen(Texto, PadroesTamanho.MaxNome
                    , nameof(Nome)
                    , string.Format(PadroesMensagens.NomeMaxTamanho, PadroesTamanho.MaxNome))
            );
        }

        #endregion
    }
}
