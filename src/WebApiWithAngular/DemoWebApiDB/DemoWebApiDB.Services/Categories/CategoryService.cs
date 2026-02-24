using DemoWebApiDB.Infrastructure.Helpers;


namespace DemoWebApiDB.Services.Categories;


/// <summary>
///     Handles business logic for Category operations.
/// </summary>
public sealed class CategoryService
{

    private readonly ApplicationDbContext _db;
    private readonly ILogger<CategoryService> _logger;


    public CategoryService(
        ApplicationDbContext db,
        ILogger<CategoryService> logger)
    {
        _db = db;
        _logger = logger;
    }


    /// <summary>
    ///     Creates a new Category.
    ///     Returns Location URI of created resource, if successful.
    /// </summary>
    public async Task<Result<string>> CreateAsync(CategoryCreateDto dto)
    {

        //----- 01. Normalize and Validate the Category Name

        var cleanedName = dto.Name.TrimOrEmpty();
        if (!cleanedName.HasValue())
        {
            _logger.LogWarning("Category creation failed: empty name provided.");

            return Result<string>.ValidationFailure(
                new ValidationErrorModel[]
            {
                new( PropertyName: nameof(dto.Name), 
                     ErrorMessage: "Category name cannot be empty or whitespace.")
            });
        }

        var normalizedName = cleanedName.NormalizeKey();


        // ----- 02. Business Rule: Check for Duplicate
        
        bool exists 
            = await _db.Categories.AnyAsync(c => c.Name.ToUpper() == normalizedName);
        if (exists)
        {
            _logger.LogWarning(
                "Category creation failed: duplicate category '{CategoryName}' attempted.",
                cleanedName);

            return Result<string>.Conflict(
                $"Category '{cleanedName}' already exists.");
        }


        // ----- 03. Create the entity and add it to the DataContext

        var entity = new Category
        {
            Name = cleanedName,                   // retain the originally provided trimmed name.
            Description = dto.Description.NullIfWhiteSpace()
        };

        _db.Categories.Add(entity);


        // ----- 04. Flush the update to the Database

        await _db.SaveChangesAsync();


        // ----- 05. Build the Location URI for the newly created entity
        
        var location = $"/api/categories/{entity.CategoryId}";

        
        // ----- 06. Log Success

        _logger.LogInformation(
            "Category created successfully. CategoryId: {CategoryId}, Name: {CategoryName}",
            entity.CategoryId,
            entity.Name);

        // ----- 07. Return Result

        return Result<string>.Created(location);
    }


    /// <summary>
    ///     Retrieves a Category by its identifier.
    /// </summary>
    public async Task<Result<CategoryReadDto>> GetByIdAsync(int id)
    {
        // ----- 01. Fetch the entity

        var entity = await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryId == id);


        // ----- 02. If not found, return NOT FOUND 
        
        if (entity is null)
        {
            _logger.LogWarning(
                "Category retrieval failed. CategoryId: {CategoryId} not found.",
                id);

            return Result<CategoryReadDto>.NotFound(
                $"Category with id '{id}' was not found.");
        }


        // ----- 03. Map to DTO

        var dto = new CategoryReadDto(
            CategoryId: entity.CategoryId,
            Name: entity.Name,
            Description: entity.Description,
            CreatedAtUtc: entity.CreatedAtUtc,
            ModifiedAtUtc: entity.ModifiedAtUtc,
            RowVersion: RowVersionHelper.ToBase64(entity.RowVersion)
        );

        
        // ----- 04. Log Success

        _logger.LogInformation(
            "Category retrieved successfully. CategoryId: {CategoryId}",
            id);


