using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using voice.api.Filters;
using voice.api.Helpers;
using voice.dapper;
using voice.files;
using voice.logs;
using voice.models;
using voice.notify;
using voice.queue;
using voice.security;

namespace voice.api
{
    public class Startup
    {
        public static string WebRootPath;

        public IConfiguration Configuration { get; }

        public static string CurrentLanguage { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebRootPath = webHostEnvironment.WebRootPath;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();  

            #region Language translate

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo(LanguageConsts.English) };
                options.DefaultRequestCulture = new RequestCulture(culture: LanguageConsts.English, uiCulture: LanguageConsts.English);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    //var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    //var firstLang = userLangs.Split(',').FirstOrDefault();
                    CurrentLanguage = LanguageConsts.English;
                    return Task.FromResult(new ProviderCultureResult(CurrentLanguage, CurrentLanguage));
                }));
            });

            #endregion Language translate

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.MaxRequestBodySize = 300000000;
            });

            services.AddSignalR();

            #region Cores Policy

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                //builder.WithOrigins("http://13.232.217.47:8090/").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));

            #endregion Cores Policy

            #region JWT Authentication

            var key = Encoding.ASCII.GetBytes(Configuration["Authentication:Key"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion JWT Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            #region Initialization of Configs.

            services.Configure<EmailConfigs>(Configuration.GetSection("EmailConfig"));
            services.Configure<AdminConfigs>(Configuration.GetSection("AdminConfig"));
            services.Configure<AppConfigs>(Configuration.GetSection("AppConfig"));
            services.Configure<AuthConfigs>(Configuration.GetSection("Authentication"));
            services.Configure<ConnectionConfigs>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<BaseUrlConfigs>(Configuration.GetSection("BaseConfig"));
            services.Configure<PushConfigs>(Configuration.GetSection("PushConfig"));
            services.Configure<PaymentConfigs>(Configuration.GetSection("PaymentConfig"));
            services.Configure<SmsConfigs>(Configuration.GetSection("SmsConfig"));
            services.Configure<SapConfigs>(Configuration.GetSection("SapConfig"));
            services.Configure<SharedFolderCredentialsConfigs>(Configuration.GetSection("SharedFolderCredentialsConfig"));

            #endregion Initialization of Configs.

            #region Registering Dependency Injections.

            services.AddTransient<IDapperRepository, dapper.implementations.DapperRepository>();
            services.AddScoped<ILogManager, logs.implementations.LogManager>();
            services.AddSingleton<ICrypto, security.implementations.Crypto>();
            services.AddSingleton<IUploadManager, files.implementations.UploadManager>();
            services.AddSingleton<IExportManager, files.implementations.ExportManager>();
            services.AddSingleton<ISharedExportManager, files.implementations.SharedExportManager>();
            services.AddSingleton<IQueue, queue.implementations.Queue>();
            services.AddSingleton<SeedHelpers>();
            services.AddScoped<IMessages, notify.implementations.Messages>();
            services.AddSingleton<ExceptionFilters, ExceptionFilters>();

            #endregion Registering Dependency Injections.

            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressMapClientErrors = true;
            });

            // Register the Swaguer generator, defining 1 or more Swaguer documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "The Voice",
                    Version = "v1",
                });
            });

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 0;
                //options.JsonSerializerOptions.IgnoreNullValues = false;
                //options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
            });
        }

        public void Configure(IApplicationBuilder app, SeedHelpers seed)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseStaticFiles();
            seed.Seed().Wait();

            var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOption.Value);

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "The Voice - API Documentation");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<SignalRHelpers>("/notificationHub");
            });
        }
    }
}
