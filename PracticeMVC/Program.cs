using Microsoft.EntityFrameworkCore;
using PracticeMVC.Models;
using System.Diagnostics;

namespace PracticeMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connection, o => o.UseNodaTime()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            ClientVM(app);
            ManufacturerVM(app);
            TvVM(app);

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Manufacturer}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Tv}/{id?}");

            app.Run();
        }

        private static void ClientVM(WebApplication app)
        {
            app.MapGet("/api/clients", async (Context db) => await db.Clients.ToListAsync());

            app.MapGet("/api/clients/{clientId:int}", async (int clientId, Context db) =>
            {
                // получаем пользователя по id
                Client? client = await db.Clients.FirstOrDefaultAsync(u => u.Id == clientId);

                // если не найден, отправляем статусный код и сообщение об ошибке
                if (client == null) return Results.NotFound(new { message = "Клиент не найден" });

                // если пользователь найден, отправляем его
                return Results.Json(client);
            });

            app.MapPost("/api/clients", async (Client client, Context db) =>
            {
                // добавляем пользователя в массив
                await db.Clients.AddAsync(client);
                await db.SaveChangesAsync();
                return client;
            });

            app.MapPut("/api/clients", async (Client client, Context db) =>
            {
                // получаем пользователя по id
                var oldClient = await db.Clients.FirstOrDefaultAsync(u => u.Id == client.Id);

                // если не найден, отправляем статусный код и сообщение об ошибке
                if (oldClient == null) return Results.NotFound(new { message = "Пользователь не найден" });

                // если пользователь найден, изменяем его данные и отправляем обратно клиенту
                oldClient.FullName = client.FullName;
                oldClient.Telephone = client.Telephone;
                oldClient.BankAccount = client.BankAccount;
                await db.SaveChangesAsync();
                return Results.Json(oldClient);
            });

            app.MapDelete("/api/clients/{clientId:int}", async (int clientId, Context db) =>
            {
                // получаем пользователя по id
                Client? client = await db.Clients.FirstOrDefaultAsync(u => u.Id == clientId);

                // если не найден, отправляем статусный код и сообщение об ошибке
                if (client == null) return Results.NotFound(new { message = "Клиента не найден" });

                // если пользователь найден, удаляем его
                db.Clients.Remove(client);
                await db.SaveChangesAsync();
                return Results.Json(client);
            });
        }

        private static void ManufacturerVM(WebApplication app)
        {
            app.MapGet("/api/manufacturers", async (Context db) => await db.Manufacturers.ToListAsync());

            app.MapGet("/api/manufacturers/{manufacturerId:int}", async (int manufacturerId, Context db) =>
            {
                Manufacturer? manufacturer = await db.Manufacturers.FirstOrDefaultAsync(u => u.Id == manufacturerId);

                if (manufacturer == null) return Results.NotFound(new { message = "Производитель не найден" });

                return Results.Json(manufacturer);
            });

            app.MapPost("/api/manufacturers", async (Manufacturer manufacturer, Context db) =>
            {
                await db.Manufacturers.AddAsync(manufacturer);
                await db.SaveChangesAsync();
                return manufacturer;
            });

            app.MapPut("/api/manufacturers", async (Manufacturer manufacturer, Context db) =>
            {
                var oldManufacturer = await db.Manufacturers.FirstOrDefaultAsync(u => u.Id == manufacturer.Id);

                if (oldManufacturer == null) return Results.NotFound(new { message = "Производитель не найден" });

                oldManufacturer.Name = manufacturer.Name;
                oldManufacturer.Director = manufacturer.Director;
                oldManufacturer.ChiefAccountant = manufacturer.ChiefAccountant;
                oldManufacturer.BankDetails = manufacturer.BankDetails;
                await db.SaveChangesAsync();
                return Results.Json(oldManufacturer);
            });

            app.MapDelete("/api/manufacturers/{manufacturerId:int}", async (int manufacturerId, Context db) =>
            {
                Manufacturer? manufacturer = await db.Manufacturers.FirstOrDefaultAsync(u => u.Id == manufacturerId);

                if (manufacturer == null) return Results.NotFound(new { message = "Клиента не найден" });

                db.Manufacturers.Remove(manufacturer);
                await db.SaveChangesAsync();
                return Results.Json(manufacturer);
            });
        }

        private static void TvVM(WebApplication app)
        {
            app.MapGet("/api/tvs", async (Context db) => await db.TVs.Include(p => p.TVType).Include(p => p.Manufacturer).ToListAsync());

            app.MapGet("/api/tvs/{manufacturerId:int}", async (int tvId, Context db) =>
            {
                TV? tv = await db.TVs.FirstOrDefaultAsync(u => u.Id == tvId);

                if (tv == null) return Results.NotFound(new { message = "Производитель не найден" });

                return Results.Json(tv);
            });

            app.MapPost("/api/tvs", async (TV tv, Context db) =>
            {
                await db.TVs.AddAsync(tv);
                await db.SaveChangesAsync();
                return tv;
            });

            app.MapPut("/api/tvs", async (TV tv, Context db) =>
            {
                var oldTv = await db.TVs.FirstOrDefaultAsync(u => u.Id == tv.Id);

                if (oldTv == null) return Results.NotFound(new { message = "Производитель не найден" });

                oldTv.Name = tv.Name;
                oldTv.TVType = tv.TVType;
                oldTv.Manufacturer = tv.Manufacturer;
                oldTv.Price = tv.Price;
                oldTv.TvInStock = tv.TvInStock;
                oldTv.SoldNumber = tv.SoldNumber;
                oldTv.DeliveredNumber = tv.DeliveredNumber;
                await db.SaveChangesAsync();
                return Results.Json(oldTv);
            });

            app.MapDelete("/api/tvs/{manufacturerId:int}", async (int tvId, Context db) =>
            {
                TV? tv = await db.TVs.FirstOrDefaultAsync(u => u.Id == tvId);

                if (tv == null) return Results.NotFound(new { message = "Клиента не найден" });

                db.TVs.Remove(tv);
                await db.SaveChangesAsync();
                return Results.Json(tv);
            });
        }
    }
}