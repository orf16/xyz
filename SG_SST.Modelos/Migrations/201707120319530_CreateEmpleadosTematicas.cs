namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEmpleadosTematicas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Empleado_Por_Tematica",
                c => new
                    {
                        Pk_Id_EmpleadoPorTematica = c.Int(nullable: false, identity: true),
                        Fk_Id_Tematica = c.Int(nullable: false),
                        Fk_Id_Empleadotematica = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_EmpleadoPorTematica)
                .ForeignKey("dbo.Tbl_Empleado_Tematica", t => t.Fk_Id_Tematica, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_Tematica", t => t.Fk_Id_Empleadotematica, cascadeDelete: true)
                .Index(t => t.Fk_Id_Tematica)
                .Index(t => t.Fk_Id_Empleadotematica);
            
            CreateTable(
                "dbo.Tbl_Empleado_Tematica",
                c => new
                    {
                        Pk_Id_EmpleadoTematica = c.Int(nullable: false, identity: true),
                        Numero_Documento = c.Int(nullable: false),
                        Nombre_Empleado = c.String(),
                        Apellidos_Empleado = c.String(),
                        Cargo_Empleado = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_EmpleadoTematica);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Empleadotematica", "dbo.Tbl_Tematica");
            DropForeignKey("dbo.Tbl_Empleado_Por_Tematica", "Fk_Id_Tematica", "dbo.Tbl_Empleado_Tematica");
            DropIndex("dbo.Tbl_Empleado_Por_Tematica", new[] { "Fk_Id_Empleadotematica" });
            DropIndex("dbo.Tbl_Empleado_Por_Tematica", new[] { "Fk_Id_Tematica" });
            DropTable("dbo.Tbl_Empleado_Tematica");
            DropTable("dbo.Tbl_Empleado_Por_Tematica");
        }
    }
}
