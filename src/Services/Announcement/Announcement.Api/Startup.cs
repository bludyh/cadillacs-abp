using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Announcement.Api.Data;
using Announcement.Api.Mappings;
using Announcement.Api.Models;
using Announcement.Api.Services;
using AutoMapper;
using Infrastructure.Common.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Announcement.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<AnnouncementContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Announcement")));

            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Add Services
            services.AddScoped<IAnnouncementService, AnnouncementService>();

            services.AddControllers();
            //services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()))
              //  .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
