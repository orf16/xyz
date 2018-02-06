namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiotipidatoplanaccionIns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_PlanAccionInspeccion", "Fecha_Cierre_Plan", c => c.DateTime());
            AlterColumn("dbo.Tbl_PlanAccionInspeccion", "Fecha_Fin_Plan_Accion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tbl_PlanAccionInspeccion", "Fecha_Fin_Plan_Accion", c => c.String());
            DropColumn("dbo.Tbl_PlanAccionInspeccion", "Fecha_Cierre_Plan");
        }
    }
}
