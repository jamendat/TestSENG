using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSENG.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    IDAccount = table.Column<int>(name: "ID_Account", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IBAN = table.Column<string>(type: "TEXT", maxLength: 18, nullable: false),
                    Balance = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IDCustomer = table.Column<int>(name: "ID_Customer", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.IDAccount);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    IDCustomer = table.Column<int>(name: "ID_Customer", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IDCardNo = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.IDCustomer);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    IDDeposit = table.Column<int>(name: "ID_Deposit", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IDAccount = table.Column<int>(name: "ID_Account", type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.IDDeposit);
                });

            migrationBuilder.CreateTable(
                name: "Log_Account",
                columns: table => new
                {
                    IDLogAccount = table.Column<int>(name: "ID_Log_Account", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IDAccount = table.Column<int>(name: "ID_Account", type: "INTEGER", nullable: false),
                    IDAccountReceive = table.Column<string>(name: "ID_Account_Receive", type: "TEXT", maxLength: 18, nullable: false),
                    Amount = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Account", x => x.IDLogAccount);
                });

            migrationBuilder.CreateTable(
                name: "TransferMoney",
                columns: table => new
                {
                    IDTransferMoney = table.Column<int>(name: "ID_TransferMoney", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IDAccount = table.Column<int>(name: "ID_Account", type: "INTEGER", nullable: false),
                    IDAccountReceive = table.Column<string>(name: "ID_Account_Receive", type: "TEXT", maxLength: 18, nullable: false),
                    Amount = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferMoney", x => x.IDTransferMoney);
                });

            migrationBuilder.CreateTable(
                name: "Withdraw",
                columns: table => new
                {
                    IDWithdraw = table.Column<int>(name: "ID_Withdraw", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IDAccount = table.Column<int>(name: "ID_Account", type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    Balance = table.Column<int>(type: "decimal(18, 2)", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraw", x => x.IDWithdraw);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Log_Account");

            migrationBuilder.DropTable(
                name: "TransferMoney");

            migrationBuilder.DropTable(
                name: "Withdraw");
        }
    }
}
