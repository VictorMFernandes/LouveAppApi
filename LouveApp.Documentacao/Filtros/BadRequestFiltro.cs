using LouveApp.Compartilhado.Entidades;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LouveApp.Documentacao.Filtros
{
    // Adiciona o erro BadRequest(400) à documentação em todos os endpoints
    public class BadRequestFiltro : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.DeclaringType?.BaseType == typeof(ControladorApi))
            {
                operation.Responses.Add("400", new Response { Description = "Se alguma propriedade enviada não for válida." });
            }
        }
    }
}
