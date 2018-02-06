namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvalEtandMiniandEmpleTercero : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Evaluacion_Estandares_Minimos", "Valor_Calificacion", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_Criterios", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Tbl_EmpleadoTercero", "FK_Empresa", c => c.Int(nullable: false));
            CreateIndex("dbo.Tbl_EmpleadoTercero", "FK_Empresa");
            AddForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Empresa", "dbo.Tbl_Empresa", "Pk_Id_Empresa", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_EmpleadoTercero", "FK_Empresa", "dbo.Tbl_Empresa");
            DropIndex("dbo.Tbl_EmpleadoTercero", new[] { "FK_Empresa" });
            DropColumn("dbo.Tbl_EmpleadoTercero", "FK_Empresa");
            DropColumn("dbo.Tbl_Criterios", "Valor");
            DropColumn("dbo.Tbl_Evaluacion_Estandares_Minimos", "Valor_Calificacion");
        }
    }
}
