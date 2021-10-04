using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Password.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Password.Api.csproj", Version = "v1" });
            });

            Password.MinLength = Int32.Parse(Configuration.GetSection("Password").GetSection("MinLength").Value);
            Password.MaxLength = Int32.Parse(Configuration.GetSection("Password").GetSection("MaxLength").Value);
            Password.MinNumber = Int32.Parse(Configuration.GetSection("Password").GetSection("MinNumber").Value);
            Password.MinUpperCase = Int32.Parse(Configuration.GetSection("Password").GetSection("MinUpperCase").Value);
            Password.MinLowerCase = Int32.Parse(Configuration.GetSection("Password").GetSection("MinLowerCase").Value);
            Password.MinSymbol = Int32.Parse(Configuration.GetSection("Password").GetSection("MinSymbol").Value);
            Password.CharSymbol = Configuration.GetSection("Password").GetSection("PatternSymbol").Value;
            Password.RepeatChar = Convert.ToBoolean(Configuration.GetSection("Password").GetSection("RepeatChar").Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Password.Api.csproj v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}