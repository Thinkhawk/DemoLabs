namespace DemoWebApiDB.DtoModels.Products;


/// <summary>
///     DTO Model used when creating a new Product.
/// </summary>
public sealed record class ProductCreateDto
(

    /// <summary>
    ///     Name of the product.
    ///     Must be unique within a category.
    /// </summary>
    [property: Required(ErrorMessage = "Product name is required.")]
    [property: StringLength(
        maximumLength: 50, 
        MinimumLength = 2,
        ErrorMessage = "Product Name must be between {2} and {1} characters.")]
    string ProductName,


    /// <summary>
    ///     Price of the Product.
    /// </summary>
    [property: Range(
        minimum: 0,
        maximum: short.MaxValue,
        ErrorMessage = "Price must be between {1} and {2}.")]
    decimal Price,


    /// <summary>
    ///     Quantity in stock.
    /// </summary>
    [property: Range(
        minimum: 0, 
        maximum: int.MaxValue,
        ErrorMessage = "Quantity cannot be negative.")]
    int? QtyInStock,


    /// <summary>
    ///     Category to which the product belongs to.
    /// </summary>
    [property: Required(ErrorMessage = "CategoryId is required.")]
    int CategoryId

);