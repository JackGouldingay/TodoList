using Microsoft.EntityFrameworkCore;
using TodoApi.Database;
using TodoApi.Models.Configuration;
using TodoApi.Services;

namespace TodoApi
{
    public class Startup
    {
        public static ConfigurationManager configuration;

        public static void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            // Add Configuration Settings
            services.Configure<JWTTokenSettings>(configuration.GetSection("JWT"));

            // Add DBContext 
            // SQL Server connection
            var connectionString = builder.Configuration.GetConnectionString("Connection");
            services.AddDbContext<TodoListDBContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.
            services.AddScoped<AuthService>();
            services.AddScoped<TodoService>();
            services.AddSingleton<JWTService>();

            // Add Controllers
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public static void ConfigureBuild(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

        }
    }
}
