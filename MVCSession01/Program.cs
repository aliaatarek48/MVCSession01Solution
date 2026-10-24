using MVCSession01.CustomMiddleware;

namespace MVCSession01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
