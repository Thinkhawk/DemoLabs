using Microsoft.EntityFrameworkCore;

using DemoWebApiDB.Models;

namespace DemoWebApiDB.Data
{
    public class ApplicationDataContext
        : DbContext
    {

        // Register the model to be exposed as properties from the DataContext
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /************* INSTEAD OF DEFINING THE [ForeignKey] Attribute IN the Product model
            
            // Fluent API version
            modelBuilder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CtgryId)
                        .OnDelete(DeleteBehavior.Cascade);

             ***********/
                        

            base.OnModelCreating(modelBuilder);
        }

    }
}
