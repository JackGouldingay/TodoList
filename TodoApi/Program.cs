
namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Startup.configuration = builder.Configuration;
            Startup.ConfigureServices(builder, builder.Services);
            
            var app = builder.Build();
            Startup.ConfigureBuild(app);

            app.Run();
        }
    }
}
