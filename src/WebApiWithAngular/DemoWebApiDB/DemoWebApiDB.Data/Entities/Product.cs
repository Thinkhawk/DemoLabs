namespace DemoWebApiDB.Data.Entities;


[Table("Products")]
[Index(nameof(Product.ProductName), Name = "IX_Products_ProductName")]
[Index(nameof(Product.CtgryId), Name = "IX_Products_CtgryId")]
[Index(nameof(CtgryId), nameof(ProductName), 
       IsUnique = true,   
       Name = "UQ_Products_Category_ProductName")]
public sealed class Product
    : AuditableEntity
{

    [Key]                                                   // EF ONLY Attribute - to DB also
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // EF ONLY Attribute - to DB also
    public Guid ProductId { get; set; }


    /// <summary>
    ///     Product Name
    /// </summary>
    // TODO: Add a Custom Validator to enforce that ProductName is unique within the category
    [Display(Name = "Name of the Product")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [StringLength(maximumLength: 50,
                  MinimumLength = 2,
                  ErrorMessage = "{0} should have minimum {2} and maximum {1} characters.")]
    public string ProductName { get; set; } = string.Empty;


    /// <summary>
    ///     Price of the Product
    /// </summary>
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [Range(minimum: 0.0, maximum: short.MaxValue, ErrorMessage = "{0} has to be between {1} and {2}.")]
    [Column(TypeName = "decimal(7,2)")]                         // EF ONLY Attribute - to DB also
    [DefaultValue(0.0)]                                         // EF ONLY Attribute
    public decimal Price { get; set; } = decimal.Zero;


    /// <summary>
    ///     Quantity in Stock
    /// </summary>
    [Display(Name = "Quantity in Stock")]
    public int? QtyInStock { get; set; }


    #region Navigation Properties to Category model (many:1 relationship)

    [Required]
    public int CtgryId { get; set; }                // should be the same data-type as the PK in Categories


    [ForeignKey(nameof(Product.CtgryId))]           // can be replaced by FluentAPI in OnModelCreating()
    public Category? Category { get; set; }

    #endregion

}
