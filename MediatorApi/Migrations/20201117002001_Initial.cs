using Microsoft.EntityFrameworkCore.Migrations;

namespace MediatorApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Name" },
                values: new object[,]
                {
                    { 1, "Author 1", "Book 1" },
                    { 2, "Author 2", "Book 2" },
                    { 3, "Author 3", "Book 3" },
                    { 4, "Author 4", "Book 4" },
                    { 5, "Author 5", "Book 5" },
                    { 6, "Author 6", "Book 6" },
                    { 7, "Author 7", "Book 7" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
