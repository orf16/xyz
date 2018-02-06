namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTblEmpleadoPorTematica : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica", "dbo.Tbl_Tematica");
            DropIndex("dbo.Tbl_Empleado_Por_Tematica", new[] { "Fk_Id_Empleadotematica" });
            AddColumn("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Rol", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Rol");
            AddForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Rol", "dbo.Tbl_Rol", "Pk_Id_Rol", cascadeDelete: true);
            DropColumn("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Rol", "dbo.Tbl_Rol");
            DropIndex("dbo.Tbl_Empleado_Por_Tematica", new[] { "Fk_Id_Rol" });
            DropColumn("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Rol");
            CreateIndex("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica");
            AddForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica", "dbo.Tbl_Tematica", "Id_Tematica", cascadeDelete: true);
        }
    }
}
