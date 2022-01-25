// This Startup file is based on ASP.NET Core new project templates and is included
// as a starting point for DI registration and HTTP request processing pipeline configuration.
// This file will need updated according to the specific scenario of the application being upgraded.
// For more information on ASP.NET Core startup files, see https://docs.microsoft.com/aspnet/core/fundamentals/startup

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calligraphy.Business.Address;
using Calligraphy.Business.Customer;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
using Calligraphy.Data.Config;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Repo;
using Calligraphy.Data.Repo.Image;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Calligraphy.Mailer.Services;
using Calligraphy.Mailer.Settings;
using Microsoft.OpenApi.Models;
using Calligraphy.Data.Repo.Customer;
using Calligraphy.Data.Repo.Address;
using Calligraphy.Data.Repo.Form;
using Calligraphy.Data.Repo.Service;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Repo.Quote;
using Microsoft.AspNetCore.Http;
using Calligraphy.Data.Repo.Contract;
using Calligraphy.Business.Contract;
using Calligraphy.Data.Repo.About;
using Calligraphy.Business.About;

namespace Calligraphy
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
            services.AddControllersWithViews(ConfigureMvcOptions)
                // Newtonsoft.Json is added for compatibility reasons
                // The recommended approach is to use System.Text.Json for serialization
                // Visit the following link for more guidance about moving away from Newtonsoft.Json to System.Text.Json
                // https://docs.microsoft.com/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to
                .AddNewtonsoftJson(options =>
                {
                    options.UseMemberCasing();
                });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddTransient<IMailerService, MailServiceImpl>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IImageRepo, ImageRepo>();
            services.AddTransient<IAboutService, AboutService>();
            services.AddTransient<IAboutRepo, AboutRepo>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IQuoteRepo, QuoteRepo>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddTransient<IAddressRepo, AddressRepo>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IFormRepo, FormRepo>();
            services.AddTransient<IServiceRepo, ServiceRepoImpl>();
            services.AddTransient<IContractRepo, ContractRepo>();
            services.AddTransient<IContractService, ContractService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calligraphy.Mailer", Version = "v1" });
            });

            services.AddDbContext<CalligraphyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CalligraphyContext")));
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calligraphy.Mailer"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseMvc();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureMvcOptions(MvcOptions mvcOptions)
        { 
        }
    }
}
