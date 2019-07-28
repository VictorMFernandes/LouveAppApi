using LouveApp.Compartilhado.Comandos;
using LouveApp.Compartilhado.Extensoes;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Entradas;
using LouveApp.Dominio.Comandos.AutenticacaoComandos.Saidas;
using LouveApp.Dominio.Enums;
using LouveApp.Dominio.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LouveApp.Compartilhado.Padroes;

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
            var usuario = await _usuarioRepo.PegarPorLogin(comando.Login);

            // Confere a senha
            if (usuario == null || !usuario.Autenticacao.Autenticar(comando.Login, comando.Senha))
            {
                AddNotification("Usuario", PadroesMensagens.UsuarioOuSenhaInvalidos);
                return null;
            }

            usuario.AtualizarDtUltimaAtividade();

            return new AutenticarUsuarioComandoResultado(usuario
                                                        , GerarToken(usuario.Id.ToString()
                                                        , usuario.Autenticacao.Login));
        }
        
        #endregion

        private string GerarToken(string usuarioId, string login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId),
                new Claim(ClaimTypes.Name, login),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
               .GetBytes(_config.GetSection(EConfigSecao.TokenSecreto.EnumTextos().Nome).Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime
                            .Now
                            .AddDays(double.Parse(_config
                                                    .GetSection(EConfigSecao.PrescricaoTokenDias.EnumTextos().Nome)
                                                    .Value)),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
