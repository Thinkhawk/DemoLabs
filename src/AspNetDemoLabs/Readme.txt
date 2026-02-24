STEPS:
   1. Create a new "ASP.NET WEB API" project
   2. Configure the JsonSerialization options in Program.cs
   3. Configure the XmlSerialization options in Program.cs

NOTE: 
   1. To view the endpoints in all the projects in the Solution:
         View > Other Windows > Endpoints Explorer
   2. To test the API endpoint:
         Right-click on the endpoint in the ENDPOINTS EXPLORER, and "GENERATE REQUEST" in the ".http" file!
      Or: In the Browser, navigate to: https://localhost:XXXX/WeatherForecast
   3. To view the OpenAPI Documentation for your APIs in the project:
            https://localhost:XXXX/openapi/v1.json

-----------------------------

To register Swagger UI documentation for the Open API
    1. Install the "Swashbuckle.AspNetCore.SwaggerUi" Nuget package
    2. In Program.cs
        builder.Services.AddOpenApi();              // Add OpenAPI support 

        if (app.Environment.IsDevelopment())
        {
            // https://localhost:xxxx/openapi/v1.json
            app.MapOpenApi();                       // Enable middleware to serve the generated OpenAPI JSON document    

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(options =>             
            {
                options.SwaggerEndpoint("/openapi/v1.json", "My API V1");       // Specify the endpoint for the generated JSON document
            });
        }
    3. Navigate to "/swagger" (Eg: https://localhost:xxxx/swagger)

-----------------------------

To register Scalar UI documentation for Open API
    1. Install the "Scalar.AspNetCore" Nuget package
    2. In Program.cs
        builder.Services.AddOpenApi();              // Add OpenAPI support 
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.MapScalarApiReference();            // Enable Scalar UI middleware
    }
    3. Navigate to "/scalar" (Eg: https://localhost:xxxx/scalar)

-----------------------------

    insert
	    {
		    name="", description=""
	    }

    delete
	    id=2


    update 	
	    id=1,
	    {
		    id=2, name="", description=""
	    }

-----------------------------

SECURITY CONCEPTS

    - Inherit ApplicationDbContext from DbContext, but from IdentityDbContext
    - OWIN (Open Web Interface for .NET) implementation of KATANA security standard
        - User (authenticated & anonymous)  ]
        - Role (for app)                    ]   => Identity
        - Permission                        ]
        - Claims                            ]

