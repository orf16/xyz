namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_algoritmo_campoempresa_nit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_Plan_Empresa_Actividad", "NitEmpresa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_Plan_Empresa_Actividad", "NitEmpresa");
        }
    }
}
