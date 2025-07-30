using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Baiomy.FCI2.DAL.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDepartmentRelationship_Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentID",
                table: "Employees",
                column: "DepartmentID",
                principalSchema: "dbo",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentID",
                table: "Employees"); 

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees");      

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Employees");      
        }
    }
}
