﻿using System.Net;

namespace LouveApp.Compartilhado.Comandos.Genericos
{
    public class NaoEncontradoResultado : IComandoResultadoGenerico
    {
        public string Resultado { get; }

        public HttpStatusCode CodigoHttp => HttpStatusCode.NotFound;

        public bool Sucesso => false;

        public NaoEncontradoResultado(string descricao)
        {
            Resultado = descricao;
        }
    }
}
