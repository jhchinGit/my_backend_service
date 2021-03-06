using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyBackendService.Businesses;
using MyBackendService.Services;

namespace MyBackendService
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
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddStackExchangeRedisCache(action =>
            {
                action.InstanceName = "redis";
                action.Configuration = "localhost:6380"; //connect to docker redis
            });

            services.AddDbContext<RepositoryContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConncetion")));

            services.AddHttpClient();

            services.AddControllers();

            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                //options.Authority = "http://192.168.0.182/identityServer";
                options.Authority = "http://localhost:5250";
                options.RequireHttpsMetadata = false;
                options.Audience = "muffinscopeapi";
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false
                };
            });

            services.AddTransient<ICovidDailyReportManager, CovidDailyReportManager>();
            services.AddTransient<IAuthenticationCodeManager, AuthenticationCodeManager>();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddSingleton<ITotpValidator, TotpValidator>();
            services.AddSingleton<ITotpGenerator, TotpGenerator>();
            services.AddSingleton<ITotpSetupGenerator, TotpSetupGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

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