using DemoWebApiDB.Services.Categories;

using Scalar.AspNetCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;


var builder = WebApplication.CreateBuilder(args);

//---------- Add services to the container.


// 1.  Grab the connection string from the appsetting.json file
const string ConnectionStringNAME = "DefaultConnectionString";
const string MigrationsAssemblyNAME = "DemoWebApiDB";
string connectionString
    = builder.Configuration.GetConnectionString(ConnectionStringNAME)
      ?? throw new InvalidOperationException($"Connection String '{ConnectionStringNAME}' not defined in appsettings file");

// 2. Register the DataContext Service into the DI Container which uses the SQL Server
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        // Register the SQL Server middleware.
        options.UseSqlServer(
            connectionString: connectionString,
            builderOptions => builderOptions.MigrationsAssembly(MigrationsAssemblyNAME) );
    });

// 3. Register Controllers
//    Ensure Content Negotiation and Serialization Support for XML and JSON 
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
    });
    // NOTE: I am not enabling support for XML serialization since most productino APIs support JSON only.
    //       returning 406 "Not Acceptable" for unsupported formats, including XML.
    // .AddXmlSerializerFormatters();               // Remove support for XML serialization


// 4. Register OpenAPI Support.  For more info: https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi();


// 5. Register automatic model validation support with RFC7807 ProblemDetails response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory 
        = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed",
                Detail = "One or more validation errors occurred.",
                Instance = context.HttpContext.Request.Path,
                // TODO: Add TraceId and CoRelationId support to the ValidationProblemDetails model in future.
            };

            return new BadRequestObjectResult(problemDetails);
        };
});

// 6. Register support for ProblemDetails for failed requests to the DI Container
builder.Services
    .AddProblemDetails();


#region Application Services

// 7. Register application services to the DI Container

builder.Services
    .AddScoped<CategoryService>();

#endregion


var app = builder.Build();



//---------- Configure the HTTP request pipeline and Middleware

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Enable API Documentation middleware to work with OpenAPI
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
