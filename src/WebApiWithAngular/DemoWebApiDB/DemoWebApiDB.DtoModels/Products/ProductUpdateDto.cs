namespace DemoWebApiDB.DtoModels.Products;


/// <summary>
///     DTO Model used when updating an existing Product.
/// </summary>
public sealed record class ProductUpdateDto
(

    /// <summary>
    ///     Product identifier.
    /// </summary>
    [property: Required]
    int ProductId,


    /// <summary>
    ///     Updated product name.
    /// </summary>
    [property: Required(ErrorMessage = "Product name is required.")]
    [property: StringLength(
        maximumLength: 50, 
        MinimumLength = 2,
        ErrorMessage = "Product Name must be between {2} and {1} characters.")]
    string ProductName,


    /// <summary>
    ///     Updated price.
    /// </summary>
    [property: Range(
        minimum: 0, 
        maximum: short.MaxValue,
        ErrorMessage = "Price must be between {1} and {2}.")]
    decimal Price,


    /// <summary>
    ///     Updated quantity in stock.
    /// </summary>
    [property: Range(
        minimum: 0,
        maximum: int.MaxValue,
        ErrorMessage = "Quantity cannot be negative.")]
    int? QtyInStock,


    /// <summary>
    ///     Category to which the product belongs to.
    ///     Allows moving product across categories.
    /// </summary>
    [property: Required(ErrorMessage = "CategoryId is required.")]
    int CategoryId,


    /// <summary>
    ///     RowVersion used for optimistic concurrency.
    ///     Must be returned unchanged from GET response.
    /// </summary>
    [property: Required(ErrorMessage = "RowVersion is required for update.")]
    string RowVersion

);