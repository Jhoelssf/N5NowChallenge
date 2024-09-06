using Microsoft.EntityFrameworkCore;
using N5Infrastructure.Data;

namespace N5Api.StartUp;

public static class DatabaseInitial
{
    public static void ExecuteMigrate(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        Migrate(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
    }

    private static void Migrate(ApplicationDbContext context)
    {
        System.Console.WriteLine("Starting database migration...");
        context.Database.Migrate();
        System.Console.WriteLine("Database migration completed.");
    }
}