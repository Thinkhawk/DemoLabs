namespace DemoWebApiDB.DtoModels.Products;


/// <summary>
///     DTO returned when reading Product data.
/// </summary>
public sealed record class ProductReadDTO
(

    /// <summary>
    ///     Product identifier.
    /// </summary>
    Guid ProductId,


    /// <summary>
    ///     Product name.
    /// </summary>
    string ProductName,


    /// <summary>
    /// Price of Product.
    /// </summary>
    decimal Price,


    /// <summary>
    ///     Quantity in stock.
    /// </summary>
    int? QtyInStock,


    /// <summary>
    ///     Category identifier.
    /// </summary>
    int CategoryId,


    /// <summary>
    ///     Category name (for display).
    /// </summary>
    string CategoryName,


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