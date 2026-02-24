# DemoWebApi.Services (Business Logic + Results Pattern) - Class Library

### Result Pattern
- Result<T>
- Success
	- 200 "Ok"
	- 201 "Created"
	- 202 "Accepted"
- Failure
	- 400 "Rejected" (with validation details)
- NotFound (404 "Not Found")
- ValidationError
- Conflict (409 "Conflict")
	- Duplicate Category Name detection
	- Duplicate Product Name detection

- RFC 7807 ProblemDetails
	- ValidationProblemDetails
	- NotFoundProblemDetails
	- ConflictProblemDetails

