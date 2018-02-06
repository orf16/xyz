namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creaciontblplaneacioninspecccion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Planeacion_Inspeccion",
                c => new
                    {
                        Pk_Id_PlaneacionInspeccion = c.Int(nullable: false, identity: true),
                        Descripcion_Tipo_Inspeccion = c.String(),
                        Responsable_Tipo_Inspeccion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_PlaneacionInspeccion);
            
            AddColumn("dbo.Tbl_Empleado", "FK_Empresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Empleado", "FK_Empresa");
            AddForeignKey("dbo.Tbl_Empleado", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Empleado", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_Empleado", new[] { "FK_Empresa" });
            DropColumn("dbo.Tbl_Empleado", "FK_Empresa");
            DropTable("dbo.Tbl_Planeacion_Inspeccion");
        }
    }
}
