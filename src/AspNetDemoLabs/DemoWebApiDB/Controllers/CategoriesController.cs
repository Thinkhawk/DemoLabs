using DemoWebApiDB.Data;
using DemoWebApiDB.DtoModels.Categories;
using DemoWebApiDB.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DemoWebApiDB.Controllers;


/// <summary>
///     API Controller responsible for CRUD operations on [dbo].[Categories]
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

    private readonly ApplicationDataContext _dbContext;
    private readonly ILogger<CategoriesController> _logger;

    /// <summary>
    /// 	Constructor 
    /// </summary>
	public CategoriesController(
        ApplicationDataContext context,
        ILogger<CategoriesController> logger)
    {
        _dbContext = context;
        _logger = logger;
    }


    #region GET: api/categories

    /// <summary>
    /// Returns all categories.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryReadDtoModel>>> GetCategories()
    {
        //         return await _dbContext.Categories.ToListAsync();


        _logger.LogInformation("Fetching all categories.");

        /********
        var query = from c in _dbContext.Categories.AsNoTracking()
                    select new CategoryReadDtoModel
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Description = c.Description
                    };
        return await query.ToListAsync();
        *****/

        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Select(c => new CategoryReadDtoModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();

        _logger.LogInformation("Returned {Count} categories.", categories.Count);

        return Ok(categories);
    }

    #endregion


    #region GET: api/categories/{id}

    /// <summary>
    /// Returns a specific category by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryReadDtoModel>> GetCategory(int id)
    {
        _logger.LogInformation("Fetching category with Id {CategoryId}.", id);

        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.CategoryId == id)
            .Select(c => new CategoryReadDtoModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            })
            .FirstOrDefaultAsync();

        if (category is null)
        {
            _logger.LogWarning("Category with Id {CategoryId} not found.", id);

            // return NotFound($"Category with Id {id} was not found.");

            return NotFound(new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = $"Category with Id {id} was not found.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });

        }

        return Ok(category);
    }

    #endregion


    #region GET api/categories/GetWithProducts/{id}


    /// <summary>
    /// Returns a specific category by ID.
    /// </summary>
    [HttpGet("GetWithProducts/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithProducts(int id)
    {
        _logger.LogInformation("Fetching category and its products for Id {CategoryId}.", id);

        var category = await _dbContext.Categories
            .AsNoTracking()
            // .Include(c => c.Products)
            .Where(c => c.CategoryId == id)
            .Select(c => new 
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                Products = c.Products!.Select( p =>
                 new {
                    ID = p.Id,
                    Name = p.ProductName
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (category is null)
        {
            _logger.LogWarning("Category with Id {CategoryId} not found.", id);

            // return NotFound($"Category with Id {id} was not found.");

            return NotFound(new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = $"Category with Id {id} was not found.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });

        }

        return Ok(category);
    }

    #endregion


    #region GET api/categories/GetWithProductsWithSlNo/{id}

    /// <summary>
    /// Returns a specific category by ID.
    /// </summary>
    [HttpGet("GetWithProductsWithSlNo/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithProductsWithSlNo(int id)
    {
        _logger.LogInformation("Fetching category and its products for Id {CategoryId}.", id);

        var c = await _dbContext.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);

        if (c is null)
        {
            _logger.LogWarning("Category with Id {CategoryId} not found.", id);

            // return NotFound($"Category with Id {id} was not found.");

            return NotFound(new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = $"Category with Id {id} was not found.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });

        }

        var category = new
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Description = c.Description,
            Products = c.Products!.Select((p, ndx) =>
                new
                {
                    SlNo = ndx + 1,
                    ID = p.Id,
                    Name = p.ProductName
                }).ToList()
        };

        return Ok(category);
    }

    #endregion


    #region POST: api/categories

    /// <summary>
    /// Creates a new Category.
    /// Returns 201 Created with Location header only.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCategory(
        [FromBody] CategoryCreateDtoModel dto)
    {
        _logger.LogInformation("Attempting to create a new category with Name {CategoryName}.", dto.Name);

        // Automatic DataAnnotation validation
        if (! ModelState.IsValid)
        {
            _logger.LogWarning("Validation failed while creating category.");
            return BadRequest(ModelState);              // HTTP 400 error with RFC 
            // return ValidationProblem(ModelState);
        }

        // Prevent duplicate category names
        bool exists 
            = await _dbContext.Categories.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

        if (exists)
        {
            // STRUCTURED LOGGING
            // _logger.LogWarning($"Duplicate category name detected: {dto.Name}.");            // bad logging
            _logger.LogWarning("Duplicate category name detected: {CategoryName}.", dto.Name);  // BEST practices

            ModelState.AddModelError(nameof(Category.Name), "Category Name must be unique.");

            // return BadRequest(ModelState);
            return ValidationProblem(ModelState);           // respond with HTTP 400 with RFC 9110 compliant error message
        }

        var category = new Category
        {
            Name = dto.Name.Trim(),
            Description = dto.Description
        };

        // insert the object to the Categories collection in the DataContext
        _dbContext.Categories.Add(category);

        // push the changes to the database
        await _dbContext.SaveChangesAsync();

        // STRUCTURED LOGGING
        // _logger.LogInformation($"Category created with Id {category.CategoryId}");
        _logger.LogInformation("Category created successfully with Id {CategoryId}.", category.CategoryId);

        // Return HTTP 201 "Created" with only location header
        return CreatedAtAction(
            nameof(GetCategory),
            new { id = category.CategoryId },
            null);
    }

    #endregion


    #region PUT: api/categories/{id}

    /// <summary>
    /// Updates an existing category.
    /// Returns 202 Accepted if successful.
    /// Returns 400 Rejected if validation fails.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCategory(
        int id,                                             // categoryId to update
        [FromBody] CategoryUpdateDtoModel dto)              // full category object to update the found entry in DB
    {
        _logger.LogInformation("Attempting to update category with Id {CategoryId}.", id);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Validation failed while updating category Id {CategoryId}.", id);
            return ValidationProblem(ModelState);
        }

        if (dto.CategoryId != id)
        {
            _logger.LogWarning("Update failed. You are trying to update the wrong Category!");

            // return BadRequest("Updating the wrong Category object!");
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid Resource!",
                Detail = "Updating the wrong Category object!.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });
        }

        var category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            _logger.LogWarning("Update failed. Category with Id {CategoryId} not found.", id);

            // return NotFound($"Category with Id {id} was not found.");
            return NotFound(new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = $"Category with Id {id} was not found.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });
        }

        // Check duplicate name (excluding current record)
        bool duplicate = await _dbContext.Categories
            .AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower() && c.CategoryId != id);

        if (duplicate)
        {
            _logger.LogWarning("Duplicate category name detected during update. Name: {CategoryName}.", dto.Name);


            ModelState.AddModelError(nameof(Category.Name), "Category Name must be unique.");

            //return BadRequest(ModelState); // Rejected
            return ValidationProblem(ModelState);
        }

        // Update the properties of the model retrieved from the DataContext
        category.Name = dto.Name.Trim();
        category.Description = dto.Description;

        await _dbContext.SaveChangesAsync();

        return Accepted(new
        {
            Message = "Category updated successfully."
        });
    }

    #endregion


    #region DELETE: api/categories/{id}

    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        _logger.LogInformation("Attempting to delete category with Id {CategoryId}.", id);

        var category = await _dbContext.Categories.FindAsync(id);

        if (category is null)

        {
            _logger.LogWarning("Delete failed. Category with Id {CategoryId} not found.", id);

            // return NotFound($"Category with Id {id} was not found.");
            return NotFound(new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = $"Category with Id {id} was not found.",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });
        }

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Category with Id {CategoryId} deleted successfully.", id);

        return NoContent();
    }

    #endregion

}
