﻿using LouveApp.Dominio.Entidades;
using LouveApp.Dominio.Repositorios;
using LouveApp.Dominio.Servicos;
using LouveApp.Dominio.ValueObjects;
using LouveApp.Infra.BancoDeDados.Transacoes;
using LouveApp.Compartilhado.Padroes;
using System.Collections.Generic;

namespace LouveApp.Infra.BancoDeDados.Contexto
{
    public class SemeadorBd : ISemeadorBd
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IMinisterioRepositorio _ministerioRepo;
        private readonly IInstrumentoRepositorio _instrumentoRepo;
        private readonly IUow _uow;

        private static Usuario _usuario;
        private static Instrumento _instrumento1;
        private static Instrumento _instrumento2;

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
            if (await _usuarioRepo.IdExiste(PadroesString.UsuarioId))
                return;

            SemearInstrumentos();

            _usuario = CriarUsuario();

            _usuario.Atualizar(null, new List<string> { PadroesString.InstrumentoId1, PadroesString.InstrumentoId2 });

            _usuarioRepo.Criar(_usuario);

            await _uow.Salvar();

            _ministerioRepo.Criar(CriarMinisterio());

            await _uow.Salvar();
        }

        private async void SemearInstrumentos()
        {
            _instrumento1 = new Instrumento(PadroesString.InstrumentoId1, new Nome(PadroesString.InstrumentoNome1));
            _instrumento2 = new Instrumento(PadroesString.InstrumentoId2, new Nome(PadroesString.InstrumentoNome2));

            Instrumento[] instrumentos =
            {
                _instrumento1,
                _instrumento2,
                new Instrumento(new Nome("Baixo Elétrico")),
                new Instrumento(new Nome("Teclado")),
                new Instrumento(new Nome("Violino")),
                new Instrumento(new Nome("Bateria")),
                new Instrumento(new Nome("Saxofone")),
                new Instrumento(new Nome("Vocal"))
            };

            if (instrumentos.Length == await _instrumentoRepo.Contar()) return;

            _instrumentoRepo.CriarVarios(instrumentos);
        }

        public static Usuario CriarUsuario()
        {
            if (_usuario != null) return _usuario;

            var nome = new Nome(PadroesString.UsuarioNome);
            var email = new Email(PadroesString.UsuarioEmail);
            var autenticacao = new Autenticacao(PadroesString.UsuarioLogin
                , PadroesString.UsuarioSenha
                , PadroesString.UsuarioSenha);

            return new Usuario(PadroesString.UsuarioId, nome, email, autenticacao);
        }

        public static Ministerio CriarMinisterio()
        {
            _usuario = _usuario ?? CriarUsuario();

            var nome = new Nome(PadroesString.MinisterioNome);
            return new Ministerio(PadroesString.MinisterioId, nome, _usuario);
        }
    }
}
