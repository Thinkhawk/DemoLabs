using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Infrastructure.Results;
using DemoWebApiDB.Services.Categories;

namespace DemoWebApiDB.Controllers;


/// <summary>
///     Provides API endpoints for managing product categories.
/// </summary>
/// <remarks>
///     This controller demonstrates:
///     - Clean separation of concerns
///     - Result pattern usage
///     - RFC7807 compliant error handling
///     - Concurrency-safe updates and deletes
///     - Enterprise-grade API design
///     
/// TODO: handle paginated result in the GET ALL action method.
/// </remarks>
[Route("api/[controller]")]
public sealed class CategoriesController : BaseApiController
{

    private readonly CategoryService _service;


    /// <summary>
    ///     Initialize a new instance of <see cref="CategoriesController"/>.
    /// </summary>
    /// <param name="service">Category service instance.</param>
    public CategoriesController(CategoryService service)
    {
        _service = service;
    }


    #region CREATE

    /// <summary>
    ///     Create a new category.
    /// </summary>
    /// <remarks>
    ///     This endpoint:
    ///     - Normalizes category name
    ///     - Prevents duplicates
    ///     - Returns only the Location of created resource
    /// 
    ///     Returns 201 Created on Success with Location Header.
    ///     Clients should perform a GET using returned location if details are required.
    /// </remarks>
    /// <param name="dto">Category creation data.</param>
    /// <returns>201 Created with Location header.</returns>
    [HttpPost]
    [EndpointSummary("Create new Category")]
    [EndpointDescription(
        "Creates a new Category after validating for duplicates and normalizing input. "
        + "Returns 201 CREATED with the Location pointing to newly created resource in the Response Header."
    )]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        if (result.IsSuccess && result.Status == ResultStatus.Created)
        {
            // Location returned from service
            return Created(result.Data!, null);
        }

        return HandleResult(result);
    }

    #endregion


    #region GET BY ID

    /// <summary>
    ///     Retrieve a Category by its identifier.
    /// </summary>
    /// <param name="id">Category identifier.</param>
    /// <returns>Category details.</returns>
    [HttpGet("{id:int}")]
    [EndpointSummary("Retrieve Category by ID")]
    [EndpointDescription(
        "Retrieves a specific Category by its identifier. " 
        + "Returns full category details including audit fields and RowVersion."
    )]
    [ProducesResponseType(typeof(CategoryReadDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return HandleResult(result);
    }

    #endregion


    #region GET ALL

    /// <summary>
    ///     Retrieve all categories.
    /// </summary>
    /// <remarks>
    ///     Returns categories ordered by name.
    /// </remarks>
    /// <returns>List of categories.</returns>
    [HttpGet]
    [EndpointSummary("Retrieve all Categories")]
    [EndpointDescription(
        "Returns all Categories ordered by Name. "
        + "NOTE: This endpoint currently does not implement paging."
    )]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryReadDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _service.GetAllAsync();
        return HandleResult(result);
    }

    #endregion


    #region UPDATE

    /// <summary>
    ///     Update an existing category.
    /// </summary>
    /// <remarks>
    ///     Requires:
    ///     - Matching route and payload IDs
    ///     - Valid RowVersion for concurrency control
    /// 
    ///     Returns 202 Accepted on success.
    /// </remarks>
    /// <param name="id">Category identifier.</param>
    /// <param name="dto">Updated category data.</param>
    /// <returns>Update result.</returns>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update an existing Category")]
    [EndpointDescription(
        "Updates a Category using optimistic concurrency validation. "
        + "Requires a valid RowVersion and matching route/payload identifiers. "
        + "Returns 202 Accepted on successful update."
    )]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCategory(
        int id,
        [FromBody] CategoryUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return HandleResult(result);
    }

    #endregion


    #region DELETE

    /// <summary>
    ///     Deletes an existing category.
    /// </summary>
    /// <remarks>
    ///     Requirements:
    ///     - Matching route and payload ID
    ///     - Valid RowVersion
    ///     - Category must not have associated products
    ///     - Deletion is restricted if dependent products exist.
    ///     
    ///     Returns NoContent on success.
    /// </remarks>
    /// <param name="id">Category identifier.</param>
    /// <param name="dto">Delete request payload containing RowVersion.</param>
    /// <returns>NoContent on success.</returns>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete existing Category")]
    [EndpointDescription(
        "Deletes a Category using optimistic concurrency validation. "
        + "Deletion is restricted if dependent Product(s) exist. "
        + "Returns 204 NoContent on successful deletion."
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCategory(
        int id, 
        [FromBody] CategoryDeleteDto dto)
    {
        var result = await _service.DeleteAsync(id, dto);
        return HandleResult(result);
    }

    #endregion

}
