using LouveApp.Compartilhado.Entidades;

namespace LouveApp.Dominio.ValueObjects
{
    public abstract class Documento : ValueObject
    {
        #region Propriedades

        public string Numero { get; }
        
        #endregion

        #region Construtores

        protected Documento(string numero)
        {
            Numero = numero;

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Numero;
        }

        #endregion
    }
}
