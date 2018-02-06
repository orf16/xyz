namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiodatoconfiguracion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura");
            DropIndex("dbo.Tbl_ConfiguracionporInspeccion", new[] { "CondicionInsegura_Pk_Id_CondicionInsegura" });
            AddColumn("dbo.Tbl_CondicionInsegura", "ConfiguracionPrioridad", c => c.String());
            DropColumn("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", c => c.Int());
            DropColumn("dbo.Tbl_CondicionInsegura", "ConfiguracionPrioridad");
            CreateIndex("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura");
            AddForeignKey("dbo.Tbl_ConfiguracionporInspeccion", "CondicionInsegura_Pk_Id_CondicionInsegura", "dbo.Tbl_CondicionInsegura", "Pk_Id_CondicionInsegura");
        }
    }
}
