using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DemoWebApiDB.Models;

/*******************
        CREATE TABLE [Products]
        (
            [Id] uniqueidentifier NOT NULL 
            , [ProductName] nvarchar(50) NOT NULL
            , [Price] decimal(7,2) NOT NULL
                CONSTRAINT [DF_Products_Price] DEFAULT (0.0)
            , [QtyInStock] int NULL
            , [CtgryId] int NOT NULL

            , CONSTRAINT [PK_Products] PRIMARY KEY
            , CONSTRAINT [FK_Products_Categories_CtgryId] FOREIGN KEY [CtgryId] REFERENCES [Category] ( [CategoryId] )
        )
 */


[Table("Products")]                                         // EF ONLY Attribute - to DB also
[Index(nameof(Product.ProductName))]                        // EF ONLY Attribute - to DB also
public class Product
{

    [Key]                                                   // EF ONLY Attribute - to DB also
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // EF ONLY Attribute - to DB also
    public Guid Id { get; set; }


    // TODO: Add a Custom Validator to enforce that ProductName is unique within the category
    [Display(Name = "Name of the Product")]
    [Required(ErrorMessage = "{0} cannot be empty.")]
    [StringLength(maximumLength : 50, 
                  MinimumLength = 2,
                  ErrorMessage = "{0} should have minimum {2} and maximum {1} characters.")]
    public string ProductName { get; set;  } = string.Empty;


    [Required(ErrorMessage = "{0} cannot be empty.")]
    [Range(minimum: 0.0, maximum: short.MaxValue, ErrorMessage = "{0} has to be between {1} and {2}.")]
    [Column(TypeName = "decimal(7,2)")]                         // EF ONLY Attribute - to DB also
    [DefaultValue(0.0)]                                         // EF ONLY Attribute
    public decimal Price { get; set; } = decimal.Zero;

    public int? QtyInStock { get; set; }


    #region Navigation Properties to Category model (many:1 relationship)

    [Required]
    public int CtgryId { get; set; }                // should be the same data-type as the PK in Categories


    // [XmlIgnore]
    // [JsonIgnore]
    [ForeignKey(nameof(Product.CtgryId))]           // can be replaced by FluentAPI in OnModelCreating()
    public Category? Category { get; set; }

    #endregion

}
