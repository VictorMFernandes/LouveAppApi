using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace LouveApp.Compartilhado.Extensoes
{
    public static class ObjectExtensoes
    {
        public static IEnumerable<PropertyInfo> PegarColecoes(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var tipo = obj.GetType();
            var resultado = new List<PropertyInfo>();

            foreach (var prop in tipo.GetProperties())
            {
                if (!typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)) continue;
                var get = prop.GetGetMethod();
                if (get.IsStatic || get.GetParameters().Length != 0) continue;

                resultado.Add(prop);
            }
            return resultado;
        }
    }
}
