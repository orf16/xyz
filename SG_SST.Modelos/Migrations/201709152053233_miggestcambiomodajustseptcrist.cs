namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miggestcambiomodajustseptcrist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro");
            CreateIndex("dbo.Tbl_GestionDelCambio", "FK_Id_Rol");
            AddForeignKey("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro", "PK_Clasificacion_De_Peligro", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", "dbo.Tbl_Rol", "Pk_Id_Rol", cascadeDelete: true);
            DropColumn("dbo.Tbl_GestionDelCambio", "AnalisisRiesgo");
            DropColumn("dbo.Tbl_GestionDelCambio", "Comunicado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_GestionDelCambio", "Comunicado", c => c.String());
            AddColumn("dbo.Tbl_GestionDelCambio", "AnalisisRiesgo", c => c.String());
            DropForeignKey("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", "dbo.Tbl_Rol");
            DropForeignKey("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro", "dbo.Tbl_Clasificacion_De_Peligro");
            DropIndex("dbo.Tbl_GestionDelCambio", new[] { "FK_Id_Rol" });
            DropIndex("dbo.Tbl_GestionDelCambio", new[] { "FK_Clasificacion_De_Peligro" });
            DropColumn("dbo.Tbl_GestionDelCambio", "FK_Id_Rol");
            DropColumn("dbo.Tbl_GestionDelCambio", "FK_Clasificacion_De_Peligro");
        }
    }
}
