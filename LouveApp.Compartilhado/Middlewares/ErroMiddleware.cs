﻿using LouveApp.Compartilhado.Entidades;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LouveApp.Compartilhado.Middlewares
{
    public class ErroMiddleware
    {
        private readonly RequestDelegate _next;

        public ErroMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var resultado = new RetornoApi(false, null, new
            {
                message = exception.Message,
                erroInterno = exception.InnerException?.Message,
                stackTrace = exception.StackTrace
            });

            return context.Response.WriteAsync(resultado.ToString());
        }
    }
}
