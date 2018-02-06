namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioProcesoDx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso");
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Proceso" });
            AlterColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", c => c.Int());
            CreateIndex("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso");
            AddForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso");
            DropIndex("dbo.Tbl_Dx_Condiciones_De_Salud", new[] { "FK_Proceso" });
            AlterColumn("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso");
            AddForeignKey("dbo.Tbl_Dx_Condiciones_De_Salud", "FK_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso", cascadeDelete: true);
        }
    }
}
