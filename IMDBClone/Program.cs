
using IMDBClone.Data;
using IMDBClone.Helpers;
using IMDBClone.Models;
using IMDBClone.Repos;
using IMDBClone.Services;
using IMDBClone.Services.Interfaces;
using IMDBClone.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IMDBClone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbOptionsBuilder.UseSqlServer(connectionString);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ApplicationDbContext>(cfg=> cfg.UseSqlServer(
                connectionString));

            //JWT
            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                    };
                });

            //Auth
            builder.Services.AddScoped<IAuthentication, Authentication>(x => new Authentication(
                    new UserManager(new UserRepo(new ApplicationDbContext(dbOptionsBuilder.Options))),
                    new RoleManager(new UserRolesRepo(new ApplicationDbContext(dbOptionsBuilder.Options))
                        , new RoleRepo(new ApplicationDbContext(dbOptionsBuilder.Options))),
                    new PasswordManager(),
                    new JWTHelper(jwtOptions)
                )
            );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(oprtions =>
                {
                    oprtions.SwaggerEndpoint("/openapi/v1.json", "Demo API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            

            app.MapControllers();

            new DataSeeding(new RoleRepo(
                new ApplicationDbContext(dbOptionsBuilder.Options)
                )).SeedAsync().Wait();

            app.Run();
        }
    }
}
