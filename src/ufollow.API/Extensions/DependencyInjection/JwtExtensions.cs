using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ufollow.API.Authorization;

namespace ufollow.API.Extensions.DependencyInjection
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, Action<ApiTokenOptions> configure)
        {
            services.AddTransient<ApiTokenOptions>();
            services.Configure<ApiTokenOptions>(configure);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var apiTokenOptions = new ApiTokenOptions();
                var token = new ApiToken(apiTokenOptions);

                configure(apiTokenOptions);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = apiTokenOptions.Issuer,
                    ValidAudience = apiTokenOptions.Audience,
                    IssuerSigningKey = token.SecurityKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
