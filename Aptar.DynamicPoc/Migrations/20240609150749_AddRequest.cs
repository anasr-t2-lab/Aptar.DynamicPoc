using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aptar.DynamicPoc.Migrations
{
    /// <inheritdoc />
    public partial class AddRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorValidations");

            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FormSchema = table.Column<JsonDocument>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestTypeId = table.Column<int>(type: "integer", nullable: false),
                    Body = table.Column<JObject>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "FormSchema", "Name" },
                values: new object[] { 1, System.Text.Json.JsonDocument.Parse("[\r\n  {\r\n    \"key\": \"subject\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Subject\",\r\n      \"required\": true,\r\n      \"maxLength\": 50\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"description\",\r\n    \"type\": \"textarea\",\r\n    \"props\": {\r\n      \"label\": \"Description\",\r\n      \"required\": true,\r\n      \"maxLength\": 500\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"samplesCount\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Samples Count\",\r\n      \"type\": \"number\",\r\n      \"required\": true,\r\n      \"min\": 1,\r\n      \"max\": 10\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"material\",\r\n    \"type\": \"select\",\r\n    \"props\": {\r\n      \"label\": \"Material\",\r\n      \"required\": true,\r\n      \"options\": [\r\n        { \"label\": \"Plastic\", \"value\": 1 },\r\n        { \"label\": \"Aluminum\", \"value\": 2 },\r\n        { \"label\": \"Glass\", \"value\": 3 }\r\n      ]\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"email\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Email\",\r\n      \"required\": true,\r\n      \"type\": \"email\"\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"phoneNumber\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Phone Number\",\r\n      \"required\": false,\r\n      \"pattern\": \"^[0-9]*$\"\r\n    }\r\n  },\r\n          {\r\n            \"key\": \"requestDate\",\r\n            \"type\": \"date\",\r\n            \"props\": {\r\n              \"label\": \"Request Date\",\r\n              \"required\": true,\r\n              \"minDate\": \"2024-01-01\",\r\n              \"maxDate\": \"2024-12-31\"\r\n            }\r\n    }\r\n]", new System.Text.Json.JsonDocumentOptions()), "Hamada" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestTypeId",
                table: "Requests",
                column: "RequestTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.CreateTable(
                name: "VendorValidations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormSchema = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorValidations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VendorValidations",
                columns: new[] { "Id", "FormSchema", "Name" },
                values: new object[] { 1, System.Text.Json.JsonDocument.Parse("[\r\n  {\r\n    \"key\": \"subject\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Subject\",\r\n      \"required\": true,\r\n      \"maxLength\": 50\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"description\",\r\n    \"type\": \"textarea\",\r\n    \"props\": {\r\n      \"label\": \"Description\",\r\n      \"required\": true,\r\n      \"maxLength\": 500\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"samplesCount\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Samples Count\",\r\n      \"type\": \"number\",\r\n      \"required\": true,\r\n      \"min\": 1,\r\n      \"max\": 10\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"material\",\r\n    \"type\": \"select\",\r\n    \"props\": {\r\n      \"label\": \"Material\",\r\n      \"required\": true,\r\n      \"options\": [\r\n        { \"label\": \"Plastic\", \"value\": 1 },\r\n        { \"label\": \"Aluminum\", \"value\": 2 },\r\n        { \"label\": \"Glass\", \"value\": 3 }\r\n      ]\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"email\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Email\",\r\n      \"required\": true,\r\n      \"type\": \"email\"\r\n    }\r\n  },\r\n  {\r\n    \"key\": \"phoneNumber\",\r\n    \"type\": \"input\",\r\n    \"props\": {\r\n      \"label\": \"Phone Number\",\r\n      \"required\": false,\r\n      \"pattern\": \"^[0-9]*$\"\r\n    }\r\n  },\r\n          {\r\n            \"key\": \"requestDate\",\r\n            \"type\": \"date\",\r\n            \"props\": {\r\n              \"label\": \"Request Date\",\r\n              \"required\": true,\r\n              \"minDate\": \"2024-01-01\",\r\n              \"maxDate\": \"2024-12-31\"\r\n            }\r\n    }\r\n]", new System.Text.Json.JsonDocumentOptions()), "Hamada" });
        }
    }
}
