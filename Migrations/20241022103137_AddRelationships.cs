using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "_MovieItemsID",
                table: "Actors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actors__MovieItemsID",
                table: "Actors",
                column: "_MovieItemsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movie__MovieItemsID",
                table: "Actors",
                column: "_MovieItemsID",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movie__MovieItemsID",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors__MovieItemsID",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "_MovieItemsID",
                table: "Actors");
        }
    }
}
