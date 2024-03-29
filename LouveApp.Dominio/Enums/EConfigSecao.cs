﻿using LouveApp.Compartilhado.Util;

namespace LouveApp.Dominio.Enums
{
    public enum EConfigSecao
    {
        [EnumTextos("AppSettings:TokenSecreto")]
        TokenSecreto,
        [EnumTextos("AppSettings:PrescricaoTokenDias")]
        PrescricaoTokenDias,
        [EnumTextos("AppSettings:NomeAplicacao")]
        NomeAplicacao,
        [EnumTextos("AppSettings:Versao")]
        Versao,
        [EnumTextos("ConexaoBd")]
        ConexaoBd,
        [EnumTextos("ConexaoBdDev")]
        ConexaoBdDev,
        [EnumTextos("SendGrid:ApiKey")]
        SendGridApiKey
    }
}
