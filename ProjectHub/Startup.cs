using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectHub.AutoMapper;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Infrastructure;
using ProjectHub.Services.Projects;
using ProjectHub.Services.Reviews;
using ProjectHub.Services.User;

namespace ProjectHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ProjectHubDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ProjectHubDbContext>();

            services
                .AddControllersWithViews();

            services
                .AddScoped(typeof(UserManager<ApplicationUser>));
            services
                .AddScoped(typeof(SignInManager<ApplicationUser>));

            services
                .AddSingleton(InitializeMapper());

            services
                .AddTransient<IUserService, UserService>();

            services
                .AddTransient<IProjectService, ProjectService>();

            services
                .AddTransient<IReviewService, ReviewService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error")
                   .UseHsts();
            }
            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                 {
                     endpoints.MapDefaultControllerRoute();
                     endpoints.MapRazorPages();
                 });


        }

        private IMapper InitializeMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
