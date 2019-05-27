using Microsoft.EntityFrameworkCore.Migrations;

namespace Graduates.Data.Migrations
{
    public partial class ProductionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "NewsCategoryId",
                table: "Article");

            migrationBuilder.AlterColumn<long>(
                name: "ArticleCategoryId",
                table: "Article",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article");

            migrationBuilder.AlterColumn<long>(
                name: "ArticleCategoryId",
                table: "Article",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "NewsCategoryId",
                table: "Article",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleCategory_ArticleCategoryId",
                table: "Article",
                column: "ArticleCategoryId",
                principalTable: "ArticleCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
