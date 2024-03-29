﻿namespace LouveApp.Dominio.Sistema.Padroes
{
    public class PadroesMensagens
    {
        public const string UsuarioNaoEncontrado = "Usuário não encontrado";
        public const string UsuarioOuSenhaInvalidos = "Usuário ou senha inválidos";
        public const string SenhasNaoCoincidem = "As senhas não coincidem";
        /// <summary>
        /// {0} Tamanho mínimo das senhas
        /// </summary>
        public const string SenhaMinTamanho = "A senha deve conter no mínimo {0} caracteres";
        /// <summary>
        /// {0} Nome do ministério
        /// </summary>
        public const string UsuarioNaoVinculado = "Usuário não vinculado ao ministério {0}";

        public const string UsuarioSemPermissao = "O Usuário não tem permissão para realizar essa ação";
        /// <summary>
        /// {0} Nome do usuário
        /// </summary>
        public const string UsuarioSemDispositivos = "O usuário {0} ainda não possui dispositivos cadastrados";

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
        public const string MusicaNaoEncontrada = "Música não encontrada";

        /// <summary>
        /// {0} Tamanho mínimo das url's
        /// </summary>
        public const string UrlMinTamanho = "A url deve conter no mínimo {0} caracteres";
        /// <summary>
        /// {0} Tamanho máximo das url's
        /// </summary>
        public const string UrlMaxTamanho = "A url deve conter no máximo {0} caracteres";

        /// <summary>
        /// {0} Tamanho mínimo dos nomes
        /// </summary>
        public const string NomeMinTamanho = "O nome deve conter no mínimo {0} caracteres";
        /// <summary>
        /// {0} Tamanho máximo dos nomes
        /// </summary>
        public const string NomeMaxTamanho = "O nome deve conter no máximo {0} caracteres";

        public const string PropriedadeNaoPodeSerNula = "A propriedade não pode ser nula";
        /// <summary>
        /// {0} Tamanho máximo dos tons
        /// </summary>
        public const string TomMaxTamanho = "O tom deve conter no máximo {0} caracteres";
        /// <summary>
        /// {0} Tamanho máximo dos bpms
        /// </summary>
        public const string BpmMaxTamanho = "O bpm deve conter no máximo {0} caracteres";
        /// <summary>
        /// {0} Tamanho máximo das classificações
        /// </summary>
        public const string ClassificacaoMaxTamanho = "A classificação deve conter no máximo {0} caracteres";

        /// <summary>
        /// {0} Nome do ingressante
        /// </summary>
        public const string IngressoEmMinisterioCorpo = "Dê boas vindas a {0}";

        public const string ParametrosInvalidos = "Os parâmetros enviados não são válidos";

        /// <summary>
        /// {0} Data da escala
        /// </summary>
        public const string IngressoEmEscalaCorpo = "Você foi condidado para uma escala no dia {0}";
    }
}
