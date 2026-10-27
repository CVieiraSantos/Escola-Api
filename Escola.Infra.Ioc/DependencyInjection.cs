using Escola.Application.Interfaces;
using Escola.Application.Services;
using Escola.Domain.Account;
using Escola.Domain.Interfaces;
using Escola.Infra.Data.Data;
using Escola.Infra.Data.Identity;
using Escola.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Escola.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(Context).Assembly.FullName));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"], //Quem emitiu o token, a sua própria API.
                    ValidAudience = configuration["Jwt:Audience"], //Pra quem esse token foi criado, pro seu frontend, quem consome esse token.
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
            services.AddScoped<INotaRepository, NotaRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<IMatriculaService, MatriculaService>();
            services.AddScoped<INotaService, NotaService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();
            return services;
        }
    }
}
