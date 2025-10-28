using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "Insert into product(Name, Description, Price, ImageUrl, Stock, RegisterDate, CategoryId)"
                    + "Values('Coke Diet', 'Soda 350ml', '1.90', 'coke.jpg', 50, now(), 1)"
            );
            migrationBuilder.Sql(
                "Insert into product(Name, Description, Price, ImageUrl, Stock, RegisterDate, CategoryId)"
                    + "Values('Tuna Sandwich', 'Tuna sandwich with mayo', '3.00', 'tuna_sandwich.jpg', 10, now(), 2)"
            );
            migrationBuilder.Sql(
                "Insert into product(Name, Description, Price, ImageUrl, Stock, RegisterDate, CategoryId)"
                    + "Values('Egg Cream Pastry', 'Sweet egg cream pastry', '4.00', 'egg_cream_pastry.jpg', 20, now(), 3)"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from product");
        }
    }
}
