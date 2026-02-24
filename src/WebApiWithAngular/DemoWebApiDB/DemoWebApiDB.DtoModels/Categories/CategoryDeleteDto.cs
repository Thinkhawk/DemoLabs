namespace DemoWebApiDB.DtoModels.Categories;


/// <summary>
///     DTO Model used when deleting a Category.
///     Requires RowVersion for concurrency validation.
/// </summary>
public sealed record class CategoryDeleteDto
(
    [property: Required]
    int CategoryId,

    [property: Required]
    string RowVersion
);