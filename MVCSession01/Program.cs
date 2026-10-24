using MVCSession01.CustomMiddleware;

namespace MVCSession01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registers services required for MVC applications with Controllers and Views
            builder.Services.AddControllersWithViews();

            // Registers services for API controllers (without View support)
            builder.Services.AddControllers();

            // Registers the full MVC framework (Controllers, Views, Razor Pages, and more)
            builder.Services.AddMvc();

            // Registers the core MVC services only (minimal MVC features; no Views or Razor Pages by default)
            builder.Services.AddMvcCore();

            // Registers services for Razor Pages applications
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Custom Middleware
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Hello");         // before
                await next(context);
                Console.WriteLine($"Bye");  // after
            });

            app.UseMiddleware<RequestTimingMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            // specific route 
            app.MapControllerRoute(
                name: "shop",
                pattern: "shop/{id:int}",
                defaults: new
                {
                    controller = "Products",
                    action = "Details"
                });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