        // ----- 05. Return Result
        return Result<CategoryReadDto>.Success(dto);
    }


    /// <summary>
    ///     Retrieves all the categories.
    /// </summary>
    /// <remarks>
    ///     Ensure that:
    ///     - Return an IReadOnlyList to:
    ///         - prevent accidental modification 
    ///         - communicate intent
    ///         - enterprise convention
    ///     - Never return unordered data.  
    ///        PROBLEM: the DB default order is undefined:
    ///         - UI might flicker
    ///         - Paging later becomes messy
    ///         - Inconsistent results
    ///        SOLUTION: 
    ///        Set the default sort on Name.  This would be easier on the UX.  
    ///        Eg: Populating a drop-down, or populating a grid on the Name instead of ID is more user-friendly.
    ///     - Adopt NoTracking behaviour
    ///     - Implement read-only EF discipline
    ///     - DTOs are projected
    ///     - Result Pattern is adopted
    ///     - Clean Logging
    /// </remarks>
    public async Task<Result<IReadOnlyList<CategoryReadDto>>> GetAllAsync()
    {

        // ----- 01. Fetch the entities (no tracking + ordered)

        var entities = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)                   // default sort on Name
            .ToListAsync();


        // ----- 02. Map to DTO as we need readonly list

        var dtos = entities
            .Select(entity => new CategoryReadDto(
                CategoryId: entity.CategoryId,
                Name: entity.Name,
                Description: entity.Description,
                CreatedAtUtc: entity.CreatedAtUtc,
                ModifiedAtUtc: entity.ModifiedAtUtc,
                RowVersion: RowVersionHelper.ToBase64(entity.RowVersion)
            ))
            .ToList()
            .AsReadOnly();                  // disable change tracking - prevent accidental modification


        // ----- 03. Log only the summary (since we need only meaningful log entries)

        _logger.LogInformation(
            "Retrieved {CategoryCount} categories.",
            dtos.Count);


        // ----- 04. Return the result

        return Result<IReadOnlyList<CategoryReadDto>>.Success(dtos);
    }


    /// <summary>
    ///     Updates an existing category.
    /// </summary>
    /// <remarks>
    ///     Ensure that the UPDATE Operation has:
    ///     - Defensive API Design
    ///     - Concurrency Safe
    ///     - Business Rule Safe
    ///     - Normalization Discipline
    ///     - Structured Logging Audit Friendly
    ///     - Result Pattern
    /// </remarks>
    public async Task<Result> UpdateAsync(int routeId, CategoryUpdateDto dto)
    {

        // ----- 01. Route Vs. Body validation

        if (routeId != dto.CategoryId)
        {
            _logger.LogWarning(
                "Category update failed: route ID {RouteId} does not match payload ID {PayloadId}.",
                routeId,
                dto.CategoryId);

            return Result.ValidationFailure(new ValidationErrorModel[]
            {
                new (nameof(dto.CategoryId), "Route ID and payload ID must match.")
            });
        }


        // ----- 02. Fetch the entity to be updated

        var entity = 
            await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == routeId);

        if (entity is null)
        {
            _logger.LogWarning(
                "Category update failed. CategoryId: {CategoryId} not found.",
                routeId);

            return Result.NotFound($"Category with id '{routeId}' was not found.");
        }


        // ----- 03. Apply RowVersion, needed for Concurrency Check

        byte[] incomingRowVersion;
        try
        {
            incomingRowVersion = RowVersionHelper.FromBase64(dto.RowVersion);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex,
                "Category update failed due to invalid RowVersion format. CategoryId: {CategoryId}",
                routeId);

            return Result.ValidationFailure(new ValidationErrorModel[]
            {
                new (nameof(dto.RowVersion), "Invalid RowVersion format.")
            });
        }

        _db.Entry(entity)
            .Property(e => e.RowVersion)
            .OriginalValue = incomingRowVersion;


        // ----- 04. Normalize and Validate Category Name

        var cleanName = dto.Name.TrimOrEmpty();

        if (!cleanName.HasValue())
        {
            _logger.LogWarning(
                "Category update failed: empty name provided. CategoryId: {CategoryId}",
                routeId);

            return Result.ValidationFailure(new ValidationErrorModel[]
            {
                new (nameof(dto.Name), "Category name cannot be empty or whitespace.")
            });
        }

        var normalizedName = cleanName.NormalizeKey();


        // ----- 05. Business Rule: Duplicate Check (excluding self)

        bool duplicateExists 
            = await _db.Categories
                .AnyAsync(c =>
                    c.CategoryId != routeId 
                    && c.Name.ToUpper() == normalizedName);
        if (duplicateExists)
        {
            _logger.LogWarning(
                "Category update failed: duplicate Category Name '{CategoryName}' attempted.",
                cleanName);

            return Result.Conflict(
                $"Another category with name '{cleanName}' already exists.");
        }


        // ----- 06. Apply changes

        entity.Name = cleanName;
        entity.Description = dto.Description.NullIfWhiteSpace();


        // ----- 07. Commit the changes to the Database, handling Concurrency Check

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogWarning(
                "Category update concurrency conflict. CategoryId: {CategoryId}",
                routeId);

            return Result.Conflict(
                "The record was modified by another user. Please reload and try again.");
        }


        // ----- 08. Log Success

        _logger.LogInformation(
            "Category updated successfully. CategoryId: {CategoryId}",
            routeId);


        // ----- 09. Return Result

        return Result.Accepted();
    }


    /// <summary>
    ///     Deletes an existing category.
    ///     Restricts deletion if products exist.
    /// </summary>
    /// <remarks>
    ///     Ensure that the DELETE Operation is:
    ///     - Concurrency Safe
    ///     - Integrity Safe
    ///     - Business Rule Safe
    ///     - Audit Friendly
    /// </remarks>
    public async Task<Result> DeleteAsync(int routeId, CategoryDeleteDto dto)
    {

        // ----- 01. Route Vs. Body validation

        if (routeId != dto.CategoryId)
        {
            _logger.LogWarning(
                "Category delete failed: route ID {RouteId} does not match payload ID {PayloadId}.",
                routeId,
                dto.CategoryId);

            return Result.ValidationFailure(new[]
            {
                new ValidationErrorModel(
                    nameof(dto.CategoryId),
                    "Route ID and payload ID must match.")
            });
        }


        // ----- 02. Fetch the entity to be deleted

        var entity = await _db.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == routeId);

        if (entity is null)
        {
            _logger.LogWarning(
                "Category delete failed. CategoryId: {CategoryId} not found.",
                routeId);

            return Result.NotFound(
                $"Category with id '{routeId}' was not found.");
        }


        // ----- 03. Apply RowVersion, needed for Concurrency Check

        byte[] incomingRowVersion;
        try
        {
            incomingRowVersion = RowVersionHelper.FromBase64(dto.RowVersion);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex,
                "Category delete failed due to invalid RowVersion format. CategoryId: {CategoryId}",
                routeId);

            return Result.ValidationFailure(new ValidationErrorModel[]
            {
                new( nameof(dto.RowVersion),
                     "Invalid RowVersion format.")
            });
        }

        _db.Entry(entity)
            .Property(e => e.RowVersion)
            .OriginalValue = incomingRowVersion;


        // ----- 04. Business Rule: Don't delete Category that has Product(s). (Referencial Integrity)

        bool hasProducts = 
            await _db.Products.AnyAsync(p => p.CtgryId == routeId);

        if (hasProducts)
        {
            _logger.LogWarning(
                "Category delete restricted. CategoryId: {CategoryId} has associated products.",
                routeId);

            return Result.Conflict(
                "Cannot delete category because associated products exist.");
        }

        
        // ----- 05. Remove the entity

        _db.Categories.Remove(entity);
        

        // ----- 06. Commit the changes to the Database, handling Concurrency Check

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogWarning(
                "Category delete concurrency conflict. CategoryId: {CategoryId}",
                routeId);

            return Result.Conflict(
                "The record was modified by another user. Please reload and try again.");
        }


        // ----- 07. Log Succes

        _logger.LogInformation(
            "Category deleted successfully. CategoryId: {CategoryId}",
            routeId);


        // ----- 08. Return the Result
        
        return Result.Success();
    }

}
