﻿using HousingReservationSystemApi.Contracts.Users;
using HousingReservationSystemApplication.Services;

namespace HousingReservationSystemApi.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }


        private static async Task<IResult> Register(RegisterUserRequest request, UserService userService)
        {
            userService.Register(request.UserName, request.Email, request.Password);

            return Results.Ok();
        }

        private static async Task<IResult> Login(LoginUserRequest request, UserService userService, HttpContext context)
        {
            var token = await userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }
    }
}
