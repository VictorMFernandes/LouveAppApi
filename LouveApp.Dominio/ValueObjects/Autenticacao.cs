﻿using FluentValidator.Validation;
using LouveApp.Compartilhado.Entidades;
using LouveApp.Compartilhado.Padroes;
using LouveApp.Dominio.Sistema.Padroes;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                .AreEquals(senha, confirmacaoSenha
                    , nameof(Senha), PadroesMensagens.SenhasNaoCoincidem)
                .HasMinLen(senha, PadroesTamanho.MinSenha
                    , nameof(Senha), string.Format(PadroesMensagens.SenhaMinTamanho, PadroesTamanho.MinSenha))
            );

            Validar();
        }

        #endregion

        public static string EncriptarSenha(string senha)
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

        public static string GerarToken(string usuarioId, string login, string tokenSecreto, double prescricaoDias)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId),
                new Claim(ClaimTypes.Name, login),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(tokenSecreto));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime
                    .Now
                    .AddDays(prescricaoDias),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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
