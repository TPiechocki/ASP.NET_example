using Example.WebApi.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Example.WebApi.Context
{
    internal static class ExampleDbInitializer
    {
        public static void Initialize(ExampleDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // DB already initialized
            }

            // initialize users
            var users = new[]
            {
                new User(
                    "TPiechocki", 
                    new PasswordHasher<User>().HashPassword(null, "password")),
                new User(
                    "admin",
                    new PasswordHasher<User>().HashPassword(null, "admin"))
            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            // initialize satellites
            var satellites = new[]
            {
                new Satellite(559, "USA", "Starlink-1007"),
                new Satellite(35743, "United Arab Emirates", "Yahsat-1A"),
                new Satellite(1175, "United Kingdom", "OneWeb-0006")

            };
            foreach (var satellite in satellites)
            {
                context.Satellites.Add(satellite);
            }
            context.SaveChanges();
        }
    }
}