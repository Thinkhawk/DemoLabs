# DemoWebApi.DtoModels (API DTOs) - Class Library

## DTO Models

Benefits:
- To prevent overposting
- Prevent ID in POST requests
- Controlled mapping
- To secure API
- Versioning friendly

### Category DTO Models (records)
- CategoryCreateDto
- CategoryUpdateDto
- CategoryReadDto
- CategoryProductReportDto (category-wise product count report)  from sp_CategoryProductReport source: vw_ProductsWithCategory
### Product DTO Models (records)
- ProductCreateDto

All DTO Models defined as records with Primary Constructor - to initialize its members 