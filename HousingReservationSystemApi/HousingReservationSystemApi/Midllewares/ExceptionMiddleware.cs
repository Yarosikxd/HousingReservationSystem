﻿using HousingReservationSystemApi.Contracts;
using System.Net;

namespace HousingReservationSystemApi.Midllewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new ErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
