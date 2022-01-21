﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common;
using VeterinaryAPI.DTOs.User;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Infastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ApplicationSettings appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<ApplicationSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context
                .Request
                .Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();

            if (token != null)
            {
                this.AttachUserToContext(context, userService, token);
            }

            await next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.JwtApiSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                var user = userService.GetUserByIdAsync<GetUserForSessionDTO>(userId).GetAwaiter().GetResult();

                context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}

