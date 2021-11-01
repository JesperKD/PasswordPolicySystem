using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolicyLibrary.Validators;
using WebLoginDemo.Data;
using WebLoginDemo.Data.DataModels;
using WebLoginDemo.Data.Repositories;
using WebLoginDemo.Data.Services;

namespace WebLoginDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var policySettings = Configuration.GetSection("PolicySettings").Get<PolicySettings>();
            var fileSettings = Configuration.GetSection("FileSettings").Get<FileSettings>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IFileSettings>(fileSettings);
            services.AddSingleton<IPolicySettings>(policySettings);
            services.AddSingleton<DefaultValidator>();
            services.AddScoped<IValidationService, DefaultValidationService>();

            services.AddScoped<IDatabase, SqlDatabase>();
            services.AddScoped<ILoginRepository, FileLoginRepository>();
            //services.AddScoped<ILoginRepository, DbLoginRepository>();
            services.AddScoped<LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
