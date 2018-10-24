using BuildFeed.Middleware;
using BuildFeed.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildFeed
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(provider => Configuration);

            var config = new MongoConfig(
                Configuration.GetValue<string>("data:MongoHost"),
                Configuration.GetValue<int?>("data:MongoPort"),
                Configuration.GetValue<string>("data:MongoDB"),
                Configuration.GetValue<string>("data:MongoUser"),
                Configuration.GetValue<string>("data:MongoPass")
                );

            services.AddTransient(provider => new BuildRepository(config));
            services.AddTransient(provider => new MetaItem(config));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseThemes();
            app.UseLocalisation();
            app.UseVersion();
            app.UseMvc();
        }
    }
}