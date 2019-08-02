namespace LouveApp.Dominio.Testes.Gerenciadores
{
    internal interface IGerenciadorTestes
    {
        void ValidarComandosAoExecutalos();
        void RetornarNullQuandoComandoInvalido();
    }
}
