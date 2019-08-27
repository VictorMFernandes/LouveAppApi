using LouveApp.Dominio.Sistema;
using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace LouveApp.Dal.Contexto
{
    internal class DapperUtil : IDisposable
    {
        internal DbConnection Conexao;

        public DapperUtil()
        {
            if (Configuracoes.EmDesenvolvimento)
                Conexao = new SqliteConnection(Configuracoes.ConnString);
            else
                Conexao = new SqlConnection(Configuracoes.ConnString);

            Conexao.Open();
        }

        public void Dispose()
        {
        }
    }
}
