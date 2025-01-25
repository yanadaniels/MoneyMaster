using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMaster.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "TransactionTypes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Transactions",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Reports",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "AccountTypes",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDelete",
                table: "Accounts",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSettings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AccountTypes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AccountTypes");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "TransactionTypes",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Transactions",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Reports",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Categories",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "AccountTypes",
                newName: "IsDelete");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Accounts",
                newName: "IsDelete");
        }
    }
}
