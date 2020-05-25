using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using CQRS.CarRental.Core.Commands;
using CQRS.CarRental.Core.Commands.Handlers;
using CQRS.CarRental.Core.Interfaces;
using CQRS.CarRental.Core.Persistance;
using CQRS.CarRental.Core.Persistance.Repositories;
using CQRS.CarRental.Core.Queries;
using CQRS.CarRental.Core.Queries.Handlers;
using CQRS.CarRental.Core.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using SharedKernel.DIContainers;
using SharedKernel.Dispatchers;
using SharedKernel.Dispatchers.Implementations;
using SharedKernel.Persistance;

namespace CQRS.CarRental.RESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(configure =>
            {
                configure.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
                    
                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            #region register adapters and dispachers
            services.AddScoped<IResolver, ASPNETServiceProviderAdapter>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            #endregion

            #region register repositories
            services.AddScoped<CarRentalContext>();
            services.AddScoped<IDriverReadModelRepository, DriverReadModelRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IRentalReadModelRepository, RentalReadModelRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<ICarReadModelRepository, CarReadModelRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarRentalUnitOfWork, CarRentalUnitOfWork>();
            #endregion

            #region register commands
            services.AddScoped<ICommandHandler<CreateDriverCommand>, CreateDriverHandler>();
            services.AddScoped<ICommandHandler<CreateRentCommand>, CreateRentHandler>();
            services.AddScoped<ICommandHandler<CreateCarCommand>, CreateCarHandler>();
            services.AddScoped<ICommandHandler<CreateCarCommand>, CreateCarHandler>();
            services.AddScoped<ICommandHandler<StopRentingCarCommand>, StopRentingCarHandler>();
            services.AddScoped<ICommandHandler<DeleteDriverCommand>, DeleteDriverCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteRentCommand>, DeleteRentCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCarCommand>, DeleteCarCommandHandler>();
            #endregion

            #region register queries
            services.AddScoped<IQueryHandler<GetAllCarsQuery, List<CarResult>>, GetAllCarsQueryHandler>();
            services.AddScoped<IQueryHandler<GetCarDetailsQuery, CarResult>, GetCarDetailsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllDriversQuery, List<DriverResult>>, GetAllDriversQueryHandler>();
            services.AddScoped<IQueryHandler<GetDriverByIdQuery, DriverResult>, GetDriverByIdQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllRentalsQuery, List<RentalResult>>, GetAllRentalsQueryHandler>();
            services.AddScoped<IQueryHandler<GetRentalByIdQuery, RentalModelResult>, GetRentalByIdQueryHandler>();
            #endregion

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "CarRental API",
                    Version = "v1",
                    Description = "Throught this API you can acces Cars to Rent, Drivers and all Rentals."
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarRentalContext carRentalContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            carRentalContext.InitialDbData();
        }
    }
}
