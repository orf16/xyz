namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatablasconsultasst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_ConsultaSST",
                c => new
                    {
                        Pk_Id_Consulta = c.Int(nullable: false, identity: true),
                        Fk_Empresa = c.Int(nullable: false),
                        Tipo_Consulta = c.String(maxLength: 200),
                        Descripcion_Consulta = c.String(),
                        Id_Usuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pk_Id_Consulta);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tbl_ConsultaSST");
        }
    }
}
