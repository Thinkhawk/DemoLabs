namespace DemoWebApiDB.Data.Data;


/// <summary>
///     Primary EF Core DbContext for the application.
///     Integrates ASP.NET Identity and domain entities.
/// </summary>
public class ApplicationDbContext
    : IdentityDbContext
{

    // Register the models to be exposed as properties from the DataContext
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductWithCategoryReadModel> ProductsWithCategoryView { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /************* INSTEAD OF DEFINING THE [ForeignKey] Attribute IN the Product model
            // Fluent API version
            modelBuilder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CtgryId)
                        .OnDelete(DeleteBehavior.Cascade);
         ***********/

        //----------- Check Constraints

        modelBuilder.Entity<Category>()
               .ToTable(t => t.HasCheckConstraint(
                   name: "CK_Categories_Name_NotBlank",
                   sql: "LEN(LTRIM(RTRIM(Name))) > 0"));

        modelBuilder.Entity<Product>()
               .ToTable(t => t.HasCheckConstraint(
                   name: "CK_Products_Price_NonNegative",
                   sql: "Price >= 0"));

        modelBuilder.Entity<Product>()
               .ToTable(t => t.HasCheckConstraint(
                   name: "CK_Products_Qty_NonNegative",
                   sql: "QtyInStock IS NULL OR QtyInStock >= 0"));


        //----------- Configure Views

        modelBuilder.Entity<ProductWithCategoryReadModel>()
               .ToView("vw_ProductsWithCategory")
               .HasNoKey();
    }

}
