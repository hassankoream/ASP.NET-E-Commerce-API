using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstractions;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region  Add services to the container.
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //AddProfile
            //builder.Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()));
            builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();


            #endregion


            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectOfDataSeeding.DataSeedAsync(); 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //for files in your projects
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
