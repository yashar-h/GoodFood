using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodFoodCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Slug = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 90, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Slug);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Slug = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 90, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Slug);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeSlug = table.Column<string>(nullable: true),
                    IngredientSlug = table.Column<string>(nullable: true),
                    Amount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredient_IngredientSlug",
                        column: x => x.IngredientSlug,
                        principalTable: "Ingredient",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipe_RecipeSlug",
                        column: x => x.RecipeSlug,
                        principalTable: "Recipe",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientSlug",
                table: "RecipeIngredient",
                column: "IngredientSlug");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_RecipeSlug",
                table: "RecipeIngredient",
                column: "RecipeSlug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
