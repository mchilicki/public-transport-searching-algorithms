using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chilicki.Ptsa.Data.Migrations
{
    public partial class RemoveStopTimesFromConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_StopTimes_EndStopTimeId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_StopTimes_StartStopTimeId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_SimilarVertices_Vertices_SimilarId",
                table: "SimilarVertices");

            migrationBuilder.DropIndex(
                name: "IX_Connections_EndStopTimeId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_StartStopTimeId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "EndStopTimeId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "StartStopTimeId",
                table: "Connections");

            migrationBuilder.AddForeignKey(
                name: "FK_SimilarVertices_Vertices_SimilarId",
                table: "SimilarVertices",
                column: "SimilarId",
                principalTable: "Vertices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimilarVertices_Vertices_SimilarId",
                table: "SimilarVertices");

            migrationBuilder.AddColumn<Guid>(
                name: "EndStopTimeId",
                table: "Connections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StartStopTimeId",
                table: "Connections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Connections_EndStopTimeId",
                table: "Connections",
                column: "EndStopTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_StartStopTimeId",
                table: "Connections",
                column: "StartStopTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_StopTimes_EndStopTimeId",
                table: "Connections",
                column: "EndStopTimeId",
                principalTable: "StopTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_StopTimes_StartStopTimeId",
                table: "Connections",
                column: "StartStopTimeId",
                principalTable: "StopTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SimilarVertices_Vertices_SimilarId",
                table: "SimilarVertices",
                column: "SimilarId",
                principalTable: "Vertices",
                principalColumn: "Id");
        }
    }
}
