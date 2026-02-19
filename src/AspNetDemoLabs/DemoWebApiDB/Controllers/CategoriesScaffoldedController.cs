using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoWebApiDB.Data;
using DemoWebApiDB.Models;


namespace DemoWebApiDB.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesScaffoldedController : ControllerBase
{
    private readonly ApplicationDataContext _dbContext;

    public CategoriesScaffoldedController(ApplicationDataContext context)
    {
        _dbContext = context;
    }

    // GET: api/CategoriesScaffolded
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    // GET: api/CategoriesScaffolded/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    // PUT: api/CategoriesScaffolded/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        _dbContext.Entry(category).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/CategoriesScaffolded
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
    }

    // DELETE: api/CategoriesScaffolded/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _dbContext.Categories.Any(e => e.CategoryId == id);
    }
}
