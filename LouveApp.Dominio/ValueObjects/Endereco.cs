using LouveApp.Compartilhado.Entidades;
using FluentValidator.Validation;

namespace LouveApp.Dominio.ValueObjects
{
    internal class Endereco : ValueObject
    {
        #region Propriedades

        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        
        #endregion

        #region Construtores

        public Endereco(string logradouro, string complemento, string bairro, string cidade, string estado, string pais, string cep)
        {
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;

            Validar();
        }

        #endregion

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return $"{Logradouro}, {Bairro} - {Estado}";
        }

        protected override void Validar()
        {
            AddNotifications(new ValidationContract()
                .IsNotNullOrEmpty(Logradouro, "Logradouro", "Logradouro é um campo obrigatório")
                .HasMaxLen(Logradouro, 60, "Estado", "O Logradouro deve ter no máximo 60 caracteres")

                .HasMaxLen(Complemento, 60, "Complemento", "O Complemento deve ter no máximo 60 caracteres")

                .IsNotNullOrEmpty(Bairro, "Bairro", "Bairro é um campo obrigatório")
                .HasMaxLen(Bairro, 30, "Bairro", "O nome do Bairro deve ter no máximo 30 caracteres")

                .IsNotNullOrEmpty(Cidade, "Cidade", "Cidade é um campo obrigatório")
                .HasMaxLen(Cidade, 30, "Cidade", "O nome da Cidade deve ter no máximo 30 caracteres")

                .IsNotNullOrEmpty(Estado, "Estado", "Estado é um campo obrigatório")
                .HasMaxLen(Estado, 30, "Estado", "O nome do Estado deve ter no máximo 30 caracteres")

                .IsNotNullOrEmpty(Pais, "Pais", "Pais é um campo obrigatório")
                .HasMaxLen(Pais, 40, "Pais", "O nome do Pais deve ter no máximo 40 caracteres")

                .IsNotNullOrEmpty(Cep, "Cep", "Cep é um campo obrigatório")
                .HasMaxLen(Cep, 8, "Cep", "O Cep deve ter no máximo 8 caracteres")
            );
        }

        #endregion
    }
}
