﻿namespace LouveApp.Compartilhado.Padroes
{
    public class PadroesMensagens
    {
        public const string UsuarioNaoEncontrado = "Usuário não encontrado";
        public const string UsuarioOuSenhaInvalidos = "Usuário ou senha inválidos";
        /// <summary>
        /// {0} Nome do ministério
        /// </summary>
        public const string UsuarioNaoVinculado = "Usuário não vinculado ao ministério {0}";

        public const string UsuarioSemPermissao = "O Usuário não tem permissão para realizar essa ação";

        public const string MinisterioNaoEncontrado = "Ministério não encontrado";
        public const string LinkInvalido = "Link convite inválido";
        public const string UsuariosDuplicadosMinisterio = "Ministério possui usuários duplicados";
        public const string MinisterioDeveTerAdministrador = "Ministério deve possuir ao menos um administrador";

        /// <summary>
        /// {0} E-mail que está em uso
        /// </summary>
        public const string EmailEmUso = "O E-mail {0} já está em uso";

        public const string InstrumentoNaoEncontrado = "Não foi possível encontrar algum(ns) " +
                                                       "instrumento(s) selecionado(s)";

        public const string EscalaNaoEncontrada = "Escala não encontrada";
    }
}
