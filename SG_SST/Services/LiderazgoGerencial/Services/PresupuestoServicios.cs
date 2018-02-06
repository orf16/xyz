
namespace SG_SST.Services.LiderazgoGerencial.Services
{
    using SG_SST.Dtos.LiderazgoGerencial;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Repositories.LiderazgoGerencial.IRepositories;
    using SG_SST.Repositories.LiderazgoGerencial.Repositories;
    using SG_SST.Services.LiderazgoGerencial.Iservices;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class PresupuestoServicios : IPresupuestoServicios
    {
        IPresupuestoRepositorio presupuestoRepositorio;
        public PresupuestoServicios()
        {
            presupuestoRepositorio = new PresupuestoRepositorio();
        }

        public bool GuardarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto)
        {
            return presupuestoRepositorio.GuardarPresupuesto(actividadesPresupuesto);
        }

        public bool EliminarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto, int PK_Presupuesto)
        {
            return presupuestoRepositorio.EliminarPresupuesto(actividadesPresupuesto, PK_Presupuesto);
        }

        public List<PresupuestoPorAnio> ObtenerPresupuestosSedePorAnio(int pk_Sede, int periodo)
        {
            return presupuestoRepositorio.ObtenerPresupuestosSedePorAnio(pk_Sede, periodo);
        }

        public List<ActividadPresupuesto> ObtenerActividadesPorPresupuesto(int PK_PresupuestoPorAnio)
        {
            return presupuestoRepositorio.ObtenerActividadesPorPresupuesto(PK_PresupuestoPorAnio);
        }


        public PresupuestoPorAnio ObtenerPresupuestoPorAnio(int PK_PresupuestoPorAnio)
        {
            return presupuestoRepositorio.ObtenerPresupuestoPorAnio(PK_PresupuestoPorAnio);
        }

        public List<ActividadPresupuesto> CrearInformePresupuesto(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo)
        {
            List<ActividadPresupuesto> actividadesPresupuestoAnio = presupuestoRepositorio.ObtenerActividadesPorPresupuesto(IDPresupuestoAnio);


            foreach (ActividadPresupuesto ap in actividadesPresupuestoAnio)
            {

                if (ap.actividadesPresupuesto != null)
                {
                    foreach (ActividadPresupuesto aphijas in ap.actividadesPresupuesto)
                    {
                        List<PresupuestoPorMes> ppms = new List<PresupuestoPorMes>();
                        double totalPresupuesto = 0;
                        double totalEjecutado = 0;
                        bool crearPresupuestoMes = false;
                        foreach (PresupuestoPorMes ppm in aphijas.presupuestosPorMes)
                        {
                            if (fecha == 1) // Mesual
                            {
                                if (ppm.Mes == intervaloDeTiempo)
                                {
                                    ppms.Add(ppm);
                                }
                            }
                            if (fecha == 2) // trimestral
                            {
                                if (intervaloDeTiempo == 1)
                                {
                                    if (ppm.Mes == 1 || ppm.Mes == 2 || ppm.Mes == 3)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                if (intervaloDeTiempo == 2)
                                {
                                    if (ppm.Mes == 4 || ppm.Mes == 5 || ppm.Mes == 6)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                if (intervaloDeTiempo == 3)
                                {
                                    if (ppm.Mes == 7 || ppm.Mes == 8 || ppm.Mes == 9)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                if (intervaloDeTiempo == 4)
                                {
                                    if (ppm.Mes == 10 || ppm.Mes == 11 || ppm.Mes == 12)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                crearPresupuestoMes = true;
                            }
                            if (fecha == 3) // Semestral
                            {
                                if (intervaloDeTiempo == 1)
                                {
                                    if (ppm.Mes == 1 || ppm.Mes == 2 || ppm.Mes == 3 || ppm.Mes == 4 || ppm.Mes == 5 || ppm.Mes == 6)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                if (intervaloDeTiempo == 2)
                                {
                                    if (ppm.Mes == 7 || ppm.Mes == 8 || ppm.Mes == 9 || ppm.Mes == 10 || ppm.Mes == 11 || ppm.Mes == 12)
                                    {
                                        totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                        totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                    }
                                }
                                crearPresupuestoMes = true;
                            }
                            if (fecha == 4) // Mesual
                            {
                                totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                crearPresupuestoMes = true;
                            }
                        }
                        if (crearPresupuestoMes)
                        {
                            PresupuestoPorMes ppm = new PresupuestoPorMes();
                            ppm.PresupuestoMes = totalPresupuesto;
                            ppm.PresupuestoEjecutadoPorMes = totalEjecutado;
                            ppms.Add(ppm);
                        }
                        aphijas.presupuestosPorMes = ppms;
                    }
                }
                else
                {
                    List<PresupuestoPorMes> ppms = new List<PresupuestoPorMes>();
                    double totalPresupuesto = 0;
                    double totalEjecutado = 0;
                    bool crearPresupuestoMes = false;
                    foreach (PresupuestoPorMes ppm in ap.presupuestosPorMes)
                    {
                        if (fecha == 1) // Mesual
                        {
                            if (ppm.Mes == intervaloDeTiempo)
                            {
                                ppms.Add(ppm);
                            }
                        }
                        if (fecha == 2) // trimestral
                        {
                            if (intervaloDeTiempo == 1)
                            {
                                if (ppm.Mes == 1 || ppm.Mes == 2 || ppm.Mes == 3)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            if (intervaloDeTiempo == 2)
                            {
                                if (ppm.Mes == 4 || ppm.Mes == 5 || ppm.Mes == 6)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            if (intervaloDeTiempo == 3)
                            {
                                if (ppm.Mes == 7 || ppm.Mes == 8 || ppm.Mes == 9)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            if (intervaloDeTiempo == 4)
                            {
                                if (ppm.Mes == 10 || ppm.Mes == 11 || ppm.Mes == 12)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            crearPresupuestoMes = true;
                        }
                        if (fecha == 3) // Semestral
                        {
                            if (intervaloDeTiempo == 1)
                            {
                                if (ppm.Mes == 1 || ppm.Mes == 2 || ppm.Mes == 3 || ppm.Mes == 4 || ppm.Mes == 5 || ppm.Mes == 6)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            if (intervaloDeTiempo == 2)
                            {
                                if (ppm.Mes == 7 || ppm.Mes == 8 || ppm.Mes == 9 || ppm.Mes == 10 || ppm.Mes == 11 || ppm.Mes == 12)
                                {
                                    totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                                    totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                                }
                            }
                            crearPresupuestoMes = true;
                        }
                        if (fecha == 4) // Mesual
                        {
                            totalPresupuesto = totalPresupuesto + ppm.PresupuestoMes;
                            totalEjecutado = totalEjecutado + ppm.PresupuestoEjecutadoPorMes;
                            crearPresupuestoMes = true;
                        }
                    }
                    if (crearPresupuestoMes)
                    {
                        PresupuestoPorMes ppm = new PresupuestoPorMes();
                        ppm.PresupuestoMes = totalPresupuesto;
                        ppm.PresupuestoEjecutadoPorMes = totalEjecutado;
                        ppms.Add(ppm);
                    }
                    ap.presupuestosPorMes = ppms;
                }
            }

            return actividadesPresupuestoAnio;

        }

        public List<InformePresupuestoDTO> GenerarExcel(int IDPresupuestoAnio, int fecha, int intervaloDeTiempo, string nombreIntervaloTiempo)
        {

            List<ActividadPresupuesto> aps = CrearInformePresupuesto(IDPresupuestoAnio, fecha, intervaloDeTiempo);
            List<InformePresupuestoDTO> informesDtos = new List<InformePresupuestoDTO>();
            foreach (ActividadPresupuesto ap in aps)
            {
                
                if (ap.actividadesPresupuesto != null)
                {
                    foreach (ActividadPresupuesto apsecun in ap.actividadesPresupuesto) { 
                        InformePresupuestoDTO ipdto = new InformePresupuestoDTO(
                            apsecun.DescripcionActividad,
                            apsecun.presupuestosPorMes.FirstOrDefault().PresupuestoMes,
                            apsecun.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes,
                            apsecun.presupuestosPorMes.FirstOrDefault().PresupuestoMes - apsecun.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes,
                            nombreIntervaloTiempo
                           );
                        informesDtos.Add(ipdto);
                    }

                }
                else
                {
                    InformePresupuestoDTO ipdto = new InformePresupuestoDTO(
                       ap.DescripcionActividad,
                       ap.presupuestosPorMes.FirstOrDefault().PresupuestoMes,
                       ap.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes,
                       ap.presupuestosPorMes.FirstOrDefault().PresupuestoMes - ap.presupuestosPorMes.FirstOrDefault().PresupuestoEjecutadoPorMes,
                       nombreIntervaloTiempo
                      );
                    informesDtos.Add(ipdto);
                }
            }

            return informesDtos;
        }

        public bool EliminarActividad(int pkActividad)
        {
            return presupuestoRepositorio.EliminarActividad(pkActividad);
        }
    }
}