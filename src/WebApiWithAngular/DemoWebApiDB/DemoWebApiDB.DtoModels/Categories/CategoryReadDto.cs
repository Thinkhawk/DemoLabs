namespace DemoWebApiDB.DtoModels.Categories;


/// <summary>
///     DTO Model returned when reading Category data.
/// </summary>
public sealed record class CategoryReadDto
(

    /// <summary>
    ///     Category identifier.
    /// </summary>
    int CategoryId,


    /// <summary>
    ///     Name of Category.
    /// </summary>
    string Name,


    /// <summary>
    ///     Description of Category.
    /// </summary>
    string? Description,


    /// <summary>
    ///     Created timestamp (UTC).
    /// </summary>
    DateTime CreatedAtUtc,


    /// <summary>
    ///     Last modified timestamp (UTC).
    /// </summary>
    DateTime? ModifiedAtUtc,


    /// <summary>
    ///     RowVersion encoded as Base64.
    ///     Used for concurrency control.
    /// </summary>
    string RowVersion

);