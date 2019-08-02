using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace LouveApp.Dominio.Testes.Falsos
{
    internal class ConfigurationFalso : IConfiguration
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren() { throw new NotImplementedException(); }

        public IChangeToken GetReloadToken() { throw new NotImplementedException(); }

        public IConfigurationSection GetSection(string key) { throw new NotImplementedException(); }
    }
}
