using System.Text;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.Customer;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
using Calligraphy.Data.Config;
using Calligraphy.Data.IUriService;
using Calligraphy.Data.Repo.Image;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
using Calligraphy.Data.Repo.AdminLogin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Calligraphy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "https://localhost:5001",
                    ValidIssuer = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@YT6*}HnibVtC4?ubl^4ybr1#ekn=<UCN]86T^=yA[8Ivz`ZasJy+GwOr=8avZU"))
                };
            });
            
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddTransient<IMailerService, MailServiceImpl>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IImageRepo, ImageRepo>();
            services.AddTransient<IQuoteService, QuoteService>();
            services.AddTransient<IQuoteRepo, QuoteRepo>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddTransient<IAddressRepo, AddressRepo>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IFormRepo, FormRepo>();
            services.AddTransient<IServiceRepo, ServiceRepoImpl>();
            services.AddTransient<IContractRepo, ContractRepo>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IAdminLoginRepo, AdminLoginRepo>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calligraphy.Mailer", Version = "v1" });
            });

            services.AddDbContext<CalligraphyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CalligraphyContext")));
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
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
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
