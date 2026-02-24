namespace DemoWebApiDB.DtoModels.Categories;


/// <summary>
///     DTO Model used when creating a new Category.
/// </summary>
public sealed record class CategoryCreateDto
(

    /// <summary>
    ///     Name of the category.
    ///     Must be unique.
    /// </summary>
    [property: Required(ErrorMessage = "Category name is required.")]
    [property: StringLength(
        maximumLength: 100, 
        MinimumLength = 2,
        ErrorMessage = "Category name must be between 2 and 100 characters.")]
    string Name,


    /// <summary>
    ///     Optional Description of the Category.
    /// </summary>
    /// <remarks>
    ///     Mapped to the SQL DB column: nvarchar(MAX)
    ///     Thus, we can have a max of 4000 UTF-8 characters only.
    /// </remarks>
    [property: StringLength(
        maximumLength: 4000,
        ErrorMessage = "Description cannot exceed 4000 characters.")]
    string? Description

);
