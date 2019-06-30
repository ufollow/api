using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ufollow.API.Authorization;
using ufollow.API.Extensions.DependencyInjection;
using ufollow.API.Filters;
using ufollow.Domain;
using ufollow.Domain.Repositories;
using ufollow.Infrastructure;
using ufollow.Infrastructure.AmazonS3;
using ufollow.Infrastructure.Database;
using ufollow.Infrastructure.Database.Repositories;
using ufollow.Infrastructure.Mailing.Smtp;

namespace ufollow.API
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AmazonS3Options>(Configuration.GetSection("AmazonS3"));
            services.Configure<ApiTokenOptions>(Configuration.GetSection("Authorization"));
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddMvc(options =>
            {
                options.Filters.Add(new ModelValidationAttribute());
            });

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(Configuration["ConnectionString:PostgreSQL"], pgsql =>
                {
                    pgsql.MigrationsHistoryTable(tableName: "__migration_history", schema: ApiDbContext.Schema);
                });
            });

            services.AddJwtAuthentication(options =>
            {
                Configuration.GetSection("Authorization").Bind(options);
            });

            services.AddDefaultCorsPolicy();

            services.AddScoped<IFileRepository, AmazonS3Api>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
