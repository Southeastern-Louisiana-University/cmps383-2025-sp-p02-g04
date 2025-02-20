namespace Selu383.SP25.P02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext") ?? throw new InvalidOperationException("Connection string 'DataContext' not found.")));

            // This service is for Swagger UI
            /*  builder.Services.AddSwaggerGen(options =>
              {
                 options.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "Theatre API",
                     Version = "v1",
                     Description = "API for managing theatres"
                  });
            });   */

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                await db.Database.MigrateAsync();
                SeedTheaters.Initialize(scope.ServiceProvider);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                //app.UseSwagger();

            }
            /*app.UseSwaggerUI(options =>
            {
                // The Swagger UI will be available at the following endpoint
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Theatre API v1");
                options.RoutePrefix = string.Empty; // Set Swagger UI to the root
            }); */

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting()
             .UseEndpoints(x =>
                            {
                                x.MapControllers();
                            });
            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseSpa(x =>
                {
                    x.UseProxyToSpaDevelopmentServer("http://localhost:5173");
                });
            }
            else
            {
                app.MapFallbackToFile("/index.html");
            }

            app.Run();
        }
    }
}

