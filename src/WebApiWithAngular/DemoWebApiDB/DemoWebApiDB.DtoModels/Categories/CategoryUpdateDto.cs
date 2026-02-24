namespace DemoWebApiDB.DtoModels.Categories;


/// <summary>
///     DTO Model used when updating an existing Category.
/// </summary>
public sealed record class CategoryUpdateDto
(

    /// <summary>
    ///     Category identifier.
    /// </summary>
    [property: Required]
    int CategoryId,


    /// <summary>
    ///     Updated Category Name.
    ///     Must remain unique.
    /// </summary>
    [property: Required(ErrorMessage = "Category name is required.")]
    [property: StringLength(
        maximumLength : 100, 
        MinimumLength = 2,
        ErrorMessage = "Category name must be between 2 and 100 characters.")]
    string Name,


    /// <summary>
    ///     Optional Description of the Category.
    /// </summary>
    [property: StringLength(4000,
        ErrorMessage = "Description cannot exceed 4000 characters.")]
    string? Description,


    /// <summary>
    ///     RowVersion used for optimistic concurrency.
    ///     Must be returned unchanged from GET response.
    /// </summary>
    [property: Required(ErrorMessage = "RowVersion is required for update.")]
    string RowVersion

);