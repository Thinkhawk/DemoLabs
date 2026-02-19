using System.ComponentModel.DataAnnotations;

namespace DemoWebApiDB.DtoModels.Categories;

/// <summary>
///     DTO received in the Request and used when creating a Category.
///     NOTE: CategoryId intentionally excluded to prevent overposting, 
///           and also because it is a Database Generated property.
/// </summary>
public class CategoryCreateDtoModel
{

    [Display(Name = "Category Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }

}
