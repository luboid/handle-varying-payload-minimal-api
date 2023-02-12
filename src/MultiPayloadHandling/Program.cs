using FluentValidation;
using MultiPayloadHandling.Attributes;
using MultiPayloadHandling.Filters;

namespace MultiPayloadHandling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // same as builder.Services.ConfigureHttpJsonOptions
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.Converters.Add(new FruitConverter());
            });

            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new FruitConverter());
            });

            var fruitHandlerType = typeof(IFruitHandler);
            var typesWithMyAttribute =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                let attribute = t.GetCustomAttribute<FruitHandlerAttribute>(true)
                where attribute != null && t.IsAssignableTo(fruitHandlerType)
                select new { Type = t, Attribute = attribute, Interface = t.GetInterfaces().Where(it =>
                {
                    var ok = it.IsAssignableTo(fruitHandlerType) && it != fruitHandlerType;
                    return ok;
                }).FirstOrDefault() };
            
            var fruitTypeMap = new FruitHandlerTypeMap();
            foreach (var type in typesWithMyAttribute) 
            {
                fruitTypeMap[type.Attribute.FruitType] = type.Interface;
                builder.Services.AddScoped(type.Interface, type.Type);
            }


            builder.Services.AddSingleton(fruitTypeMap);
            builder.Services.AddScoped<IFruitHandlerFactory, FruitHandlerFactory>();
            builder.Services.AddScoped<IPearJuiceHandler, PearJuiceHandler>();
            builder.Services.AddScoped<IAppleJuiceHandler, AppleJuiceHandler>();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

            var root = app.MapGroup("");
            root.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

            root.AddFruitHandlerEndpoint();

            app.Run();
        }
    }
}