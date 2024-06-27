using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuzyceApi.Db.AppDb.Migrations
{
    /// <inheritdoc />
    public partial class OrdersForProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentsDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsDefinitions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lampshades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lampshades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrdersForProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerSymbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersForProduction", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItemsForProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderItemLp = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantityInStock = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Unit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductSymbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemsForProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemsForProduction_Lampshades_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentsDefinitionId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    lockedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentsDefinitions_DocumentsDefinitionId",
                        column: x => x.DocumentsDefinitionId,
                        principalTable: "DocumentsDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    QuantityNetto = table.Column<int>(type: "int", nullable: false),
                    QuantityLoss = table.Column<int>(type: "int", nullable: false),
                    QuantityToImprove = table.Column<int>(type: "int", nullable: false),
                    QuantityGross = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LampshadeId = table.Column<int>(type: "int", nullable: false),
                    OrderItemForProductionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Lampshades_LampshadeId",
                        column: x => x.LampshadeId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_OrderItemsForProduction_OrderItemForProduc~",
                        column: x => x.OrderItemForProductionId,
                        principalTable: "OrderItemsForProduction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentDocumentId = table.Column<int>(type: "int", nullable: false),
                    SubordinateDocumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRelations_Documents_ParentDocumentId",
                        column: x => x.ParentDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRelations_Documents_SubordinateDocumentId",
                        column: x => x.SubordinateDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    QuantityNetDelta = table.Column<int>(type: "int", nullable: false),
                    QuantityLossDelta = table.Column<int>(type: "int", nullable: false),
                    QuantityToImproveDelta = table.Column<int>(type: "int", nullable: false),
                    ErrorCodeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Errors_ErrorCodeId",
                        column: x => x.ErrorCodeId,
                        principalTable: "Errors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operations_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentItemRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentDocumentId = table.Column<int>(type: "int", nullable: false),
                    SubordinateDocumentId = table.Column<int>(type: "int", nullable: false),
                    ParentPositionId = table.Column<int>(type: "int", nullable: false),
                    SubordinatePositionId = table.Column<int>(type: "int", nullable: false),
                    NetQuantityParent = table.Column<int>(type: "int", nullable: false),
                    QuantityLossParent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentItemRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentItemRelationships_DocumentPositions_ParentPositionId",
                        column: x => x.ParentPositionId,
                        principalTable: "DocumentPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentItemRelationships_DocumentPositions_SubordinatePosit~",
                        column: x => x.SubordinatePositionId,
                        principalTable: "DocumentPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentItemRelationships_Documents_ParentDocumentId",
                        column: x => x.ParentDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentItemRelationships_Documents_SubordinateDocumentId",
                        column: x => x.SubordinateDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "DocumentsDefinitions",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "KW", "Kwit" },
                    { 2, "ZP", "Zlecenie Produkcji" }
                });

            migrationBuilder.InsertData(
                table: "Lampshades",
                columns: new[] { "Id", "Code" },
                values: new object[] { 1, "KL4124" });

            migrationBuilder.InsertData(
                table: "OrdersForProduction",
                columns: new[] { "Id", "CustomerId", "CustomerName", "CustomerSymbol", "Date", "Number" },
                values: new object[] { 1, 1, "Testowanie", "TEST", new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(1966), "1" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[] { 1, "Otwarty", 1 });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "M", "Magazyn" },
                    { 2, "P", "Produkcja" }
                });

            migrationBuilder.InsertData(
                table: "OrderItemsForProduction",
                columns: new[] { "Id", "Description", "OrderId", "OrderItemId", "OrderItemLp", "OrderNumber", "ProductDescription", "ProductId", "ProductName", "ProductSymbol", "Quantity", "QuantityInStock", "SerialNumber", "Symbol", "Unit" },
                values: new object[] { 1, "Test", 1, 1, 1, "1", "Test", 1, "KL4124", "KL4124", 1m, 1m, "1", "TEST", "szt" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Hash", "LastName", "Login", "Name", "Password", "RoleId" },
                values: new object[] { 1, new DateTime(2024, 6, 27, 9, 34, 5, 119, DateTimeKind.Local).AddTicks(4305), "admin@gmail.com", "admin", "Admin", "admin", "Admin", "$2a$11$nRy8ntehl8n8AEYqmpAO5.qZpZzBAO4nRanlbzVDTOFAE4biDOmyq", 1 });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClosedAt", "CreatedAt", "DocNumber", "DocumentsDefinitionId", "Number", "OperatorId", "StatusId", "UpdatedAt", "WarehouseId", "Year", "lockedBy" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(1829), 1, 1, "0001/KW/2024", 1, 1, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(1884), 1, 2023, null },
                    { 2, null, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(2189), 1, 2, "0001/ZP/2024", 1, 1, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(2195), 2, 2024, null }
                });

            migrationBuilder.InsertData(
                table: "DocumentPositions",
                columns: new[] { "Id", "DocumentId", "EndTime", "LampshadeId", "OperatorId", "OrderItemForProductionId", "QuantityGross", "QuantityLoss", "QuantityNetto", "QuantityToImprove", "StartTime", "StatusId" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 1, null, 0, 0, 0, 0, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(1941), 1 },
                    { 2, 2, null, 1, 1, 1, 0, 0, 0, 0, new DateTime(2024, 6, 27, 9, 34, 5, 229, DateTimeKind.Local).AddTicks(2221), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_ParentDocumentId",
                table: "DocumentItemRelationships",
                column: "ParentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_ParentPositionId",
                table: "DocumentItemRelationships",
                column: "ParentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_SubordinateDocumentId",
                table: "DocumentItemRelationships",
                column: "SubordinateDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_SubordinatePositionId",
                table: "DocumentItemRelationships",
                column: "SubordinatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_DocumentId",
                table: "DocumentPositions",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_LampshadeId",
                table: "DocumentPositions",
                column: "LampshadeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_OperatorId",
                table: "DocumentPositions",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_OrderItemForProductionId",
                table: "DocumentPositions",
                column: "OrderItemForProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_StatusId",
                table: "DocumentPositions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelations_ParentDocumentId",
                table: "DocumentRelations",
                column: "ParentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelations_SubordinateDocumentId",
                table: "DocumentRelations",
                column: "SubordinateDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentsDefinitionId",
                table: "Documents",
                column: "DocumentsDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OperatorId",
                table: "Documents",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StatusId",
                table: "Documents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_WarehouseId",
                table: "Documents",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_DocumentId",
                table: "Operations",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ErrorCodeId",
                table: "Operations",
                column: "ErrorCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperatorId",
                table: "Operations",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemsForProduction_ProductId",
                table: "OrderItemsForProduction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentItemRelationships");

            migrationBuilder.DropTable(
                name: "DocumentRelations");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "OrdersForProduction");

            migrationBuilder.DropTable(
                name: "DocumentPositions");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "OrderItemsForProduction");

            migrationBuilder.DropTable(
                name: "DocumentsDefinitions");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Lampshades");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
