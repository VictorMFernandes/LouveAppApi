using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Enums;
using Microsoft.Extensions.Configuration;

namespace LouveApp.Dominio.Sistema
{
    public class Configuracoes
    {
        public static string NomeAplicacao;
        public static string VersaoAplicacao;
        public static string ConnString;
        public static bool EmDesenvolvimento;
        public const string BancoTestesCaminho = @"..\..\..\Teste.db";
        public static string ConnStringTestes = $"Data Source={BancoTestesCaminho}";
        public static string TokenSecreto;
        public static double PrescricaoTokenDias;

        public static void InicializarConfiguracoes(IConfiguration config, bool emDesenvolvimento)
        {
            EmDesenvolvimento = emDesenvolvimento;
            ConnString = config.GetConnectionString(
                EmDesenvolvimento ? EConfigSecao.ConexaoBdDev.EnumTextos().Nome 
                                    : EConfigSecao.ConexaoBd.EnumTextos().Nome);
            NomeAplicacao = config
                .GetSection(EConfigSecao.NomeAplicacao.EnumTextos().Nome)
                .Value;
            VersaoAplicacao = config
                .GetSection(EConfigSecao.Versao.EnumTextos().Nome)
                .Value;
            TokenSecreto = config
                .GetSection(EConfigSecao.TokenSecreto.EnumTextos().Nome)
                .Value;
            PrescricaoTokenDias = double.Parse(config
                .GetSection(EConfigSecao.PrescricaoTokenDias.EnumTextos().Nome)
                .Value);
        }
    }
}
