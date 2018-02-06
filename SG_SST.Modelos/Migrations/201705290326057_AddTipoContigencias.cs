namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTipoContigencias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Tipo_Contigencias",
                c => new
                    {
                        PK_Id_Tipo_Contigencia = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PK_Id_Tipo_Contigencia);
            
            AddColumn("dbo.Tbl_Contingencias", "FK_Tipo_Contingencia", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Contingencias", "FK_Tipo_Contingencia");
            AddForeignKey("dbo.Tbl_Contingencias", "FK_Tipo_Contingencia", "dbo.Tbl_Tipo_Contigencias", "PK_Id_Tipo_Contigencia", cascadeDelete: true);
            DropColumn("dbo.Tbl_Contingencias", "Tipo_Contingencia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Contingencias", "Tipo_Contingencia", c => c.String());
            DropForeignKey("dbo.Tbl_Contingencias", "FK_Tipo_Contingencia", "dbo.Tbl_Tipo_Contigencias");
            DropIndex("dbo.Tbl_Contingencias", new[] { "FK_Tipo_Contingencia" });
            DropColumn("dbo.Tbl_Contingencias", "FK_Tipo_Contingencia");
            DropTable("dbo.Tbl_Tipo_Contigencias");
        }
    }
}
