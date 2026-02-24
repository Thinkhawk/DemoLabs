# DemoWebApi.Infrastructure (Logging, Middleware, Filters, Result Pattern) - Class Library

Never call DbContext directly from controller.

Services
- Category
	- ICategoryService
	- CategoryService
- Product
	- IProductService
	- ProductService

Responsibilities:
- Validation
- Duplicate checks
- Business rules
- Logging


Centralized Structured JSON Logging for:
- Request start/end
- Validation failures
- Duplicate detection
- Exceptions
- Auth events

---

### Recommended enterprise patterns

Use RECORD class for immutibility.  Key benefits:
- Immutable
- Value-based equality
- Perfect for API error modeling

Implement the Result Pattern to ensure that no exceptions are thrown from the data/service layer to the controller.
- No exceptions used for flow
- Clean separation between validation & general errors
- Explicit statuses
- Immutable error models
- Fully testable
- Easily mappable to HTTP
- Extendable (pagination, metadata, etc. later)