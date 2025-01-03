
using IMDBClone.Data;
using IMDBClone.EventHandlers;
using IMDBClone.Helpers;
using IMDBClone.Helpers.Interfaces;
using IMDBClone.Models;
using IMDBClone.Repos;
using IMDBClone.Repos.Interfaces;
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

            //Helpers
            builder.Services.AddScoped<JWTHelper>();

            //EventHandlers
            builder.Services.AddScoped<SeriesEventHandler>();

            //Repos
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IUserRolesRepo, UserRolesRepo>();
            builder.Services.AddScoped<IRoleRepo, RoleRepo>();
            builder.Services.AddScoped<IActorRepo, ActorRepo>();
            builder.Services.AddScoped<IDirectorRepo, DirectorRepo>();
            builder.Services.AddScoped<IProducerRepo, ProducerRepo>();
            builder.Services.AddScoped<IGenreRepo, GenreRepo>();
            builder.Services.AddScoped<IMovieGenresRepo, MovieGenresRepo>();
            builder.Services.AddScoped<IMovieRepo, MovieRepo>();
            builder.Services.AddScoped<ISeriesGenresRepo, SeriesGenresRepo>();
            builder.Services.AddScoped<ISeriesRepo, SeriesRepo>();
            builder.Services.AddScoped<ISeasonRepo, SeasonRepo>();
            builder.Services.AddScoped<IEpisodeRepo, EpisodeRepo>();

            //Managers
            builder.Services.AddScoped<IPasswordManager, PasswordManager>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();
            builder.Services.AddScoped<IActorManager, ActorManager>();
            builder.Services.AddScoped<IDirectorManager, DirectorManager>();
            builder.Services.AddScoped<IProducerManager, ProducerManager>();
            builder.Services.AddScoped<IGenreManager, GenreManager>();
            builder.Services.AddScoped<IMovieManager, MovieManager>();
            builder.Services.AddScoped<ISeriesManager, SeriesManager>();
            builder.Services.AddScoped<ISeasonManager, SeasonManager>();
            builder.Services.AddScoped<IEpisodeManager, EpisodeManager>();

            //Auth
            builder.Services.AddScoped<IAuthentication, Authentication>();

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
            app.UseStaticFiles();
            app.Run();
        }
    }
}
