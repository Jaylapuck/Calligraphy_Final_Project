using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy.Business.About;
using Calligraphy.Business.AuthenticationService;
using Calligraphy.Business.Contract;
using Calligraphy.Business.Customer;
using Calligraphy.Business.Form;
using Calligraphy.Business.Image;
using Calligraphy.Business.JWTService.JWTTokenHandler;
using Calligraphy.Business.JWTService.RefreshTokenGenerator;
using Calligraphy.Business.JWTService.TokenRefresher;
using Calligraphy.Business.Quote;
using Calligraphy.Data.Config;
using Calligraphy.Data.Repo.About;
using Calligraphy.Data.Repo.Address;
using Calligraphy.Data.Repo.AdminLogin;
using Calligraphy.Data.Repo.Contract;
using Calligraphy.Data.Repo.Customer;
using Calligraphy.Data.Repo.Form;
using Calligraphy.Data.Repo.Image;
using Calligraphy.Data.Repo.Quote;
using Calligraphy.Data.Repo.Service;
using Calligraphy.Mailer.Services;
using Calligraphy.Mailer.Settings;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Calligraphy
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(ConfigureMvcOptions)
                .AddNewtonsoftJson(options => { options.UseMemberCasing(); });

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

            string audiece;
            string issuer;

            if (CurrentEnvironment.IsDevelopment())
            {
                audiece = "https://localhost:5001";
                issuer = "https://localhost:5001";
            }
            else
            {
                audiece = Configuration["Jwt:Audience"];
                issuer = Configuration["Jwt:Issuer"];
            }

          
            
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = audiece,
                        ValidIssuer = issuer,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["JwtToken"];
                            return Task.CompletedTask;
                        },
                    };
                });
            
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
            
            // Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Calligraphy.Mailer", Version = "v1"});
            });

            // configure connection to  the database
            if(CurrentEnvironment.IsDevelopment())
            {
                services.AddDbContext<CalligraphyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalContext")));
            }
            else
            {
                services.AddDbContext<CalligraphyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CalligraphyContext")));
            }

            //configure CORS
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                    .AllowAnyHeader();

                builder.WithOrigins("http://172.22.192.1:3000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();

                builder.WithOrigins("https://calligraphy-final-project.vercel.app")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();

                builder.WithOrigins("https://calligraphy-final-project-trim.vercel.app")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();

                builder.WithOrigins("https://calligraphy-final-project-git-master-trim.vercel.app")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            }));

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private void ConfigureMvcOptions(MvcOptions obj)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
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
            
            app.Use(next => context =>

            {

                string path = context.Request.Path.Value;

                string[] urlAreas = { "/api", "/swagger", "articles" };

                if (

                    string.Equals(path, "/api/admin/login", StringComparison.OrdinalIgnoreCase) ||
                    
                    string.Equals(path, "/api/admin/refresh", StringComparison.OrdinalIgnoreCase) ||
                    
                    urlAreas.Any(urlAreas=>path.StartsWith(urlAreas))

                )

                {
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,

                        new CookieOptions() {

                            HttpOnly = false ,

                            Secure=true,

                            IsEssential=true,
                        });
                    
                }
                return next(context);
            }); 
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}