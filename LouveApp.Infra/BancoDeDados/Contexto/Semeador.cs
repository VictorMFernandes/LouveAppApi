using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Transacoes;
using LouveApp.Compartilhado.Padroes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LouveApp.Infra.BancoDeDados.Contexto
{
    public class SemeadorBd : ISemeadorBd
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IInstrumentoRepositorio _instrumentoRepo;
        private readonly IUow _uow;

        #region Entidades Semeadas

        private static Usuario _usuario1;
        private static Usuario _usuario2;
        private static Usuario _usuario3;

        private static Ministerio _ministerio1;
        private static Ministerio _ministerio2;
        private static Ministerio _ministerio3;

        private static Instrumento _instrumento1;
        private static Instrumento _instrumento2;
        private static Instrumento _instrumento3;
        private static Instrumento _instrumento4;
        private static Instrumento _instrumento5;
        private static Instrumento _instrumento6;

        private static Musica _musica1;
        private static Musica _musica2;

        #endregion

        public SemeadorBd(IUsuarioRepositorio usuarioRepo
            , IMinisterioRepositorio ministerioRepo
            , IInstrumentoRepositorio instrumentoRepo, IUow uow)
        {
            _usuarioRepo = usuarioRepo;
            _ministerioRepo = ministerioRepo;
            _instrumentoRepo = instrumentoRepo;
            _uow = uow;
        }

        public async void SemearBancoDeDados()
        {
            if (await _usuarioRepo.IdExiste(PadroesString.UsuarioId1))
                return;

            await SemearInstrumentos();

            SemearUsuarios();

            await _uow.Salvar();

            SemearMinisterios();

            await _uow.Salvar();
        }

        private async Task SemearInstrumentos()
        {
            _instrumento1 = new Instrumento(PadroesString.InstrumentoId1, new Nome(PadroesString.InstrumentoNome1));
            _instrumento2 = new Instrumento(PadroesString.InstrumentoId2, new Nome(PadroesString.InstrumentoNome2));
            _instrumento3 = new Instrumento(PadroesString.InstrumentoId3, new Nome(PadroesString.InstrumentoNome3));
            _instrumento4 = new Instrumento(PadroesString.InstrumentoId4, new Nome(PadroesString.InstrumentoNome4));
            _instrumento5 = new Instrumento(PadroesString.InstrumentoId5, new Nome(PadroesString.InstrumentoNome5));
            _instrumento6 = new Instrumento(PadroesString.InstrumentoId6, new Nome(PadroesString.InstrumentoNome6));

            Instrumento[] instrumentos =
            {
                _instrumento1,
                _instrumento2,
                _instrumento3,
                _instrumento4,
                _instrumento5,
                _instrumento6,
            };

            if (instrumentos.Length == await _instrumentoRepo.Contar()) return;

            _instrumentoRepo.CriarVarios(instrumentos);
        }

        private void SemearUsuarios()
        {
            _usuarioRepo.Criar(CriarUsuario1());
            _usuarioRepo.Criar(CriarUsuario2());
            _usuarioRepo.Criar(CriarUsuario3());
        }

        private void SemearMinisterios()
        {
            _ministerioRepo.Criar(CriarMinisterio1());
            _ministerioRepo.Criar(CriarMinisterio2());
            _ministerioRepo.Criar(CriarMinisterio3());
        }

        public static Usuario CriarUsuario1()
        {
            if (_usuario1 != null) return _usuario1;

            var nome = new Nome(PadroesString.UsuarioNome1);
            var email = new Email(PadroesString.UsuarioEmail1);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin1
                , PadroesString.SenhaValida
                , PadroesString.SenhaValida);

            _usuario1 = new Usuario(PadroesString.UsuarioId1, nome, email, autenticacao);

            _usuario1.Atualizar(null, new List<string>
            {
                PadroesString.InstrumentoId3,
                PadroesString.InstrumentoId5
            });

            return _usuario1;
        }

        public static Usuario CriarUsuario2()
        {
            if (_usuario2 != null) return _usuario2;

            var nome = new Nome(PadroesString.UsuarioNome2);
            var email = new Email(PadroesString.UsuarioEmail2);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin2
                , PadroesString.SenhaValida
                , PadroesString.SenhaValida);

            _usuario2 = new Usuario(PadroesString.UsuarioId2, nome, email, autenticacao);
            _usuario2.Atualizar(null, new List<string>
            {
                PadroesString.InstrumentoId6
            });

            return _usuario2;
        }

        public static Usuario CriarUsuario3()
        {
            if (_usuario3 != null) return _usuario3;

            var nome = new Nome(PadroesString.UsuarioNome3);
            var email = new Email(PadroesString.UsuarioEmail3);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin3
                , PadroesString.SenhaValida
                , PadroesString.SenhaValida);

            _usuario3 = new Usuario(PadroesString.UsuarioId3, nome, email, autenticacao);
            _usuario3.Atualizar(null, new List<string>
            {
                PadroesString.InstrumentoId1,
                PadroesString.InstrumentoId2,
                PadroesString.InstrumentoId3
            });

            return _usuario3;
        }

        public static Ministerio CriarMinisterio1()
        {
            if (_ministerio1 != null) return _ministerio1;

            var usuarioAdm = CriarUsuario1();
            var nome = new Nome(PadroesString.MinisterioNome1);
            _ministerio1 = new Ministerio(PadroesString.MinisterioId1, nome, usuarioAdm);

            var usuarioAdm2 = CriarUsuario2();
            _ministerio1.AdicionarAdministrador(usuarioAdm2);
            var usuario = CriarUsuario3();
            _ministerio1.AdicionarUsuario(usuario);

            _ministerio1.AdicionarMusica(usuarioAdm.Id, CriarMusica1());
            _ministerio1.AdicionarMusica(usuarioAdm.Id, CriarMusica2());

            _ministerio1.CriarEscala(usuarioAdm.Id
                , DateTime.Now.AddDays(3)
                , new string[] { usuarioAdm.Id, usuarioAdm2.Id }
                , new string[] { _musica1.Id, _musica2.Id });
            _ministerio1.CriarEscala(usuarioAdm.Id
                , DateTime.Now.AddDays(7)
                , new string[] { usuarioAdm.Id, usuarioAdm2.Id, usuario.Id }
                , new string[] { _musica2.Id });
            _ministerio1.CriarEscala(usuarioAdm2.Id
                , DateTime.Now.AddDays(6)
                , new string[] { usuarioAdm2.Id }
                , null);

            return _ministerio1;
        }

        public static Ministerio CriarMinisterio2()
        {
            if (_ministerio2 != null) return _ministerio2;

            var usuarioAdm = CriarUsuario1();
            var nome = new Nome(PadroesString.MinisterioNome2);
            _ministerio2 = new Ministerio(PadroesString.MinisterioId2, nome, usuarioAdm);

            var usuario = CriarUsuario3();
            _ministerio2.AdicionarUsuario(usuario);

            _ministerio2.CriarEscala(usuarioAdm.Id
                , DateTime.Now.AddDays(3), new string[] { usuarioAdm.Id, usuario.Id }, null);

            return _ministerio2;
        }

        public static Ministerio CriarMinisterio3()
        {
            if (_ministerio3 != null) return _ministerio3;

            var usuarioAdm = CriarUsuario3();
            var nome = new Nome(PadroesString.MinisterioNome3);
            _ministerio3 = new Ministerio(PadroesString.MinisterioId3, nome, usuarioAdm);

            var usuario = CriarUsuario1();
            _ministerio3.AdicionarUsuario(usuario);

            _ministerio3.CriarEscala(usuarioAdm.Id
                , DateTime.Now.AddDays(10), new string[] { usuario.Id }, null);

            return _ministerio3;
        }

        public static Musica CriarMusica1()
        {
            if (_musica1 != null) return _musica1;

            var nome = new Nome(PadroesString.MusicaNome1);
            var referencia = new Link(PadroesString.MusicaReferencia1);

            _musica1 = new Musica(PadroesString.MusicaId1, nome, referencia);

            return _musica1;
        }

        public static Musica CriarMusica2()
        {
            if (_musica2 != null) return _musica2;

            var nome = new Nome(PadroesString.MusicaNome2);
            var referencia = new Link(PadroesString.MusicaReferencia2);

            _musica2 = new Musica(PadroesString.MusicaId2, nome, referencia);

            return _musica2;
        }
    }
}
