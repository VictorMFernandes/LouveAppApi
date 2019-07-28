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
                    , "Nome"
                    , $"O nome deve conter no mínimo {PadroesTamanho.MinNome} caracteres")
                .HasMaxLen(Texto, PadroesTamanho.MaxNome
                    , "Nome"
                    , $"O nome deve conter no máximo {PadroesTamanho.MaxNome} caracteres")
            );
        }

        #endregion
    }
}
