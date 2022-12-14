using AutoMapper;

using CompanyEmployess.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace CompanyEmployess
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers(config => {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters().AddCustomCSVFormatter().AddNewtonsoftJson();
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });
            //services.AddScoped<ValidationFilterAttribute>();
            //services.AddScoped<ValidateEmployeeForCompanyExistsAttribute>();
            services.ConfigureVersioning();
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseAuthentication();
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => 
                    opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
                CreateMap<Appliance, ApplianceDto>();
                CreateMap<Furniture, FurnitureDto>();
                CreateMap<Employee, EmployeeDto>();
                CreateMap<CompanyForCreationDto, Company>();
                CreateMap<EmployeeForCreationDto, Employee>();
                CreateMap<ApplianceForCreationDto, Appliance>();
                CreateMap<FurnitureForCreationDto, Furniture>();
                CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
                CreateMap<CompanyForUpdateDto, Company>();
                CreateMap<ApplianceForUpdateDto, Employee>().ReverseMap();
                CreateMap<FurnitureForUpdateDto, Employee>().ReverseMap();
                CreateMap<UserForRegistrationDto, User>();
            }
        }
    }
}
