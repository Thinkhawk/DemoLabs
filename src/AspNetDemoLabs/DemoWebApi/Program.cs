using DemoWebApi.Services;

using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

//------------------- Register the services needed, to the DI Container.



// builder.Services.AddControllers();

builder.Services
    .AddControllers(options =>
    {
        // Respect the Accept header sent by the browser/client
        options.RespectBrowserAcceptHeader = true;

        // Return 406 Not Acceptable if the client requests an unsupported format
        options.ReturnHttpNotAcceptable = true;

        // Enable support to define the Character-set in the Request "Accept" parameter
        var jsonOutputFormatter
             = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();
        jsonOutputFormatter?.SupportedMediaTypes.Add("application/json; charset=utf-8");

    })
    .AddXmlSerializerFormatters();               // Add support for XML serialization


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Register the custom data service
builder.Services.AddSingleton<IMyDataService, MyDataService>();

// ------------------ instantiate the Application Object

var app = builder.Build();



//------------------- Configure the Services in the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/", () => "hello world from API");


// ----- Run the application

app.Run();



