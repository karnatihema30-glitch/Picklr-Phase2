using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.ClubID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    AvailableDays = table.Column<string>(type: "TEXT", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_Programs_Clubs_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Clubs",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "ClubID", "Description", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Our flagship downtown club with 10 indoor courts.", "123 Main St, Chicago, IL", "Picklr Downtown" },
                    { 2, "A vibrant outdoor facility with 8 courts and a pro shop.", "456 Oak Ave, Evanston, IL", "Picklr Northside" },
                    { 3, "Modern indoor pickleball club located in New York.", "789 Madison Ave, New York, NY", "Picklr New York" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { 1, "alice@picklr.com", "Alice", "Smith", "Admin" },
                    { 2, "bob@picklr.com", "Bob", "Jones", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "ProgramID", "AvailableDays", "ClubID", "Description", "Fee", "Name" },
                values: new object[,]
                {
                    { 1, "Monday, Wednesday, Friday", 1, "Drop-in open play for new players. No experience needed.", 10.00m, "Beginner Open Play" },
                    { 2, "Tuesday, Thursday", 1, "Weekly skill-building clinic led by a certified coach.", 25.00m, "Intermediate Clinic" },
                    { 3, "Saturday, Sunday", 2, "Competitive round-robin tournament for rated players.", 40.00m, "Advanced Tournament" },
                    { 4, "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday", 3, "The program is designed for beginners.", 10.00m, "Picklr 101" },
                    { 5, "Saturday", 2, "Weekend social play for all skill levels.", 0.00m, "Picklr Social" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ClubID",
                table: "Programs",
                column: "ClubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
