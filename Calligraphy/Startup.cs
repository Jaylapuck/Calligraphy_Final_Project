using System;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.Customer;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
using Calligraphy.Data.Config;
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
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Business.JWTService.RefreshTokenGenerator;
using Calligraphy.Business.JWTService.TokenRefresher;
using Calligraphy.Data.Repo.AdminLogin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Calligraphy.Business.About;
using Calligraphy.Data.Repo.About;

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
            services.AddControllersWithViews(ConfigureMvcOptions)
                .AddNewtonsoftJson(options =>
                {
                    options.UseMemberCasing();
                });
            
            //Mail Configuration
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            
            //DP Injection
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
            services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddTransient<ITokenRefresher, TokenRefresher>();
            services.AddTransient<IAboutService, AboutService>();
            services.AddTransient<IAboutRepo, AboutRepo>();
            
            //JWT AUTHENTICATION
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme =  JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                    ClockSkew=TimeSpan.Zero,
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            
            // Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calligraphy.Mailer", Version = "v1" });
            });

            // configure connection to  the database
            services.AddDbContext<CalligraphyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CalligraphyContext")));
            
            //configure CORS
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                    .AllowAnyHeader();
            }));
            
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private void ConfigureMvcOptions(MvcOptions obj)
        {
            
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
