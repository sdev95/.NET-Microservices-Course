using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data 
{

    // Testing purposes
    public static class PrepDb 
    {
        public static void PrepPopulation(IApplicationBuilder app, Boolean isProduction)
        {
            // IApplicationbuilder uit startup
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Get service AppDbContext en geef aan SeedData method
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext context, Boolean isProduction)
        {
            if(isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try 
                {
                    context.Database.Migrate();
                } 
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            // Als context leeg is
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.Platforms.AddRange(
                    new Platform() {Name="Dot Net", Publisher="Microsoft", Cost=0},
                    new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost=10},
                    new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost=0},
                    new Platform() {Name="Jaspersoft Server", Publisher="Jaspersoft", Cost=100}                    
                );
                context.SaveChanges();
            } 
            else 
            {
                Console.WriteLine("--> We already have data!");
            }
        }
    }

}