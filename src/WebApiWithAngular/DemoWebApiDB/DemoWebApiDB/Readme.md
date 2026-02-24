# DemoWebApi (API Layer) - ASP.NET Core Web API Project

APIs must NEVER crash with raw exception.

Controllers
- CategoriesController
- ProductsController
- ReportsController

RULES:
- Controllers
	- Use DTO only
		- Defensive API design
		- Contract validation
	- No DbContext directly
	- Return proper HTTP codes
	- RFC7807 errors
	- Ensure Route + Body validation for robustness
- Global Exception Handling
	- Exception middleware
	- ProblemDetails
	- ValidationProblemDetails
- Structured Logging
	- Request logging
	- Error logging
	- DB logging optional

## GitHub Strategy

Branch strategy
- main
	- dev
	- feature/category-api
	- feature/product-api

Pull request flow
01. Create feature branch
02. Push code
03. Create PR
04. Review
05. Merge

