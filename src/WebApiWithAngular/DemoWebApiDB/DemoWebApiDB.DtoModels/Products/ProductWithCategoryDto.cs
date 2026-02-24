namespace DemoWebApiDB.DtoModels.Products;


/// <summary>
///     DTO Model representing the details of Product along with its Category.
///     Returned from view-based queries.
/// </summary>
/// <remarks>
///     Since this represents the data returned from a VIEW:
///     - No RowVersion
///     - No audit fields
/// </remarks>
public sealed record class ProductWithCategoryDto
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
    ///     Price of Product.
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
    string CategoryName

);
