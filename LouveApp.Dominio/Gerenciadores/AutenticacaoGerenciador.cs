using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Enums;
using LouveApp.Dominio.Repositorios;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Entidades;

namespace LouveApp.Dominio.Gerenciadores
{
    public class AutenticacaoGerenciador : Gerenciador
        , IComandoGerenciador<AutenticarUsuarioComando>
    {

        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IConfiguration _config;

        public AutenticacaoGerenciador(IUsuarioRepositorio usuarioRepo, IConfiguration config)
        {
            _usuarioRepo = usuarioRepo;
            _config = config;
        }

        #region Comandos

        public async Task<IComandoResultado> Executar(AutenticarUsuarioComando comando)
        {
            if (!ValidarComando(comando))
                return null;

            // Pega usuario pelo login
            var usuario = await _usuarioRepo.PegarAutenticado(comando.Login, Autenticacao.EncriptarSenha(comando.Senha));

            // Confere a senha
            if (usuario == null)
            {
                AddNotification("Usuario", PadroesMensagens.UsuarioOuSenhaInvalidos);
                return null;
            }

            usuario.AdicionarDispositivo(new Dispositivo(comando.DispositivoToken, new Nome(comando.DispositivoNome)));

            // Validar entidade
            AddNotifications(usuario);

            if (Invalid) return null;

            _usuarioRepo.Atualizar(usuario);

            var autenticacaoToken = Autenticacao.GerarToken(usuario.Id, comando.Login
                , _config.GetSection(EConfigSecao.TokenSecreto.EnumTextos().Nome).Value
                , double.Parse(_config.GetSection(EConfigSecao.PrescricaoTokenDias.EnumTextos().Nome).Value));
            var comandoResultado = new AutenticarUsuarioComandoResultado(autenticacaoToken, usuario, usuario.PegarMinisterios());

            return comandoResultado;
        }
        
        #endregion
    }
}
