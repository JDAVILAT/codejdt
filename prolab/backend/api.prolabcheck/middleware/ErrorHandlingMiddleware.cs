﻿using System.Net;
using System.Text.Json;

namespace prolabcheck.api.middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new { message = "Se ha producido un error inesperado.", details = exception.Message };
            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
