namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkidrolgestioncambioajustemodelo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_GestionDelCambio", "FK_Id_Rol");
            AddForeignKey("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", "dbo.Tbl_Rol", "Pk_Id_Rol", cascadeDelete: true);
            DropColumn("dbo.Tbl_GestionDelCambio", "Comunicado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_GestionDelCambio", "Comunicado", c => c.String());
            DropForeignKey("dbo.Tbl_GestionDelCambio", "FK_Id_Rol", "dbo.Tbl_Rol");
            DropIndex("dbo.Tbl_GestionDelCambio", new[] { "FK_Id_Rol" });
            DropColumn("dbo.Tbl_GestionDelCambio", "FK_Id_Rol");
        }
    }
}
