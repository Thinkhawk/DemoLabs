// Demo of how to register Services into the Dependency Injection Container

/// <summary>
///     SINGLETON
///     - A single instance of the object is created and shared across all requests 
///       as long as the application is running.
///     - Great for sharing states.
///     - Best prefered for dealing with data or state that needs to be shared across
///       multiple requests, or when the instantiation process is expensive. 
///     - Keep an eye on state management and thread-safety when using singleton services.
///     SCOPED
///     - With every HTTP Request, a new object is instantiated, and it lasts for that request lifecycle.
///       Meaning, the same instance is provided for the entire scope of that request.
///     - Best suited when you want to maintain state within a single request,
///       but not persistently. This is also ideal when you need shared
///       communication/data-access within object instances of a single request.
///     TRANSIENT
///     - The object is instantiated on each call to the service, and will be in a class-scoped lifecycle.
///     - It is lightweight and stateless.
///     - Can be memory and resource intensive, since a new object is instantiated on each call.
///     - Best suited for lightweight, stateless services that are implemented throughout
///       the application without needing integration or communication. 
///       These instances do not remember their previous state – like state amnesia!
/// </summary>


using Demo_DI_Services.Services;

var builder = WebApplication.CreateBuilder(args);

//------- Add services to the DI container

// Singleton: only one instance of the resource is created and is reused anytime it is requested.
// builder.Services.AddSingleton(typeof(MyService));
// builder.Services.AddSingleton<MyService>();
builder.Services.AddSingleton<IMySingletonService, MyService>();

// Transient: a new instance is provided to every controller, and is shared amongst all of its services.
builder.Services.AddTransient<IMyTransientService, MyService>();

// Scoped: a different object is provided for each request.
builder.Services.AddScoped<IMyScopedService, MyService>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// -------- Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");


app.Run();

