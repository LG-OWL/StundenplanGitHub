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
                List<Schueler> schuelerlist = new List<Schueler>();
                List<Stunden> stundenlist = new List<Stunden>();
                for (int zaehler = 0; zaehler < 5; zaehler++)
                {
                    Random rnd = new Random();
                    string wochentag;
                    switch (zaehler)
                    {
                        case 0:
                            wochentag = "Montag";
                            break;
                        case 1:
                            wochentag = "Dienstag";
                            break;
                        case 2:
                            wochentag = "Mittwoch";
                            break;
                        case 3:
                            wochentag = "Donnerstag";
                            break;
                        case 4:
                            wochentag = "Freitag";
                            break;
                        default:
                            wochentag = "";
                            break;
                    }
                    for (int i = 1; i <= 6; i++)
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
                        Lehrer lehrer = new Lehrer() { Name = "Wittmann" };
                        Stunden stunden = new Stunden() { Stunde = i, Wochentag = wochentag, Fach = lessonStr, Lehrer = lehrer};
                        stundenlist.Add(stunden);
                    }
                }
                Schueler schueler = new Schueler() { Name = "Werner" };
                schuelerlist.Add(schueler);
                dbContext.Klasse.Add(new Klasse() { Bezeichnung = "6a", Schuelers = schuelerlist, Stundens = stundenlist });
                dbContext.SaveChanges();
            }

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<StundenplanDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("StundenplanDbContext")));
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
