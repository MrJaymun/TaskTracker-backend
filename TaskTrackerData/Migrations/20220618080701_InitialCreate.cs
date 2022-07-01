using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerData.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Creator = table.Column<string>(type: "text", nullable: false),
                    IsPersonal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsGuid, x.UsersGuid });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsGuid",
                        column: x => x.ProjectsGuid,
                        principalTable: "Projects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_UsersGuid",
                        column: x => x.UsersGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ProjectGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PerformerGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectGuid",
                        column: x => x.ProjectGuid,
                        principalTable: "Projects",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_PerformerGuid",
                        column: x => x.PerformerGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    AuthorGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Comments_Tasks_TaskGuid",
                        column: x => x.TaskGuid,
                        principalTable: "Tasks",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_AuthorGuid",
                        column: x => x.AuthorGuid,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorGuid",
                table: "Comments",
                column: "AuthorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TaskGuid",
                table: "Comments",
                column: "TaskGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersGuid",
                table: "ProjectUser",
                column: "UsersGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PerformerGuid",
                table: "Tasks",
                column: "PerformerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectGuid",
                table: "Tasks",
                column: "ProjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
