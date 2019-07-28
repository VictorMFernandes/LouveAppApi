using LouveApp.Compartilhado.Extensoes;
using FluentValidator.Validation;

namespace LouveApp.Dominio.ValueObjects
{
    public class Cpf: Documento
    {
        #region Construtores

        public Cpf(string numero) : base(numero) { }

        #endregion

        #region Métodos de Sobrescrita

        protected sealed override void Validar()
        {
            AddNotifications(new ValidationContract()
                .IsTrue(Numero.CpfValido(), "Cpf", "Documento inválido")
            );
        }

        #endregion
    }
}
