using LouveApp.Compartilhado.Entidades;
using FluentValidator.Validation;

namespace LouveApp.Dominio.ValueObjects
{
    public sealed class Email : ValueObject
    {
        #region Propriedades

        public string Endereco { get; }

        #endregion

        #region Construtores

        public Email(string endereco)
        {
            Endereco = endereco;

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita
        public override string ToString()
        {
            return Endereco;
        }

        protected override void Validar()
        {
            AddNotifications(new ValidationContract()
                .IsEmail(Endereco, "Email", "E-mail inválido")
            );
        }

        #endregion
    }
}
