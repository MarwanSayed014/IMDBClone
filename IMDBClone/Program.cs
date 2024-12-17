
using IMDBClone.Data;
using IMDBClone.Models;
using IMDBClone.Repos;
using Microsoft.EntityFrameworkCore;

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
