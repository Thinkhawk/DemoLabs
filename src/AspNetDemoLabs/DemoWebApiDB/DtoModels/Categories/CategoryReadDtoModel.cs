namespace DemoWebApiDB.DtoModels.Categories;

/// <summary>
/// 	DTO Model returned in the HTTP Response.
/// </summary>
public class CategoryReadDtoModel
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

}
