using Microsoft.EntityFrameworkCore;

using DemoWebApiDB.Models;

namespace DemoWebApiDB.Data
{
    public class ApplicationDataContext
        : DbContext
    {

        // Register the model to be exposed as properties from the DataContext
        public DbSet<Category> Categories { get; set; }


        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) 
            : base(options)
        {
        }

    }
}
