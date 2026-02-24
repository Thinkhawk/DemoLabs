using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebApiDB.Migrations
{
    /// <inheritdoc />
    public partial class Ver02ProductWithCategoryView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // ---------- VIEW ----------
            migrationBuilder.Sql(@"
CREATE OR ALTER VIEW dbo.vw_ProductsWithCategory
AS
SELECT
    p.ProductId,
    p.ProductName,
    p.Price,
    p.QtyInStock,
    c.CategoryId,
    c.Name AS CategoryName
FROM dbo.Products p
INNER JOIN dbo.Categories c ON p.CtgryId = c.CategoryId;
    ");

            // ---------- STORED PROCEDURE ----------
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE dbo.sp_GetCategoryProductSummary
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.CategoryId,
        c.Name AS CategoryName,
        COUNT(p.ProductId) AS ProductCount
    FROM dbo.Categories c
    LEFT JOIN dbo.Products p ON c.CategoryId = p.CtgryId
    GROUP BY c.CategoryId, c.Name
    ORDER BY c.Name;
END;
    ");

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS dbo.vw_ProductsWithCategory;");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS dbo.sp_GetCategoryProductSummary;");
        }
    }
}
