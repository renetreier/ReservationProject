using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationProject.Infra.Migrations
{
    public partial class AddedRoomAndWorkerObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservation",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WorkerId",
                table: "Reservation",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Worker_WorkerId",
                table: "Reservation",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Room_RoomId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Worker_WorkerId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RoomId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_WorkerId",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
