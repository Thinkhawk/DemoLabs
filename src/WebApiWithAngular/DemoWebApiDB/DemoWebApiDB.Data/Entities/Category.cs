
namespace DemoWebApiDB.Data.Entities;


/// <summary>
///     Represents a product category.
/// </summary>
[Table("Categories", Schema = "dbo")]
[Index(nameof(Name), IsUnique = true, Name = "UQ_Categories_Name")]
public sealed class Category
    : AuditableEntity
{

    /// <summary>
    ///     Primary Key (Identity)
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }


    /// <summary>
    ///     Name of the Category. Must be unique.
    /// </summary>
    [Display(Name = "Category Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(length: 100, ErrorMessage = "{0} cannot have more than {1} characters.")]
    [Column(TypeName = "varchar(100)")]                         // EF ONLY attribute
    public string Name { get; set; } = string.Empty;


    /// <summary>
    ///     Optional description of the category.
    /// </summary>
    public string? Description { get; set; }



    #region Navigation Properties to Product model (1:many relationship)

    /// <summary>
    ///     Products belonging to this category.
    /// </summary>
    /// <remarks>
    ///     To avoid cyclic reference serialization (since Product also refers to Category), 
    ///         ensure that this is suppressed in XML Serialization and JSON Serialization.
    /// </remarks>
    // [XmlIgnore]                      // needed only when we require to suppress during serialization
    // [JsonIgnore]                     // needed only when we require to suppress during serialization
    public ICollection<Product>? Products { get; set; }

    #endregion

}