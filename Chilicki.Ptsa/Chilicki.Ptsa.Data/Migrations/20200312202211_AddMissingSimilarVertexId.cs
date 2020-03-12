using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chilicki.Ptsa.Data.Migrations
{
    public partial class AddMissingSimilarVertexId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SimilarVertexId",
                table: "SimilarVertices",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimilarVertexId",
                table: "SimilarVertices");
        }
    }
}
