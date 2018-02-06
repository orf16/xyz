
using SG_SST.EntidadesDominio.ComunicadosAPP;
using SG_SST.Interfaces.ComunicadosAPP;
using SG_SST.Models;
using SG_SST.Models.ComunicadosAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.ComunicadosAPP
{
    public class ComunicadosAppManager : IUsuariosComunicadoAPP
    {
        private SG_SSTContext context;
        public ComunicadosAppManager()
        {
            context = new SG_SSTContext();
        }

        public List<EDUsuarioComunicadosAPP> ListarComunicadosPorUsuario(string IdentificacionUsuario)
        {
            return context.Tbl_UsuarioComunicadoAPP
                .Join(context.Tbl_EstadosComunicadosAPP,
                a => a.IDEstadoComunicado,
                b => b.PK_Id_EstadoComunicado,
                (a, b) => new { a, b }
                )
                .Join(context.Tbl_ComunicadosAPP,
                c => c.a.FK_Id_ComunicadosAPP,
                d => d.IDComunicadosAPP,
                (c, d) => new
                {
                    PK_Id_Mensaje = c.a.PK_Id_Mensaje,
                    FK_Id_ComunicadosAPP = c.a.FK_Id_ComunicadosAPP,
                    IdentificacionUsuario = c.a.IdentificacionUsuario,
                    EstadoComunicado = c.b.Nombre,
                    PlayerID = c.a.PlayerID,
                    Titulo = d.Titulo,
                    Asunto = d.AsuntoAPP,
                    Destinatario = d.Destinatarios,
                    FechaCreacion = d.FechaCreacion,
                    FechaEnvio = d.FechaEnvio
                }
                ).Select(x => new EDUsuarioComunicadosAPP()
                {
                    PK_Id_Mensaje = x.PK_Id_Mensaje,
                    FK_Id_ComunicadosAPP = x.FK_Id_ComunicadosAPP,
                    IdentificacionUsuario = x.IdentificacionUsuario,
                    EstadoComunicado = x.EstadoComunicado,
                    PlayerID = x.PlayerID,
                    Titulo = x.Titulo,
                    Asunto = x.Asunto,
                    Destinatario = x.Destinatario,
                    FechaCreacion = x.FechaCreacion,
                    FechaEnvio = x.FechaEnvio
                }).ToList();
        }

        public List<EDUsuarioComunicadosAPP> ListarComunicadosUsuarioPorEstado(string IdentificacionUsuario, int Estado)
        {
            return context.Tbl_UsuarioComunicadoAPP.Where(x => (x.IdentificacionUsuario == IdentificacionUsuario && x.IDEstadoComunicado == Estado))
                .Join(context.Tbl_EstadosComunicadosAPP,
                a => a.IDEstadoComunicado,
                b => b.PK_Id_EstadoComunicado,
                (a, b) => new { a, b }
                )
                .Join(context.Tbl_ComunicadosAPP,
                c => c.a.FK_Id_ComunicadosAPP,
                d => d.IDComunicadosAPP,
                (c, d) => new
                {
                    PK_Id_Mensaje = c.a.PK_Id_Mensaje,
                    FK_Id_ComunicadosAPP = c.a.FK_Id_ComunicadosAPP,
                    IdentificacionUsuario = c.a.IdentificacionUsuario,
                    EstadoComunicado = c.b.Nombre,
                    PlayerID = c.a.PlayerID,
                    Titulo = d.Titulo,
                    Asunto = d.AsuntoAPP,
                    Destinatario = d.Destinatarios,
                    FechaCreacion = d.FechaCreacion,
                    FechaEnvio = d.FechaEnvio
                }
                ).Select(x => new EDUsuarioComunicadosAPP()
                {
                    PK_Id_Mensaje = x.PK_Id_Mensaje,
                    FK_Id_ComunicadosAPP = x.FK_Id_ComunicadosAPP,
                    IdentificacionUsuario = x.IdentificacionUsuario,
                    EstadoComunicado = x.EstadoComunicado,
                    PlayerID = x.PlayerID,
                    Titulo = x.Titulo,
                    Asunto = x.Asunto,
                    Destinatario = x.Destinatario,
                    FechaCreacion = x.FechaCreacion,
                    FechaEnvio = x.FechaEnvio
                }).ToList();
        }

        /// <summary>
        /// ACTUALIZAR EL ESTADO DEL COMUNICADO
        /// </summary>
        /// <param name="PK_Id_Mensaje"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public bool UpdateUsuarioComunicadoAPP(int PK_Id_Mensaje, int Estado)
        {
            using (var Transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var usuariocomunicadoapp = context.Tbl_UsuarioComunicadoAPP.Where(x => x.PK_Id_Mensaje == PK_Id_Mensaje).SingleOrDefault();
                    usuariocomunicadoapp.IDEstadoComunicado = Estado;
                    context.Tbl_UsuarioComunicadoAPP.Attach(usuariocomunicadoapp);
                    var entry = context.Entry(usuariocomunicadoapp);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.PlayerID).IsModified = true;
                    context.SaveChanges();
                    Transaction.Commit();
                    return true;
                }
                catch
                {
                    Transaction.Rollback();
                    return false;
                }
            }

        }

        public bool UpdateUsuarioPlayerIDComunicadoAPP(string IdentificacionUsuario, string PlayerID)
        {
            using (var Transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var usuariosuscritoapp = context.Tbl_ComunicacionesUsuariosSuscritosAPP.Where(x => x.IdentificacionUsuario == IdentificacionUsuario).SingleOrDefault();
                    if (usuariosuscritoapp != null)
                    {
                        usuariosuscritoapp.PlayerID = PlayerID;
                        context.Tbl_ComunicacionesUsuariosSuscritosAPP.Attach(usuariosuscritoapp);
                        var entry = context.Entry(usuariosuscritoapp);
                        entry.State = System.Data.Entity.EntityState.Modified;
                        entry.Property(x => x.PlayerID).IsModified = true;
                        context.SaveChanges();
                    }
                    else
                    {
                        ComunicacionesUsuariosSuscritosAPP suscritoapp = new ComunicacionesUsuariosSuscritosAPP() { 
                            IdentificacionUsuario = IdentificacionUsuario,
                            PlayerID = PlayerID
                        };
                        context.Tbl_ComunicacionesUsuariosSuscritosAPP.Add(suscritoapp);
                        context.SaveChanges();
                    }
                    
                    Transaction.Commit();
                    return true;
                }
                catch
                {
                    Transaction.Rollback();
                    return false;
                }
            }

        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose();
        }
    }
}
