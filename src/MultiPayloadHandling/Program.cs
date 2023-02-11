using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MultiPayloadHandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var fruitHandlerType = typeof(IFruitHandler);
            var typesWithMyAttribute =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                let attribute = t.GetCustomAttribute<FruitSreviceAttribute>(true)
                where attribute != null && t.IsAssignableTo(fruitHandlerType)
                select new { Type = t, Attribute = attribute, Interface = t.GetInterfaces().Where(it =>
                {
                    var ok = it.IsAssignableTo(fruitHandlerType) && it != fruitHandlerType;
                    return ok;
                }).FirstOrDefault() };
            
            var fruitTypeMap = new FruitTypeMap();
            foreach (var type in typesWithMyAttribute) 
            {
                fruitTypeMap[type.Attribute.FruitType] = type.Interface;
                builder.Services.AddScoped(type.Interface, type.Type);
            }


            builder.Services.AddSingleton(fruitTypeMap);
            builder.Services.AddScoped<IFruitHandlerFactory, FruitHandlerFactory>();
            builder.Services.AddScoped<IPearJuiceHandler, PearJuiceHandler>();
            builder.Services.AddScoped<IAppleJuiceHandler, AppleJuiceHandler>();

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.AddWetherForecastEndpoint();
            app.AddPayloadEndpoint();

            app.Run();
        }
    }
}