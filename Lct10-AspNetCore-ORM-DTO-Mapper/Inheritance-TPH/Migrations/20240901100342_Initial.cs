using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inheritance.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    MeowSound = table.Column<string>(type: "TEXT", nullable: true),
                    BarkSound = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Discriminator", "MeowSound", "Name" },
                values: new object[] { 1, "Cat", "Lasagna!", "Garfield" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "BarkSound", "Discriminator", "Name" },
                values: new object[] { 2, "Glory to Ukraine!", "Dog", "Patron" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
