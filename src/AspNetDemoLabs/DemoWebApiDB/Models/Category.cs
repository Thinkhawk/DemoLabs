using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;


namespace DemoWebApiDB.Models;

/****************
        CREATE TABLE [dbo].[Categories]
        (
            [CategoryId] int NOT NULL IDENTITY(1,1)
            , [Name] varchar(100) NOT NULL,
            , [Description] nvarchar(MAX) NULL

            , CONSTRAINT [PK_Categories] PRIMARY KEY ( [CategoryId] )
        )
 * ******/

// This Class represents the MODEL mappped to a TABLE IN THE DATABASE

[Table(name: "Categories", Schema = "dbo")]                     // EF ONLY attribute
[Index(nameof(Name), IsUnique = true)]			                // EF ONLY attribute
public class Category
{

    [Key]                                                       // EF ONLY attribute: Primary Key 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       // EF ONLY attribute
    public int CategoryId { get; set; }



    /**********************
        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name.Length > 100)
                {
                    throw new InvalidDataException("Category Name cannot have more than 100 characters");
                }
                _name = value;
            }
        }
    ************/

    [Display(Name = "Category Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(length: 100, ErrorMessage = "{0} cannot have more than {1} characters.")]
    [Column(TypeName = "varchar(100)")]                         // EF ONLY attribute
    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }

}


/**********************
    private string _name = string.Empty;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (_name.Length > 100)
            {
                throw new InvalidDataException("Category Name cannot have more than 100 characters");
            }
            _name = value;
        }
    }
************/
