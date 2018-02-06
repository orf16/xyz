namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_tbl_plan_empresa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_Plan_Empresa",
                c => new
                    {
                        pk_id_plan_empresa = c.Int(nullable: false, identity: true),
                        IdSede = c.Int(nullable: false),
                        FechaDesde = c.String(),
                        FechaHasta = c.String(),
                        Vigencia = c.String(),
                        ObjetivosDescripcion = c.String(),
                        ObjetivosMetas = c.String(),
                        Actividad = c.String(),
                        Responsable = c.String(),
                        RecursosHumanos = c.String(),
                        RecursosTecnologico = c.String(),
                        RecursosFinanciero = c.String(),
                        FechaProg = c.String(),
                        HoraProgIni = c.String(),
                        HoraProgFin = c.String(),
                        Estado = c.String(),
                        PorcentajeEjecucion = c.String(),
                        RepresentanteLegal = c.String(),
                        RepresentanteSGSST = c.String(),
                    })
                .PrimaryKey(t => t.pk_id_plan_empresa);
            
            AddColumn("dbo.Tbl_CondicionInsegura", "PKConfiguracionPrioridadInspeccion", c => c.Int(nullable: false));
            DropColumn("dbo.Tbl_CondicionInsegura", "ConfiguracionPrioridad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tbl_CondicionInsegura", "ConfiguracionPrioridad", c => c.String());
            DropColumn("dbo.Tbl_CondicionInsegura", "PKConfiguracionPrioridadInspeccion");
            DropTable("dbo.Tbl_Plan_Empresa");
        }
    }
}
