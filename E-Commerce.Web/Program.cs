using Domain.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using ServicesAbstractions;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region  Add services to the container.
          

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwagerServices();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            //this to replace the registration of the three other classes.
            builder.Services.AddInfrastructureServices(builder.Configuration);
            //builder.Services.AddDbContext<StoreDbContext>(Options =>
            //{
            //    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            //builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //AddProfile
            //builder.Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()));

            //replace those two with new class of the registrations
            builder.Services.AddApplicationService();
            //builder.Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            //builder.Services.AddScoped<IServiceManager, ServiceManager>();

            //replace ApiBehaviorOptions with this for the application
            builder.Services.AddWebApplicationServices();
            //builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            //{
            //    //Cleaner
            //    Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;


            //    //Old
            //    //(Context) =>
            //    //{
            //    //    var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
            //    //    .Select(M => new ValidationError()
            //    //    {
            //    //        Field = M.Key,
            //    //        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
            //    //    });
            //    //    var Response = new ValidationErrorToReturn()
            //    //    {
            //    //        validationErrors = Errors,
            //    //    };
            //    //    return new BadRequestObjectResult(Response);
            //    //};
            //});

            builder.Services.AddJwtServices(builder.Configuration);


            #endregion


            var app = builder.Build();

            //using var Scope = app.Services.CreateScope();
            //var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            //ObjectOfDataSeeding.DataSeedAsync();

            app.SeedDataBaseAsync();

            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.

            //Old Method
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("Waiting Response");
            //    Console.WriteLine(RequestContext.Response.Body);
            //});



            //app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            //replaced with:
            app.UseCustomExceptionMiddleWare();



            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();

                app.UseSwaggerMiddleWare();
            }

            app.UseHttpsRedirection();

            //for files in your projects
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
