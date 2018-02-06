namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioforaneadeinspeccionsedeproceso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_Inspecciones", new[] { "Fk_Id_Empresa" });
            AddColumn("dbo.Tbl_Inspecciones", "Fk_Id_Sede", c => c.Int(nullable: false));
            AddColumn("dbo.Tbl_Inspecciones", "Fk_Id_Proceso", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_Inspecciones", "Fk_Id_Sede");
            CreateIndex("dbo.Tbl_Inspecciones", "Fk_Id_Proceso");
            AddForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Proceso", "dbo.Tbl_Proceso", "Pk_Id_Proceso", cascadeDelete: true);
            AddForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Sede", "dbo.Tbl_Sede", "Pk_Id_Sede", cascadeDelete: true);
            DropColumn("dbo.Tbl_Inspecciones", "Fk_Id_Empresa");
            DropColumn("dbo.Tbl_Inspecciones", "Sede");
            DropColumn("dbo.Tbl_Inspecciones", "Proceso");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_Inspecciones", "Proceso", c => c.String());
            AddColumn("dbo.Tbl_Inspecciones", "Sede", c => c.String());
            AddColumn("dbo.Tbl_Inspecciones", "Fk_Id_Empresa", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Sede", "dbo.Tbl_Sede");
            DropForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Proceso", "dbo.Tbl_Proceso");
            DropIndex("dbo.Tbl_Inspecciones", new[] { "Fk_Id_Proceso" });
            DropIndex("dbo.Tbl_Inspecciones", new[] { "Fk_Id_Sede" });
            DropColumn("dbo.Tbl_Inspecciones", "Fk_Id_Proceso");
            DropColumn("dbo.Tbl_Inspecciones", "Fk_Id_Sede");
            CreateIndex("dbo.Tbl_Inspecciones", "Fk_Id_Empresa");
            AddForeignKey("dbo.Tbl_Inspecciones", "Fk_Id_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
    }
}
