namespace LouveApp.Dominio.Sistema
{
    public class Configuracoes
    {
        public static string ConnString;
        public const string BancoTestesCaminho = @"..\..\..\BancoDeDados\Teste.db";
        public static string ConnStringTestes = $"Data Source={BancoTestesCaminho}";
    }
}
