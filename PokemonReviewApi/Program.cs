using Microsoft.EntityFrameworkCore;
using PokemonReviewApi.Data;
using PokemonReviewApi.Interface;
using PokemonReviewApi.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace PokemonReviewApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //seed data before it starts
            builder.Services.AddTransient<Seed>();

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //tell api that we have done dependency injection
            builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();





            builder.Services.AddDbContext<ReviewDbContext>(opts =>
            {


                var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];

                opts.UseSqlServer(defaultConn);

            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedDataContext();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}