namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cu_empresa_filtros_algoritmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tbl_ComunicacionesEncuestas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_ComunicacionesExternas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_ComunicacionesInternas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_ComunicadosAdjutos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_ComunicadosAPP", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_AnalisisRiesgo", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_Bd_Externa", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_CaracteristicasInstalacion", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_DescripcionOcupacion", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_Elementos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_EsquemaOrganizacional", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_FrentesAccion", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_Generalidades", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_Georeferenciacion", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_InfoGeneral", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_NivelEmergencia", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_PlanAyuda", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_ProcOpera_Normalizados", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_RecursosHumanos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_RecursosTecnicos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Eme_Roles", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_Plan_Empresa", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_PlanCapacitacion", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_PlanCapacitacion_Asignaciones", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_PlanCapacitacion_Soporte", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Consolidado", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_IdentificacionAmenazas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Personas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_Recursos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_eme_sistemas_procesos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_Identificacion_Personas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_Personas", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_Recursos", "NitEmpresa", c => c.String());
            AddColumn("dbo.Tbl_vul_SistemasProcesos", "NitEmpresa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tbl_vul_SistemasProcesos", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_Recursos", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_Personas", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_Identificacion_Personas", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_eme_sistemas_procesos", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_eme_Recursos", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_eme_Personas", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_eme_IdentificacionAmenazas", "NitEmpresa");
            DropColumn("dbo.Tbl_vul_eme_Consolidado", "NitEmpresa");
            DropColumn("dbo.Tbl_PlanCapacitacion_Soporte", "NitEmpresa");
            DropColumn("dbo.Tbl_PlanCapacitacion_Asignaciones", "NitEmpresa");
            DropColumn("dbo.Tbl_PlanCapacitacion", "NitEmpresa");
            DropColumn("dbo.Tbl_Plan_Empresa", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_Roles", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_RecursosTecnicos", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_RecursosHumanos", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_ProcOpera_Normalizados", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_PlanAyuda", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_NivelEmergencia", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_InfoGeneral", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_Georeferenciacion", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_Generalidades", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_FrentesAccionAdjuntos", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_FrentesAccion", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_EsquemaOrganizacional", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_Elementos", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_DescripcionOcupacion", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_CaracteristicasInstalacion", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_Bd_Externa", "NitEmpresa");
            DropColumn("dbo.Tbl_Eme_AnalisisRiesgo", "NitEmpresa");
            DropColumn("dbo.Tbl_ComunicadosAPP", "NitEmpresa");
            DropColumn("dbo.Tbl_ComunicadosAdjutos", "NitEmpresa");
            DropColumn("dbo.Tbl_ComunicacionesInternas", "NitEmpresa");
            DropColumn("dbo.Tbl_ComunicacionesExternas", "NitEmpresa");
            DropColumn("dbo.Tbl_ComunicacionesEncuestas", "NitEmpresa");
        }
    }
}
