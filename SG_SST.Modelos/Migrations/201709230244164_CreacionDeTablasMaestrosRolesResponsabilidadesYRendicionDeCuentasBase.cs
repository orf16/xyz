namespace SG_SST.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionDeTablasMaestrosRolesResponsabilidadesYRendicionDeCuentasBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tbl_RendicionDeCuentasBase",
                c => new
                    {
                        Pk_Id_RendicionDeCuentasBase = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_RendicionDeCuentasBase);
            
            CreateTable(
                "dbo.Tbl_Roles_Por_RendicionDeCuentasBase",
                c => new
                    {
                        Id_Pk_RolesPorRendicionDeCuentasBase = c.Int(nullable: false, identity: true),
                        Fk_Id_RolesBase = c.Int(nullable: false),
                        Fk_Id_RendicionDeCuentasBase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_RolesPorRendicionDeCuentasBase)
                .ForeignKey("dbo.Tbl_RendicionDeCuentasBase", t => t.Fk_Id_RendicionDeCuentasBase, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_RolesBase", t => t.Fk_Id_RolesBase, cascadeDelete: true)
                .Index(t => t.Fk_Id_RolesBase)
                .Index(t => t.Fk_Id_RendicionDeCuentasBase);
            
            CreateTable(
                "dbo.Tbl_RolesBase",
                c => new
                    {
                        Pk_Id_RolesBase = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_RolesBase);
            
            CreateTable(
                "dbo.Tbl_Roles_Por_ResponsabilidadesBase",
                c => new
                    {
                        Id_Pk_RolesPorResponsabilidadesBase = c.Int(nullable: false, identity: true),
                        Fk_Id_RolesBase = c.Int(nullable: false),
                        Fk_Id_ResponsabilidadesBase = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Pk_RolesPorResponsabilidadesBase)
                .ForeignKey("dbo.Tbl_ResponsabilidadesBase", t => t.Fk_Id_ResponsabilidadesBase, cascadeDelete: true)
                .ForeignKey("dbo.Tbl_RolesBase", t => t.Fk_Id_RolesBase, cascadeDelete: true)
                .Index(t => t.Fk_Id_RolesBase)
                .Index(t => t.Fk_Id_ResponsabilidadesBase);
            
            CreateTable(
                "dbo.Tbl_ResponsabilidadesBase",
                c => new
                    {
                        Pk_Id_ResponsabilidadesBase = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Pk_Id_ResponsabilidadesBase);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tbl_Roles_Por_RendicionDeCuentasBase", "Fk_Id_RolesBase", "dbo.Tbl_RolesBase");
            DropForeignKey("dbo.Tbl_Roles_Por_ResponsabilidadesBase", "Fk_Id_RolesBase", "dbo.Tbl_RolesBase");
            DropForeignKey("dbo.Tbl_Roles_Por_ResponsabilidadesBase", "Fk_Id_ResponsabilidadesBase", "dbo.Tbl_ResponsabilidadesBase");
            DropForeignKey("dbo.Tbl_Roles_Por_RendicionDeCuentasBase", "Fk_Id_RendicionDeCuentasBase", "dbo.Tbl_RendicionDeCuentasBase");
            DropIndex("dbo.Tbl_Roles_Por_ResponsabilidadesBase", new[] { "Fk_Id_ResponsabilidadesBase" });
            DropIndex("dbo.Tbl_Roles_Por_ResponsabilidadesBase", new[] { "Fk_Id_RolesBase" });
            DropIndex("dbo.Tbl_Roles_Por_RendicionDeCuentasBase", new[] { "Fk_Id_RendicionDeCuentasBase" });
            DropIndex("dbo.Tbl_Roles_Por_RendicionDeCuentasBase", new[] { "Fk_Id_RolesBase" });
            DropTable("dbo.Tbl_ResponsabilidadesBase");
            DropTable("dbo.Tbl_Roles_Por_ResponsabilidadesBase");
            DropTable("dbo.Tbl_RolesBase");
            DropTable("dbo.Tbl_Roles_Por_RendicionDeCuentasBase");
            DropTable("dbo.Tbl_RendicionDeCuentasBase");
        }
    }
}
