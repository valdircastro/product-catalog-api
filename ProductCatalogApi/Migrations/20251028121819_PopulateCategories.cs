using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "Insert into Category(Name, ImageUrl) Values ('Beverages', 'beverages.jpg')"
            );
            migrationBuilder.Sql(
                "Insert into Category(Name, ImageUrl) Values('Snacks', 'snacks.jpg')"
            );
            migrationBuilder.Sql(
                "Insert into Category(Name, ImageUrl) Values('Desserts', 'desserts.jpg')"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Category");
        }
    }
}
