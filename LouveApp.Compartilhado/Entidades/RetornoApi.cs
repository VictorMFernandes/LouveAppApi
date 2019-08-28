using Newtonsoft.Json;

namespace LouveApp.Compartilhado.Entidades
{
    internal class RetornoApi
    {
        #region Propriedades

        public bool Sucesso { get; private set; }
        public object Resultado { get; private set; }
        public object[] Erros { get; private set; }

        #endregion

        #region Construtores

        public RetornoApi(bool sucesso, object resultado, object[] erros)
        {
            Sucesso = sucesso;
            Resultado = resultado;
            Erros = erros;
        }

        public RetornoApi(bool sucesso, object resultado, object erro)
        {
            Sucesso = sucesso;
            Resultado = resultado;
            Erros = new[] { erro };
        }

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
