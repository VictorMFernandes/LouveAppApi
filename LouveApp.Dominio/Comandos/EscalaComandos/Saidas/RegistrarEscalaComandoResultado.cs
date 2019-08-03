using LouveApp.Compartilhado.Comandos;
using System;

namespace LouveApp.Dominio.Comandos.EscalaComandos.Saidas
{
    public class RegistrarEscalaComandoResultado : IComandoResultado
    {
        #region Propriedades Api

        public string Id { get; }
        public DateTime Data { get; }
        public int QtdUsuarios { get; }

        #endregion

        #region Construtores

        public RegistrarEscalaComandoResultado(string id, DateTime data, int qtdUsuarios)
        {
            Id = id;
            Data = data;
            QtdUsuarios = qtdUsuarios;
        }

        #endregion
    }
}
