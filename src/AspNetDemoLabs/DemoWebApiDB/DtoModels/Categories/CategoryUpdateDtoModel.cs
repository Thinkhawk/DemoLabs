using System.ComponentModel.DataAnnotations;

namespace DemoWebApiDB.DtoModels.Categories;

/// <summary>
///     DTO Model used when updating a Category.
///     NOTE: CategoryId is also part of the body 
///           and it also comes from route parameter in the PUT request.
/// </summary>

public class CategoryUpdateDtoModel
{

    [Required]
    public int CategoryId { get; set; }


    [Display(Name = "Category Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be empty.")]
    [MaxLength(100, ErrorMessage = "{0} cannot have more than {1} characters.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

}
