using DemoWebApi.Services;

using Microsoft.AspNetCore.Mvc.Formatters;

using Scalar.AspNetCore;

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
    .AddJsonOptions(options =>
    {
        // Throw an exception if the JSON contains properties that are not in the model
        options.JsonSerializerOptions.UnmappedMemberHandling
            = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;
    })
    .AddXmlSerializerFormatters();               // Add support for XML serialization


// Register the Open API service 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi();


// Register the custom data service
builder.Services.AddSingleton<IMyDataService, MyDataService>();


// ------------------ instantiate the Application Object

var app = builder.Build();



//------------------- Configure the Services in the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    // View the OpenAPI documentation at: https://localhost:7085/openapi/v1.json
    app.MapOpenApi();

    // Register the middleware for Scalar to use OpenAPI documentation
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Register the Custom Middleware #1
app.Use(async (httpContext, objDelegateForNextMiddleware) =>
{
    // context.Request.Headers["Author"]

    httpContext.Response.Headers.Append("CompanyName", "Trivium");

    // Call the next delegate/middleware in the pipeline
    await objDelegateForNextMiddleware();

    // do something else also (after the next middleware has executed)
});



// Register the Custom Middleware #2
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("CompanyName", "Manoj Kumar Sharma");

    // Call the next delegate/middleware in the pipeline
    await next();
});



// Register the MINIMAL API
app.Map(pattern: "/",                               // Route for invoking the API endpoint
        () => "hello world from API");              // Handler for the delegate


// ----- Run the application

app.Run();



