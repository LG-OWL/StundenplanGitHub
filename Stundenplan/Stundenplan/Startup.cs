using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Stundenplan.Models;
using Stundenplan.Data;
using Stundenplan.Services;

namespace Stundenplan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Seed(IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<StundenplanDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Schueler.Any())
            {
                List<Stunden> stundenlist = new List<Stunden>();
                for (int j = 0; j < 5; j++)
                {
                    Random rnd = new Random();
                    for (int k = 1; k <= 6; k++)
                    {
                        int lessonDecision = rnd.Next(1, 4);
                        string lessonStr;
                        switch (lessonDecision)
                        {
                            case 1:
                                lessonStr = "Mathe";
                                break;
                            case 2:
                                lessonStr = "Deutsch";
                                break;
                            case 3:
                                lessonStr = "Englisch";
                                break;
                            default:
                                lessonStr = "Frei";
                                break;
                        }
                        Stunden stunden = new Stunden() { Stunde = k, Wochentag = j, Fach = lessonStr};
                        stundenlist.Add(stunden);
                    }
                }
                dbContext.Klasse.Add(new Klasse() { Bezeichnung = "6a", Stundens = stundenlist });
                dbContext.SaveChanges();
            }

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<StundenplanDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StundenplanDbContext")));
            services.AddTransient<IStundenService, StundenService>();
            Seed(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
