using BlazorCRUDApp.ApplicationDbContext;
using BlazorCRUDApp.Data;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using System;

namespace BlazorCRUDApp
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddSweetAlert2();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source = rihalDB.db");
            });
            services.AddScoped<ClassServices>();
            services.AddScoped<StudentServices>();
            services.AddScoped<CountryServices>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IStudentService, StudentService>();

            #region Connection String  
            //services.AddDbContext<AppDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            
              using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
      }
             
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
