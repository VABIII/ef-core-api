using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable( // Function that creates our actual db table
                name: "Countries", // Name of db table
                columns: table => new // Creates the columns names for the table based on what is declared in the Country and Hotel classes
                { // Columns name and type are set
                    Id = table.Column<int>(type: "int", nullable: false)  
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => // Identifies the tables the primary key
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false) // CountryId being a Foriegn Key
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id); // The primary key gets set before the foreign key does
                    
                    // Foreign Key gets set and joins the FK to the table and column it is pointing to
                    table.ForeignKey(
                        name: "FK_Hotels_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); //  Cascade - If you delete the country, it will also delete all the hotels associated with it as well
                        //onDelete: ReferentialAction.Restrict);  Restrict - This would prevent the hotels from being deleted as well
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CountryId",
                table: "Hotels",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) // Removes the tables created in the event a migration needs to be reversed
        {                                                               // Tables should be removed in reverse order than they were added
            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
