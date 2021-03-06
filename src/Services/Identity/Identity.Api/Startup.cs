using AutoMapper;
using Identity.Api.Mappings;
using Identity.Api.Services;
using Identity.Common.Data;
using Identity.Common.Models;
using Infrastructure.Common.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pitstop.Infrastructure.Messaging.Configuration;

namespace Identity.Api
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
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Identity")));

            // Add Identity
            // https://stackoverflow.com/questions/43224177/how-to-add-asp-net-identity-to-asp-net-core-when-webapi-template-is-selected
            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityContext>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Add Services
            services.AddScoped<IEmployeeService, EmployeeService<Employee>>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IProgramService, ProgramService>();

            // Add MessagePublisher
            services.UseRabbitMQMessagePublisher(Configuration);

            // CORS
            services.AddCors();

            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Identity API", Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // CORS
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
