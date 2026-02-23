using erp_sql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace erp_sql
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                ));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
    

            var app = builder.Build();

            // 🔍 CHECK DATABASE CONNECTION
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    if (db.Database.CanConnect())
                    {
                        app.Logger.LogInformation("✅ MSSQL database connected successfully.");
                    }
                    else
                    {
                        app.Logger.LogError("❌ MSSQL database connection failed.");
                    }
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "❌ Database connection error");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API v1");
                    c.RoutePrefix = string.Empty; // Swagger at root: http://localhost:5000/
                });
            }


            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
