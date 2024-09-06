using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "People",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Gender = table.Column<byte>(type: "tinyint", nullable: false, comment: "0 => Male \r\n1 => Female"),
            //        Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Persons__AA2FFB8516B9919B", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Clients",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        PinCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        PersonId = table.Column<int>(type: "int", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Clients__7AD04FF10A571613", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Clients__Perso__398D8EEE",
            //            column: x => x.PersonId,
            //            principalTable: "People",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        Permissions = table.Column<int>(type: "int", nullable: false),
            //        PersonId = table.Column<int>(type: "int", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Users__1788CCACAB45311C", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__Users__PersonID__3C69FB99",
            //            column: x => x.PersonId,
            //            principalTable: "People",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RegisterLogins",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RegisterLoginsDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Register__A13C533E51AEE68A", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK__RegisterL__UserI__02FC7413",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TransfersLogs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "datetime", nullable: false),
            //        SourceClientId = table.Column<int>(type: "int", nullable: false),
            //        DestinationClientId = table.Column<int>(type: "int", nullable: false),
            //        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        SourceBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        DestinationBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        CreatedByUserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TransfersLog", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TransfersLog_Clients",
            //            column: x => x.SourceClientId,
            //            principalTable: "Clients",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_TransfersLog_Clients1",
            //            column: x => x.DestinationClientId,
            //            principalTable: "Clients",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_TransfersLogs_Users",
            //            column: x => x.CreatedByUserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Clients_PersonId",
            //    table: "Clients",
            //    column: "PersonId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Clients__AA2FFB84B567F7DF",
            //    table: "Clients",
            //    column: "PersonId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Clients__BE2ACD6FC14AB4AE",
            //    table: "Clients",
            //    column: "AccountNumber",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RegisterLogins_UserId",
            //    table: "RegisterLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransfersLogs_CreatedByUserId",
            //    table: "TransfersLogs",
            //    column: "CreatedByUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransfersLogs_DestinationClientId",
            //    table: "TransfersLogs",
            //    column: "DestinationClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransfersLogs_SourceClientId",
            //    table: "TransfersLogs",
            //    column: "SourceClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_PersonId",
            //    table: "Users",
            //    column: "PersonId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Users__AA2FFB84DC0D1567",
            //    table: "Users",
            //    column: "PersonId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ__Users__C9F28456AD961997",
            //    table: "Users",
            //    column: "Username",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "RegisterLogins");

            //migrationBuilder.DropTable(
            //    name: "TransfersLogs");

            //migrationBuilder.DropTable(
            //    name: "Clients");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "People");
        }
    }
}
