using LouveApp.Compartilhado.Entidades;
using FluentValidator.Validation;
using System.Text;
using LouveApp.Compartilhado.Padroes;

namespace LouveApp.Dominio.ValueObjects
{
    public sealed class Autenticacao : ValueObject
    {
        #region Propriedades

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public bool Ativo { get; private set; }

        #endregion

        #region Construtores

        private Autenticacao() { }

        public Autenticacao(string login, string senha, string confirmacaoSenha)
        {
            Login = login;
            Senha = EncriptarSenha(senha);
            Ativo = true;

            AddNotifications(new ValidationContract()
                .AreEquals(senha, confirmacaoSenha, "Senha", "As senhas não coincidem")
            );

            Validar();
        }

        #endregion

        public bool Autenticar(string login, string senha)
        {
            if (Login == login && Senha == EncriptarSenha(senha))
                return true;

            AddNotification("Usuario", "Login ou senha inválidos");
            return false;
        }

        private static string EncriptarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                return string.Empty;

            var password = (senha + "|f168wa7-j8k64hj-6v48-qw89t7-n31x315fq78w66-11");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(password));
            var sbString = new StringBuilder();

            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

        #region Métodos de Sobrescrita

        public override string ToString()
        {
            return Login;
        }

        protected override void Validar()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(Login
                            , PadroesTamanho.MinLogin
                            , "Login"
                            , $"O login deve conter no mínimo {PadroesTamanho.MinLogin} caracteres")
                .HasMaxLen(Login
                            , PadroesTamanho.MaxLogin
                            , "Login"
                            , $"O login deve conter no máximo {PadroesTamanho.MaxLogin} caracteres")
            );
        }

        #endregion
    }
}
