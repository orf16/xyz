using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Aplicacion
{
    public class BateriaManager : IBateria
    {
        //Cuestionarios
        public List<EDBateriaCuestionario> ConsultarFormulario(int Pagina, int IdBateria)
        {
            List<EDBateriaCuestionario> ListaFormulario = new List<EDBateriaCuestionario>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaCuestionario
                                join d in db.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
                                join e in db.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
                                where e.Pk_Id_Bateria == IdBateria
                                select s).ToList<BateriaCuestionario>().Distinct();
                if (Listavar != null)
                {
                    Listavar = Listavar.OrderBy(s => s.Orden).ToList();
                    foreach (var item in Listavar)
                    {
                        EDBateriaCuestionario EDBateriaCuestionario = new EDBateriaCuestionario();
                        EDBateriaCuestionario.Pk_Id_BateriaCuestionario = item.Pk_Id_BateriaCuestionario;
                        EDBateriaCuestionario.Dominio = item.Dominio;
                        EDBateriaCuestionario.Orden = item.Orden;
                        EDBateriaCuestionario.Pagina = item.Pagina;
                        EDBateriaCuestionario.Pregunta = item.Pregunta;
                        ListaFormulario.Add(EDBateriaCuestionario);
                    }
                }
            }

            if (Pagina!=0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    foreach (var item in ListaFormulario)
                    {
                        int pk_form = item.Pk_Id_BateriaCuestionario;
                        int user = Pagina;
                        var Listavar = (from s in db.Tbl_BateriaResultado
                                        where s.Fk_Id_BateriaCuestionario == pk_form && s.Fk_Id_BateriaUsuario == user
                                        select s).FirstOrDefault<BateriaResultado>();
                        if (Listavar!=null)
                        {
                            item.Valor = Listavar.Valor;
                        }
                    }
                }
            }
            


            return ListaFormulario;
        }
        public int PaginaIntralaboralA(string formdata , EDBateriaUsuario EDBateriaUsuario)
        {
            int PkUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
            int Pagina = 0;
            List<EDBateriaCuestionario> ListaFormulario = new List<EDBateriaCuestionario>();
            List<EDBateriaResultado> ListaResultado = new List<EDBateriaResultado>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaCuestionario
                                join d in db.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
                                join e in db.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
                                where e.Pk_Id_Bateria == 1
                                select s).ToList<BateriaCuestionario>().Distinct();
                if (Listavar != null)
                {
                    Listavar = Listavar.OrderBy(s => s.Orden).ToList();
                    foreach (var item in Listavar)
                    {
                        EDBateriaCuestionario EDBateriaCuestionario = new EDBateriaCuestionario();
                        EDBateriaCuestionario.Pk_Id_BateriaCuestionario = item.Pk_Id_BateriaCuestionario;
                        EDBateriaCuestionario.Dominio = item.Dominio;
                        EDBateriaCuestionario.Orden = item.Orden;
                        EDBateriaCuestionario.Pagina = item.Pagina;
                        EDBateriaCuestionario.Pregunta = item.Pregunta;
                        ListaFormulario.Add(EDBateriaCuestionario);
                    }
                }
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item1 in ListaFormulario)
                {
                    var Listavar = (from s in db.Tbl_BateriaResultado
                                    where s.Fk_Id_BateriaCuestionario == item1.Pk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == PkUsuario
                                    select s).FirstOrDefault<BateriaResultado>();
                    if (Listavar != null)
                    {

                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Orden = item1.Orden;
                        EDBateriaResultado.Valor = Listavar.Valor;
                        EDBateriaResultado.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = Listavar.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = Listavar.Fk_Id_BateriaCuestionario;
                        ListaResultado.Add(EDBateriaResultado);

                    }
                }
            }

            foreach (var item in ListaFormulario)
            {
                int ordenf = item.Orden;
                EDBateriaResultado CuestBusqueda = ListaResultado.Where(s => s.Orden == ordenf).FirstOrDefault();
                if (CuestBusqueda==null)
                {
                    if (ordenf >= 1 && ordenf <=12)
                    {
                        Pagina = 1;
                        return Pagina;
                    }
                    if (ordenf >= 13 && ordenf <= 23)
                    {
                        Pagina = 2;
                        return Pagina;
                    }
                    if (ordenf >= 24 && ordenf <= 38)
                    {
                        Pagina = 3;
                        return Pagina;
                    }
                    if (ordenf >= 39 && ordenf <= 52)
                    {
                        Pagina = 4;
                        return Pagina;
                    }
                    if (ordenf >= 53 && ordenf <= 62)
                    {
                        Pagina = 5;
                        return Pagina;
                    }
                    if (ordenf >= 63 && ordenf <= 75)
                    {
                        Pagina = 6;
                        return Pagina;
                    }
                    if (ordenf >= 76 && ordenf <= 89)
                    {
                        Pagina = 7;
                        return Pagina;
                    }
                    if (ordenf >= 90 && ordenf <= 105)
                    {
                        Pagina = 8;
                        return Pagina;
                    }
                    if (ordenf >= 106 && ordenf <= 114)
                    {
                        Pagina = 9;
                        if (EDBateriaUsuario.CheckPag9!=null)
                        {
                            if (EDBateriaUsuario.CheckPag9 == "No")
                            {
                                Pagina = 10;
                            }
                        }
                        return Pagina;
                    }
                    if (ordenf >= 115 && ordenf <= 123)
                    {
                        Pagina = 10;
                        if (EDBateriaUsuario.CheckPag9 == null)
                        {
                          Pagina = 9;
                        }
                        return Pagina;
                    }
                }
            }
            
                
            




            return Pagina;
        }
        public int PaginaIntralaboralB(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int PkUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
            int Pagina = 0;
            List<EDBateriaCuestionario> ListaFormulario = new List<EDBateriaCuestionario>();
            List<EDBateriaResultado> ListaResultado = new List<EDBateriaResultado>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaCuestionario
                                join d in db.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
                                join e in db.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
                                where e.Pk_Id_Bateria == 2
                                select s).ToList<BateriaCuestionario>().Distinct();
                if (Listavar != null)
                {
                    Listavar = Listavar.OrderBy(s => s.Orden).ToList();
                    foreach (var item in Listavar)
                    {
                        EDBateriaCuestionario EDBateriaCuestionario = new EDBateriaCuestionario();
                        EDBateriaCuestionario.Pk_Id_BateriaCuestionario = item.Pk_Id_BateriaCuestionario;
                        EDBateriaCuestionario.Dominio = item.Dominio;
                        EDBateriaCuestionario.Orden = item.Orden;
                        EDBateriaCuestionario.Pagina = item.Pagina;
                        EDBateriaCuestionario.Pregunta = item.Pregunta;
                        ListaFormulario.Add(EDBateriaCuestionario);
                    }
                }
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item1 in ListaFormulario)
                {
                    var Listavar = (from s in db.Tbl_BateriaResultado
                                    where s.Fk_Id_BateriaCuestionario == item1.Pk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == PkUsuario
                                    select s).FirstOrDefault<BateriaResultado>();
                    if (Listavar != null)
                    {

                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Orden = item1.Orden;
                        EDBateriaResultado.Valor = Listavar.Valor;
                        EDBateriaResultado.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = Listavar.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = Listavar.Fk_Id_BateriaCuestionario;
                        ListaResultado.Add(EDBateriaResultado);

                    }
                }
            }

            foreach (var item in ListaFormulario)
            {
                int ordenf = item.Orden;
                EDBateriaResultado CuestBusqueda = ListaResultado.Where(s => s.Orden == ordenf).FirstOrDefault();
                if (CuestBusqueda == null)
                {
                    if (ordenf >= 1 && ordenf <= 12)
                    {
                        Pagina = 1;
                        return Pagina;
                    }
                    if (ordenf >= 13 && ordenf <= 22)
                    {
                        Pagina = 2;
                        return Pagina;
                    }
                    if (ordenf >= 23 && ordenf <= 35)
                    {
                        Pagina = 3;
                        return Pagina;
                    }
                    if (ordenf >= 36 && ordenf <= 44)
                    {
                        Pagina = 4;
                        return Pagina;
                    }
                    if (ordenf >= 45 && ordenf <= 54)
                    {
                        Pagina = 5;
                        return Pagina;
                    }
                    if (ordenf >= 55 && ordenf <= 68)
                    {
                        Pagina = 6;
                        return Pagina;
                    }
                    if (ordenf >= 69 && ordenf <= 78)
                    {
                        Pagina = 7;
                        return Pagina;
                    }
                    if (ordenf >= 79 && ordenf <= 88)
                    {
                        Pagina = 8;
                        return Pagina;
                    }
                    if (ordenf >= 89 && ordenf <= 97)
                    {
                        Pagina = 9;
                        return Pagina;
                    }

                }
            }







            return Pagina;
        }
        public int PaginaExtralaboral(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int PkUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
            int Pagina = 0;
            List<EDBateriaCuestionario> ListaFormulario = new List<EDBateriaCuestionario>();
            List<EDBateriaResultado> ListaResultado = new List<EDBateriaResultado>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaCuestionario
                                join d in db.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
                                join e in db.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
                                where e.Pk_Id_Bateria == 3
                                select s).ToList<BateriaCuestionario>().Distinct();
                if (Listavar != null)
                {
                    Listavar = Listavar.OrderBy(s => s.Orden).ToList();
                    foreach (var item in Listavar)
                    {
                        EDBateriaCuestionario EDBateriaCuestionario = new EDBateriaCuestionario();
                        EDBateriaCuestionario.Pk_Id_BateriaCuestionario = item.Pk_Id_BateriaCuestionario;
                        EDBateriaCuestionario.Dominio = item.Dominio;
                        EDBateriaCuestionario.Orden = item.Orden;
                        EDBateriaCuestionario.Pagina = item.Pagina;
                        EDBateriaCuestionario.Pregunta = item.Pregunta;
                        ListaFormulario.Add(EDBateriaCuestionario);
                    }
                }
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item1 in ListaFormulario)
                {
                    var Listavar = (from s in db.Tbl_BateriaResultado
                                    where s.Fk_Id_BateriaCuestionario == item1.Pk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == PkUsuario
                                    select s).FirstOrDefault<BateriaResultado>();
                    if (Listavar != null)
                    {

                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Orden = item1.Orden;
                        EDBateriaResultado.Valor = Listavar.Valor;
                        EDBateriaResultado.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = Listavar.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = Listavar.Fk_Id_BateriaCuestionario;
                        ListaResultado.Add(EDBateriaResultado);

                    }
                }
            }

            foreach (var item in ListaFormulario)
            {
                int ordenf = item.Orden;
                EDBateriaResultado CuestBusqueda = ListaResultado.Where(s => s.Orden == ordenf).FirstOrDefault();
                if (CuestBusqueda == null)
                {
                    if (ordenf >= 1 && ordenf <= 13)
                    {
                        Pagina = 1;
                        return Pagina;
                    }
                    if (ordenf >= 14 && ordenf <= 27)
                    {
                        Pagina = 2;
                        return Pagina;
                    }
                    if (ordenf >= 28 && ordenf <= 31)
                    {
                        Pagina = 3;
                        return Pagina;
                    }
                }
            }







            return Pagina;
        }
        public int PaginaEstres(string formdata, EDBateriaUsuario EDBateriaUsuario)
        {
            int PkUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
            int Pagina = 0;
            List<EDBateriaCuestionario> ListaFormulario = new List<EDBateriaCuestionario>();
            List<EDBateriaResultado> ListaResultado = new List<EDBateriaResultado>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaCuestionario
                                join d in db.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
                                join e in db.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
                                where e.Pk_Id_Bateria == 4
                                select s).ToList<BateriaCuestionario>().Distinct();
                if (Listavar != null)
                {
                    Listavar = Listavar.OrderBy(s => s.Orden).ToList();
                    foreach (var item in Listavar)
                    {
                        EDBateriaCuestionario EDBateriaCuestionario = new EDBateriaCuestionario();
                        EDBateriaCuestionario.Pk_Id_BateriaCuestionario = item.Pk_Id_BateriaCuestionario;
                        EDBateriaCuestionario.Dominio = item.Dominio;
                        EDBateriaCuestionario.Orden = item.Orden;
                        EDBateriaCuestionario.Pagina = item.Pagina;
                        EDBateriaCuestionario.Pregunta = item.Pregunta;
                        ListaFormulario.Add(EDBateriaCuestionario);
                    }
                }
            }


            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item1 in ListaFormulario)
                {
                    var Listavar = (from s in db.Tbl_BateriaResultado
                                    where s.Fk_Id_BateriaCuestionario == item1.Pk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == PkUsuario
                                    select s).FirstOrDefault<BateriaResultado>();
                    if (Listavar != null)
                    {

                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Orden = item1.Orden;
                        EDBateriaResultado.Valor = Listavar.Valor;
                        EDBateriaResultado.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = Listavar.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = Listavar.Fk_Id_BateriaCuestionario;
                        ListaResultado.Add(EDBateriaResultado);

                    }
                }
            }
            foreach (var item in ListaFormulario)
            {
                int ordenf = item.Orden;
                EDBateriaResultado CuestBusqueda = ListaResultado.Where(s => s.Orden == ordenf).FirstOrDefault();
                if (CuestBusqueda == null)
                {
                    if (ordenf >= 1 && ordenf <= 31)
                    {
                        Pagina = 1;
                        return Pagina;
                    }
                }
            }

            return Pagina;
        }
        public bool GuardarEncuesta(List<EDBateriaResultado> ListaResultado, string key, int form)
        {
            bool ProbarGuardar = false;
            EDBateriaUsuario EDBateriaUsuario = ConsultarConvocadoKey(key, form);
            List<EDBateriaCuestionario> ListaCuestionario = ConsultarFormulario(1, form);
            List<BateriaResultado> ListaResultado1 = new List<BateriaResultado>();
            List<BateriaResultado> ListaResultadoE = new List<BateriaResultado>();
            List<BateriaResultado> ListaResultadoG = new List<BateriaResultado>();
            if (EDBateriaUsuario!=null)
            {
                if (ListaCuestionario.Count>0)
                {
                    foreach (var item in ListaResultado)
                    {
                        BateriaResultado BateriaResultado = new BateriaResultado();
                        EDBateriaCuestionario EDBateriaCuestionario1 = ListaCuestionario.Where(s => s.Orden == item.Orden).FirstOrDefault();
                        if (EDBateriaCuestionario1!=null)
                        {
                            BateriaResultado.Fk_Id_BateriaCuestionario = EDBateriaCuestionario1.Pk_Id_BateriaCuestionario;
                        }
                        BateriaResultado.Fk_Id_BateriaUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
                        if (item.ValorS=="A")
                        {
                            BateriaResultado.Valor = 1;
                        }
                        if (item.ValorS == "B")
                        {
                            BateriaResultado.Valor = 2;
                        }
                        if (item.ValorS == "C")
                        {
                            BateriaResultado.Valor = 3;
                        }
                        if (item.ValorS == "D")
                        {
                            BateriaResultado.Valor = 4;
                        }
                        if (item.ValorS == "E")
                        {
                            BateriaResultado.Valor = 5;
                        }
                        if (BateriaResultado.Valor>0)
                        {
                            ListaResultado1.Add(BateriaResultado);
                        }
                    }
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        foreach (var item1 in ListaResultado1)
                        {
                            var Listavar = (from s in db.Tbl_BateriaResultado
                                            where s.Fk_Id_BateriaCuestionario == item1.Fk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == item1.Fk_Id_BateriaUsuario
                                            select s).FirstOrDefault<BateriaResultado>();
                            if (Listavar != null)
                            {
                                item1.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                                BateriaResultado BateriaResultado = new BateriaResultado();
                                BateriaResultado.Fk_Id_BateriaCuestionario = item1.Fk_Id_BateriaCuestionario;
                                BateriaResultado.Fk_Id_BateriaUsuario = item1.Fk_Id_BateriaUsuario;
                                BateriaResultado.Pk_Id_BateriaResultado = item1.Pk_Id_BateriaResultado;
                                BateriaResultado.Valor = item1.Valor;
                                ListaResultadoE.Add(BateriaResultado);
                            }
                            else
                            {
                                BateriaResultado BateriaResultado = new BateriaResultado();
                                BateriaResultado.Fk_Id_BateriaCuestionario = item1.Fk_Id_BateriaCuestionario;
                                BateriaResultado.Fk_Id_BateriaUsuario = item1.Fk_Id_BateriaUsuario;
                                BateriaResultado.Valor = item1.Valor;
                                ListaResultadoG.Add(BateriaResultado);
                            }
                        }
                    }
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        foreach (var item in ListaResultadoE)
                        {
                            db.Entry(item).State = EntityState.Modified;
                        }
                        foreach (var item in ListaResultadoG)
                        {
                            db.Tbl_BateriaResultado.Add(item);
                        }
                        try
                        {
                            db.SaveChanges();
                            ProbarGuardar = true;
                            return ProbarGuardar;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            return ProbarGuardar;
        }
        public bool GuardarEncuestaExtra(List<EDBateriaResultado> ListaResultado, string key, int form)
        {
            bool ProbarGuardar = false;
            EDBateriaUsuario EDBateriaUsuario = ConsultarConvocadoKeyExtra(key, form);
            List<EDBateriaCuestionario> ListaCuestionario = ConsultarFormulario(1, 3);
            List<BateriaResultado> ListaResultado1 = new List<BateriaResultado>();
            List<BateriaResultado> ListaResultadoE = new List<BateriaResultado>();
            List<BateriaResultado> ListaResultadoG = new List<BateriaResultado>();
            if (EDBateriaUsuario != null)
            {
                if (ListaCuestionario.Count > 0)
                {
                    foreach (var item in ListaResultado)
                    {
                        BateriaResultado BateriaResultado = new BateriaResultado();
                        EDBateriaCuestionario EDBateriaCuestionario1 = ListaCuestionario.Where(s => s.Orden == item.Orden).FirstOrDefault();
                        if (EDBateriaCuestionario1 != null)
                        {
                            BateriaResultado.Fk_Id_BateriaCuestionario = EDBateriaCuestionario1.Pk_Id_BateriaCuestionario;
                        }
                        BateriaResultado.Fk_Id_BateriaUsuario = EDBateriaUsuario.Pk_Id_BateriaUsuario;
                        if (item.ValorS == "A")
                        {
                            BateriaResultado.Valor = 1;
                        }
                        if (item.ValorS == "B")
                        {
                            BateriaResultado.Valor = 2;
                        }
                        if (item.ValorS == "C")
                        {
                            BateriaResultado.Valor = 3;
                        }
                        if (item.ValorS == "D")
                        {
                            BateriaResultado.Valor = 4;
                        }
                        if (item.ValorS == "E")
                        {
                            BateriaResultado.Valor = 5;
                        }
                        if (BateriaResultado.Valor > 0)
                        {
                            ListaResultado1.Add(BateriaResultado);
                        }
                    }
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        foreach (var item1 in ListaResultado1)
                        {
                            var Listavar = (from s in db.Tbl_BateriaResultado
                                            where s.Fk_Id_BateriaCuestionario == item1.Fk_Id_BateriaCuestionario && s.Fk_Id_BateriaUsuario == item1.Fk_Id_BateriaUsuario
                                            select s).FirstOrDefault<BateriaResultado>();
                            if (Listavar != null)
                            {
                                item1.Pk_Id_BateriaResultado = Listavar.Pk_Id_BateriaResultado;
                                BateriaResultado BateriaResultado = new BateriaResultado();
                                BateriaResultado.Fk_Id_BateriaCuestionario = item1.Fk_Id_BateriaCuestionario;
                                BateriaResultado.Fk_Id_BateriaUsuario = item1.Fk_Id_BateriaUsuario;
                                BateriaResultado.Pk_Id_BateriaResultado = item1.Pk_Id_BateriaResultado;
                                BateriaResultado.Valor = item1.Valor;
                                ListaResultadoE.Add(BateriaResultado);
                            }
                            else
                            {
                                BateriaResultado BateriaResultado = new BateriaResultado();
                                BateriaResultado.Fk_Id_BateriaCuestionario = item1.Fk_Id_BateriaCuestionario;
                                BateriaResultado.Fk_Id_BateriaUsuario = item1.Fk_Id_BateriaUsuario;
                                BateriaResultado.Valor = item1.Valor;
                                ListaResultadoG.Add(BateriaResultado);
                            }
                        }
                    }
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        foreach (var item in ListaResultadoE)
                        {
                            db.Entry(item).State = EntityState.Modified;
                        }
                        foreach (var item in ListaResultadoG)
                        {
                            db.Tbl_BateriaResultado.Add(item);
                        }
                        try
                        {
                            db.SaveChanges();
                            ProbarGuardar = true;
                            return ProbarGuardar;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            return ProbarGuardar;
        }
        public bool TerminarEncuesta(int pkusuario)
        {
            DateTime now = DateTime.Now;
            bool ProbarGuardar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == pkusuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    Listavar.RegistroOperacion = "Fin";
                    Listavar.FechaRespuesta = now;
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        ProbarGuardar = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return ProbarGuardar;

        }
        public bool TerminarEncuestaRechazo(int pkusuario)
        {
            bool ProbarGuardar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == pkusuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    Listavar.ConfirmacionParticipacion = "Rechazado";
                    Listavar.RegistroOperacion = "Rechazo";
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        ProbarGuardar = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return ProbarGuardar;

        }
        public bool AceptarEncuesta(int pkusuario)
        {
            bool ProbarGuardar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == pkusuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    Listavar.ConfirmacionParticipacion = "Aceptado";
                    Listavar.RegistroOperacion = "Aceptado";
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        ProbarGuardar = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return ProbarGuardar;

        }
        public void RecibirEditarDocumento(int pkusuario, string cedula)
        {
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == pkusuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    Listavar.DocumentoDigitado = cedula;
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        public void EditarSegunCheck(int pkusuario, string Check9, string Check10)
        {

            if (Check9!=null)
            {
                if (Check9=="No")
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        var Listavar = (from s in db.Tbl_BateriaResultado
                                        where s.Fk_Id_BateriaUsuario == pkusuario
                                        select s).ToList<BateriaResultado>();
                        if (Listavar != null)
                        {
                            foreach (var item in Listavar)
                            {
                                if (item.BateriaCuestionario.Orden >= 106 && item.BateriaCuestionario.Orden <= 114)
                                {
                                    db.Entry(item).State = EntityState.Deleted;
                                }
                            }
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }
            if (Check10 != null)
            {
                if (Check10 == "No")
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        var Listavar = (from s in db.Tbl_BateriaResultado
                                        where s.Fk_Id_BateriaUsuario == pkusuario
                                        select s).ToList<BateriaResultado>();
                        if (Listavar != null)
                        {
                            foreach (var item in Listavar)
                            {
                                if (item.BateriaCuestionario.Orden >= 115 && item.BateriaCuestionario.Orden <= 123)
                                {
                                    db.Entry(item).State = EntityState.Deleted;
                                }
                            }
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
            }
            
        }
        public void EditarCheck9y10(int pkusuario, int tipo, string val)
        {
            if (tipo==9)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_BateriaUsuario
                                    where s.Pk_Id_BateriaUsuario == pkusuario
                                    select s).FirstOrDefault<BateriaUsuario>();
                    if (Listavar != null)
                    {
                        Listavar.CheckPag9 = val;
                        db.Entry(Listavar).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            if (tipo == 10)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var Listavar = (from s in db.Tbl_BateriaUsuario
                                    where s.Pk_Id_BateriaUsuario == pkusuario
                                    select s).FirstOrDefault<BateriaUsuario>();
                    if (Listavar != null)
                    {
                        Listavar.CheckPag10 = val;
                        db.Entry(Listavar).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

        }
        //Gestion
        public List<EDBateria> ConsultarBaterias()
        {
            List<EDBateria> ListaBaterias = new List<EDBateria>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Bateria
                                select s).ToList<Bateria>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateria EDBateria = new EDBateria();
                        EDBateria.Pk_Id_Bateria = item.Pk_Id_Bateria;
                        EDBateria.Descripcion = item.Descripción;
                        EDBateria.Nombre = item.Nombre;
                        EDBateria.ModalidadesAplicacion = item.ModalidadesAplicacion;
                        ListaBaterias.Add(EDBateria);
                    }
                }
            }
            return ListaBaterias;
        }
        public EDBateriaGestion CrearGestionNuevo(EDBateriaGestion EDBateriaGestionC)
        {
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            BateriaGestion BateriaGestion = new BateriaGestion();

            BateriaGestion.FechaFinalizacion = EDBateriaGestionC.FechaFinalizacion;
            BateriaGestion.FechaRegistro = EDBateriaGestionC.FechaRegistro;
            BateriaGestion.Finalizada = false;
            BateriaGestion.Fk_Id_Bateria = EDBateriaGestionC.Fk_Id_Bateria;
            BateriaGestion.Fk_Id_Empresa = EDBateriaGestionC.Fk_Id_Empresa;
            BateriaGestion.TokenPublico = EDBateriaGestionC.TokenPublico;
            BateriaGestion.Estado = EDBateriaGestionC.EstadoInt;
            BateriaGestion.bateriaExtra = EDBateriaGestionC.bateriaExtra;


            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Tbl_BateriaGestion.Add(BateriaGestion);
                try
                {
                    db.SaveChanges();
                    var Listavar = (from s in db.Tbl_BateriaGestion
                                    where s.Fk_Id_Empresa== EDBateriaGestionC.Fk_Id_Empresa && s.TokenPublico== EDBateriaGestionC.TokenPublico
                                    select s).FirstOrDefault<BateriaGestion>();
                    if (Listavar != null)
                    {
                        EDBateriaGestion.FechaFinalizacion = Listavar.FechaFinalizacion;
                        EDBateriaGestion.FechaRegistro = Listavar.FechaRegistro;
                        EDBateriaGestion.Finalizada = Listavar.Finalizada;
                        EDBateriaGestion.Fk_Id_Bateria = Listavar.Fk_Id_Bateria;
                        EDBateriaGestion.Fk_Id_Empresa = Listavar.Fk_Id_Empresa;
                        EDBateriaGestion.TokenPublico = Listavar.TokenPublico;
                        EDBateriaGestion.Pk_Id_BateriaGestion = Listavar.Pk_Id_BateriaGestion;
                        EDBateriaGestion.EstadoInt = Listavar.Estado;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return EDBateriaGestion;
        }
        public List<EDBateriaUsuario> ConsultarUsuariosCorreos(int IdEmpresa, int IdConv, int IdGestion)
        {
            List<EDBateriaUsuario> ListaUsuarios = new List<EDBateriaUsuario>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                join d in db.Tbl_BateriaGestion on s.Fk_Id_BateriaGestion equals d.Pk_Id_BateriaGestion
                                where d.Fk_Id_Empresa== IdEmpresa && d.Pk_Id_BateriaGestion== IdGestion && s.NumeroIntentos!=1
                                select s).ToList<BateriaUsuario>().Distinct();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateriaUsuario EDUsuario = new EDBateriaUsuario();
                        EDUsuario.Nombre = item.Nombre;
                        EDUsuario.NumeroIdentificacion = item.NumeroIdentificacion;
                        EDUsuario.TipoDocumento = item.TipoDocumento;
                        EDUsuario.CorreoElectronico = item.CorreoElectronico;
                        EDUsuario.Id_Empresa = item.Id_Empresa;
                        EDUsuario.TokenPrivado = item.TokenPrivado;
                        EDUsuario.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                        EDUsuario.Pk_Id_BateriaUsuario = item.Pk_Id_BateriaUsuario;
                        EDUsuario.TipoConv = item.TipoConv;
                        EDUsuario.EstadoEnvio = item.EstadoEnvio;

                        DateTime fe = item.FechaEnvio ?? DateTime.MinValue;
                        if (fe != DateTime.MinValue)
                        {
                            EDUsuario.FechaEnvio = fe;

                        }
                        DateTime fr = item.FechaRespuesta ?? DateTime.MinValue;
                        if (fr != DateTime.MinValue)
                        {
                            EDUsuario.FechaRespuesta = fr;
                        }

                        ListaUsuarios.Add(EDUsuario);
                    }
                }
            }
            return ListaUsuarios;
        }
        public bool CrearConvocado(EDBateriaUsuario EDBateriaUsuario)
        {
            bool guardar = false;
            BateriaUsuario BateriaUsuario = new BateriaUsuario();
            BateriaUsuario.Nombre = EDBateriaUsuario.Nombre;
            BateriaUsuario.NumeroIdentificacion = EDBateriaUsuario.NumeroIdentificacion;
            BateriaUsuario.TipoDocumento = EDBateriaUsuario.TipoDocumento;
            BateriaUsuario.CorreoElectronico = EDBateriaUsuario.CorreoElectronico;
            BateriaUsuario.Id_Empresa = EDBateriaUsuario.Id_Empresa;
            BateriaUsuario.TokenPrivado = EDBateriaUsuario.TokenPrivado;
            BateriaUsuario.Fk_Id_BateriaGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
            BateriaUsuario.TipoConv = EDBateriaUsuario.TipoConv;
            BateriaUsuario.EstadoEnvio = EDBateriaUsuario.EstadoEnvio;
            BateriaUsuario.NumeroIntentos = EDBateriaUsuario.NumeroIntentos;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Tbl_BateriaUsuario.Add(BateriaUsuario);
                try
                {
                    db.SaveChanges();
                    guardar = true;
                    return guardar;
                }
                catch (Exception ex)
                {
                }
            }

            return guardar;
        }
        public bool CrearConvocadoPublico(EDBateriaUsuario EDBateriaUsuario)
        {
            bool guardar = false;
            BateriaUsuario BateriaUsuario = new BateriaUsuario();
            BateriaUsuario.Nombre = EDBateriaUsuario.Nombre;
            BateriaUsuario.NumeroIdentificacion = EDBateriaUsuario.NumeroIdentificacion;
            BateriaUsuario.TipoDocumento = EDBateriaUsuario.TipoDocumento;
            BateriaUsuario.CorreoElectronico = EDBateriaUsuario.CorreoElectronico;
            BateriaUsuario.Id_Empresa = EDBateriaUsuario.Id_Empresa;
            BateriaUsuario.TokenPrivado = EDBateriaUsuario.TokenPrivado;
            BateriaUsuario.Fk_Id_BateriaGestion = EDBateriaUsuario.Fk_Id_BateriaGestion;
            BateriaUsuario.TipoConv = EDBateriaUsuario.TipoConv;
            BateriaUsuario.EstadoEnvio = EDBateriaUsuario.EstadoEnvio;
            BateriaUsuario.DocumentoDigitado = EDBateriaUsuario.DocumentoDigitado;
            BateriaUsuario.NumeroIntentos = EDBateriaUsuario.NumeroIntentos;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                db.Tbl_BateriaUsuario.Add(BateriaUsuario);
                try
                {
                    db.SaveChanges();
                    guardar = true;
                    return guardar;
                }
                catch (Exception ex)
                {
                }
            }

            return guardar;
        }
        public List<EDBateriaUsuario> CrearConvocadoMasivo(List<EDBateriaUsuario> ListaEDBateriaUsuario, int IdGestion, bool extra)
        {
            List<EDBateriaUsuario> guardar = new List<EDBateriaUsuario>();
            List<EDBateriaUsuario> ListaEDUsuarioRegistrado = ConsultarUsuarioRegistrados(IdGestion);
            using (SG_SSTContext db = new SG_SSTContext())
            {
                foreach (var item in ListaEDBateriaUsuario)
                {
                    EDBateriaUsuario Registrado = ListaEDUsuarioRegistrado.Where(s => s.NumeroIdentificacion == item.NumeroIdentificacion).FirstOrDefault();
                    if (Registrado==null)
                    {
                        BateriaUsuario BateriaUsuario = new BateriaUsuario();
                        BateriaUsuario EDBateriaUsuarioExtra = new BateriaUsuario();
                        BateriaUsuario.Nombre = item.Nombre;
                        BateriaUsuario.NumeroIdentificacion = item.NumeroIdentificacion;
                        BateriaUsuario.TipoDocumento = item.TipoDocumento;
                        BateriaUsuario.CorreoElectronico = item.CorreoElectronico;
                        BateriaUsuario.Id_Empresa = item.Id_Empresa;
                        BateriaUsuario.TokenPrivado = item.TokenPrivado;
                        BateriaUsuario.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                        BateriaUsuario.TipoConv = item.TipoConv;
                        BateriaUsuario.EstadoEnvio = item.EstadoEnvio;
                        BateriaUsuario.NumeroIntentos = 0;
                        if (extra)
                        {
                            EDBateriaUsuarioExtra.Nombre = item.Nombre;
                            EDBateriaUsuarioExtra.NumeroIdentificacion = item.NumeroIdentificacion;
                            EDBateriaUsuarioExtra.TipoDocumento = item.TipoDocumento;
                            EDBateriaUsuarioExtra.CorreoElectronico = item.CorreoElectronico;
                            EDBateriaUsuarioExtra.Id_Empresa = item.Id_Empresa;
                            EDBateriaUsuarioExtra.TokenPrivado = item.TokenPrivado;
                            EDBateriaUsuarioExtra.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                            EDBateriaUsuarioExtra.TipoConv = item.TipoConv;
                            EDBateriaUsuarioExtra.EstadoEnvio = item.EstadoEnvio;
                            EDBateriaUsuarioExtra.NumeroIntentos = 1;
                        }
                        guardar.Add(item);
                        db.Tbl_BateriaUsuario.Add(BateriaUsuario);
                        if (extra)
                        {
                            db.Tbl_BateriaUsuario.Add(EDBateriaUsuarioExtra);
                        }
                    }
                }
                try
                {
                    db.SaveChanges();
                    return guardar;
                }
                catch (Exception ex)
                {
                }
            }
            return guardar;
        }
        public List<EDBateriaUsuario> ConsultarUsuarioRegistrados(int IdGestion)
        {
            List<EDBateriaUsuario> ListaUsuario = new List<EDBateriaUsuario>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Fk_Id_BateriaGestion== IdGestion
                                select s).ToList<BateriaUsuario>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                        EDBateriaUsuario.Nombre = item.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = item.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = item.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = item.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = item.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = item.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = item.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = item.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = item.EstadoEnvio;
                        EDBateriaUsuario.RegistroOperacion = item.RegistroOperacion;

                        DateTime fe = item.FechaEnvio ?? DateTime.MinValue;
                        if (fe != DateTime.MinValue)
                        {
                            EDBateriaUsuario.FechaEnvio = fe;
                            
                        }
                        DateTime fr = item.FechaRespuesta ?? DateTime.MinValue;
                        if (fr != DateTime.MinValue)
                        {
                            EDBateriaUsuario.FechaRespuesta = fr;
                        }

                        


                        ListaUsuario.Add(EDBateriaUsuario);
                    }
                }
            }
            return ListaUsuario;
        }
        public EDBateriaGestion ConsultarGestion(int IdGestion, int IdEmpresa)
        {
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            BateriaGestion BateriaGestion = new BateriaGestion();
            List<EDBateriaUsuario> ListaUsuarios = new List<EDBateriaUsuario>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Fk_Id_Empresa == IdEmpresa && s.Pk_Id_BateriaGestion == IdGestion
                                select s).FirstOrDefault<BateriaGestion>();
                if (Listavar != null)
                {
                    EDBateriaGestion.FechaFinalizacion = Listavar.FechaFinalizacion;
                    EDBateriaGestion.FechaRegistro = Listavar.FechaRegistro;
                    EDBateriaGestion.Finalizada = Listavar.Finalizada;
                    EDBateriaGestion.Fk_Id_Bateria = Listavar.Fk_Id_Bateria;
                    EDBateriaGestion.Fk_Id_Empresa = Listavar.Fk_Id_Empresa;
                    EDBateriaGestion.TokenPublico = Listavar.TokenPublico;
                    EDBateriaGestion.Pk_Id_BateriaGestion = Listavar.Pk_Id_BateriaGestion;
                    EDBateriaGestion.EstadoInt = Listavar.Estado;
                    EDBateriaGestion.bateriaExtra = Listavar.bateriaExtra;

                    if (EDBateriaGestion.Fk_Id_Bateria == 1 & EDBateriaGestion.bateriaExtra == 0)
                    {
                        EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A";
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 & EDBateriaGestion.bateriaExtra == 0)
                    {
                        EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B";
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 1 & EDBateriaGestion.bateriaExtra == 3)
                    {
                        EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral";
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 2 & EDBateriaGestion.bateriaExtra == 3)
                    {
                        EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral";
                    }
                    if (EDBateriaGestion.Fk_Id_Bateria == 4 & EDBateriaGestion.bateriaExtra == 0)
                    {
                        EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Estres";
                    }
                    if (Listavar.Estado != 0)
                    {
                        if (Listavar.Estado == 1)
                        {
                            EDBateriaGestion.Estado = "Convocatoria - Inactivo";
                        }
                        if (Listavar.Estado == 2)
                        {
                            EDBateriaGestion.Estado = "Activo";
                        }
                        if (Listavar.Estado == 3)
                        {
                            EDBateriaGestion.Estado = "Finalizado";
                        }
                        if (Listavar.Estado == 4)
                        {
                            EDBateriaGestion.Estado = "Activo - Temporalmente";
                        }

                    }

                    var Listavar1 = (from s in db.Tbl_BateriaUsuario
                                     where s.Fk_Id_BateriaGestion == EDBateriaGestion.Pk_Id_BateriaGestion
                                     select s).ToList<BateriaUsuario>();
                    foreach (var item in Listavar1)
                    {
                        EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
                        EDBateriaUsuario.Nombre = item.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = item.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = item.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = item.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = item.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = item.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = item.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = item.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = item.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = item.EstadoEnvio;

                        EDBateriaUsuario.NumeroIntentos = item.NumeroIntentos;
                        EDBateriaUsuario.RegistroOperacion = item.RegistroOperacion;
                        EDBateriaUsuario.MailBody = item.MailBody;
                        EDBateriaUsuario.DocumentoDigitado = item.DocumentoDigitado;
                        EDBateriaUsuario.ConfirmacionParticipacion = item.ConfirmacionParticipacion;

                        if (item.BateriaGestion.Bateria.Pk_Id_Bateria==1)
                        {
                            EDBateriaUsuario.NombreEncuesta = "INTRALABORAL A";
                        }
                        if (item.BateriaGestion.Bateria.Pk_Id_Bateria == 2)
                        {
                            EDBateriaUsuario.NombreEncuesta = "INTRALABORAL B";
                        }
                        if (item.BateriaGestion.Bateria.Pk_Id_Bateria == 4)
                        {
                            EDBateriaUsuario.NombreEncuesta = "ESTRÉS";
                        }


                        DateTime fe = item.FechaEnvio ?? DateTime.MinValue;
                        if (fe != DateTime.MinValue)
                        {
                            EDBateriaUsuario.FechaEnvio = fe;

                        }
                        DateTime fr = item.FechaRespuesta ?? DateTime.MinValue;
                        if (fr != DateTime.MinValue)
                        {
                            EDBateriaUsuario.FechaRespuesta = fr;
                        }

                        ListaUsuarios.Add(EDBateriaUsuario);
                    }
                    EDBateriaGestion.ListaUsuarios = ListaUsuarios;
                }
            }
            return EDBateriaGestion;
        }
        public bool VerificarCorreoExistente(string email,string numeroId, int IdGestion)
        {
            bool validar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.CorreoElectronico == email && s.Fk_Id_BateriaGestion == IdGestion && s.NumeroIdentificacion == numeroId
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    validar = true;
                }
            }
            return validar;
        }
        public EDBateriaUsuario ConsultarConvocado(int IdConv, int IdEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == IdConv
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    EDBateriaUsuario.Nombre = Listavar.Nombre;
                    EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                    EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                    EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                    EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                    EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                    EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                    EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                    EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                    EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                    EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                    EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                    EDBateriaUsuario.MailBody = Listavar.MailBody;
                }
            }

            return EDBateriaUsuario;

        }
        public EDBateriaUsuario ConsultarConvocadoExtra(int IdConv, int IdEmpresa)
        {


            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == IdConv
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    var Listavar1 = (from s in db.Tbl_BateriaUsuario
                                     where s.TokenPrivado == Listavar.TokenPrivado && s.Fk_Id_BateriaGestion == Listavar.Fk_Id_BateriaGestion && s.NumeroIntentos == 1
                                     select s).FirstOrDefault<BateriaUsuario>();
                    if (Listavar1 != null)
                    {
                        EDBateriaUsuario.Nombre = Listavar1.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = Listavar1.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = Listavar1.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = Listavar1.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = Listavar1.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = Listavar1.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar1.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = Listavar1.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar1.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = Listavar1.EstadoEnvio;
                        EDBateriaUsuario.NumeroIntentos = Listavar1.NumeroIntentos;
                        EDBateriaUsuario.RegistroOperacion = Listavar1.RegistroOperacion;
                        EDBateriaUsuario.MailBody = Listavar1.MailBody;
                    }
                    else
                    {
                        EDBateriaUsuario = null;
                    }
                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }

            return EDBateriaUsuario;
        }
        public bool EliminarConvocado(int IdConv, int IdEmpresa)
        {
            bool validar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == IdConv
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    db.Entry(Listavar).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    validar = true;
                }
            }
            return validar;
        }
        public List<EDRol> ConsultarRolesEmpresa(int IdEmpresa)
        {
            List<EDRol> ListaRoles = new List<EDRol>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Rol
                                where s.Fk_Id_Empresa == IdEmpresa
                                select s).ToList<Rol>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDRol EDRol = new EDRol();
                        EDRol.Descripcion = item.Descripcion;
                        EDRol.Fk_Id_Empresa = item.Fk_Id_Empresa ?? 0;
                        EDRol.Pk_Id_Rol = item.Pk_Id_Rol;
                        if (EDRol.Fk_Id_Empresa!=0)
                        {
                            ListaRoles.Add(EDRol);
                        }
                    }
                }
            }
            return ListaRoles;
        }
        public List<EDCargo> ListaCargos()
        {
            List<Cargo> ListaCargos = new List<Cargo>();
            List<EDCargo> ListaEDCargos = new List<EDCargo>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Cargo
                                select s).ToList<Cargo>();
                if (Listavar != null)
                {
                    ListaCargos = Listavar;
                    foreach (var item in ListaCargos)
                    {
                        EDCargo EDCargo = new EDCargo();
                        EDCargo.IDCargo = item.Pk_Id_Cargo;
                        EDCargo.NombreCargo = item.Nombre_Cargo;
                        ListaEDCargos.Add(EDCargo);
                    }
                }
            }

            return ListaEDCargos;
        }
        public List<EDBateriaGestion> ConsultarListaGestion(int IdEmpresa)
        {
            List<EDBateriaGestion> Listagestion = new List<EDBateriaGestion>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Fk_Id_Empresa == IdEmpresa
                                select s).ToList<BateriaGestion>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
                        EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDBateriaGestion.FechaFinalizacion = item.FechaFinalizacion;
                        EDBateriaGestion.FechaRegistro = item.FechaRegistro;
                        EDBateriaGestion.Finalizada = item.Finalizada;
                        EDBateriaGestion.Fk_Id_Bateria = item.Fk_Id_Bateria;
                        EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDBateriaGestion.Pk_Id_BateriaGestion = item.Pk_Id_BateriaGestion;
                        EDBateriaGestion.TokenPublico = item.TokenPublico;
                        EDBateriaGestion.NombreBateria = item.Bateria.Nombre;
                        EDBateriaGestion.EstadoInt = item.Estado;
                        EDBateriaGestion.bateriaExtra = item.bateriaExtra;

                        if (EDBateriaGestion.Fk_Id_Bateria==1 & EDBateriaGestion.bateriaExtra==3)
                        {
                            EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral";
                        }
                        if (EDBateriaGestion.Fk_Id_Bateria == 2 & EDBateriaGestion.bateriaExtra == 3)
                        {
                            EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral";
                        }

                        if (item.Estado!=0)
                        {
                            if (item.Estado == 1)
                            {
                                EDBateriaGestion.Estado = "Convocatoria - Inactivo";
                            }
                            if (item.Estado == 2)
                            {
                                EDBateriaGestion.Estado = "Activo";
                            }
                            if (item.Estado == 3)
                            {
                                EDBateriaGestion.Estado = "Finalizado";
                            }
                            if (item.Estado == 4)
                            {
                                EDBateriaGestion.Estado = "Activo - Temporalmente";
                            }

                        }
                        Listagestion.Add(EDBateriaGestion);
                    }
                }
            }

            foreach (var item in Listagestion)
            {
                int idGestion = item.Pk_Id_BateriaGestion;
                item.ListaUsuarios = ConsultarUsuarioRegistrados(idGestion);
            }

            return Listagestion;

        }
        public List<EDBateriaGestion> ConsultarListaGestionFiltros(int IdEmpresa, string Fantes, string Fdespues, int Tipo)
        {
            List<EDBateriaGestion> Listagestion = new List<EDBateriaGestion>();
            DateTime FechaA_conv = DateTime.MinValue;
            DateTime FechaD_conv = DateTime.MinValue;

            if (Fantes != null && Fdespues != null)
            {
                if (Fantes != string.Empty && Fdespues != string.Empty)
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(Fantes);
                        FechaD_conv = DateTime.Parse(Fdespues);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Fk_Id_Empresa == IdEmpresa
                                select s).ToList<BateriaGestion>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        if (Tipo!=0)
                        {
                            #region consultatipo
                            int idbateria = 0;
                            int extrabateria = 0;
                            if (Tipo==1)
                            {
                                idbateria = 1;
                                extrabateria = 0;
                            }
                            if (Tipo == 2)
                            {
                                idbateria = 2;
                                extrabateria = 0;
                            }
                            if (Tipo == 3)
                            {
                                idbateria = 1;
                                extrabateria = 3;
                            }
                            if (Tipo == 4)
                            {
                                idbateria = 2;
                                extrabateria = 3;
                            }
                            if (Tipo == 5)
                            {
                                idbateria = 4;
                                extrabateria = 0;
                            }
                            if (item.Fk_Id_Bateria== idbateria && item.bateriaExtra== extrabateria)
                            {
                                EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
                                EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                                EDBateriaGestion.FechaFinalizacion = item.FechaFinalizacion;
                                EDBateriaGestion.FechaRegistro = item.FechaRegistro;
                                EDBateriaGestion.Finalizada = item.Finalizada;
                                EDBateriaGestion.Fk_Id_Bateria = item.Fk_Id_Bateria;
                                EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                                EDBateriaGestion.Pk_Id_BateriaGestion = item.Pk_Id_BateriaGestion;
                                EDBateriaGestion.TokenPublico = item.TokenPublico;
                                EDBateriaGestion.NombreBateria = item.Bateria.Nombre;
                                EDBateriaGestion.EstadoInt = item.Estado;
                                EDBateriaGestion.bateriaExtra = item.bateriaExtra;
                                if (item.Estado != 0)
                                {
                                    if (item.Estado == 1)
                                    {
                                        EDBateriaGestion.Estado = "Convocatoria - Inactivo";
                                    }
                                    if (item.Estado == 2)
                                    {
                                        EDBateriaGestion.Estado = "Activo";
                                    }
                                    if (item.Estado == 3)
                                    {
                                        EDBateriaGestion.Estado = "Finalizado";
                                    }
                                    if (item.Estado == 4)
                                    {
                                        EDBateriaGestion.Estado = "Activo - Temporalmente";
                                    }
                                }
                                if (EDBateriaGestion.Fk_Id_Bateria == 1 & EDBateriaGestion.bateriaExtra == 3)
                                {
                                    EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral";
                                }
                                if (EDBateriaGestion.Fk_Id_Bateria == 2 & EDBateriaGestion.bateriaExtra == 3)
                                {
                                    EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral";
                                }
                                Listagestion.Add(EDBateriaGestion);
                            }
                            #endregion
                        }
                        else
                        {
                            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
                            EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                            EDBateriaGestion.FechaFinalizacion = item.FechaFinalizacion;
                            EDBateriaGestion.FechaRegistro = item.FechaRegistro;
                            EDBateriaGestion.Finalizada = item.Finalizada;
                            EDBateriaGestion.Fk_Id_Bateria = item.Fk_Id_Bateria;
                            EDBateriaGestion.Fk_Id_Empresa = item.Fk_Id_Empresa;
                            EDBateriaGestion.Pk_Id_BateriaGestion = item.Pk_Id_BateriaGestion;
                            EDBateriaGestion.TokenPublico = item.TokenPublico;
                            EDBateriaGestion.NombreBateria = item.Bateria.Nombre;
                            EDBateriaGestion.EstadoInt = item.Estado;
                            EDBateriaGestion.bateriaExtra = item.bateriaExtra;
                            if (item.Estado != 0)
                            {
                                if (item.Estado == 1)
                                {
                                    EDBateriaGestion.Estado = "Convocatoria - Inactivo";
                                }
                                if (item.Estado == 2)
                                {
                                    EDBateriaGestion.Estado = "Activo";
                                }
                                if (item.Estado == 3)
                                {
                                    EDBateriaGestion.Estado = "Finalizado";
                                }
                                if (item.Estado == 4)
                                {
                                    EDBateriaGestion.Estado = "Activo - Temporalmente";
                                }
                            }
                            if (EDBateriaGestion.Fk_Id_Bateria == 1 & EDBateriaGestion.bateriaExtra == 3)
                            {
                                EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA A y Cuestionario Extralaboral";
                            }
                            if (EDBateriaGestion.Fk_Id_Bateria == 2 & EDBateriaGestion.bateriaExtra == 3)
                            {
                                EDBateriaGestion.NombreBateria = "Cuestionario de Factores de Riesgo Psicosocial Intralaboral FORMA B y Cuestionario Extralaboral";
                            }
                            Listagestion.Add(EDBateriaGestion);
                        }
                        
                    }
                }
            }

            if (FechaA_conv != DateTime.MinValue && FechaD_conv != DateTime.MinValue)
            {
                FechaD_conv = FechaD_conv.AddHours(23).AddMinutes(59).AddSeconds(59);
                Listagestion = Listagestion.Where(s => s.FechaRegistro >= FechaA_conv).ToList();
                Listagestion = Listagestion.Where(s => s.FechaRegistro <= FechaD_conv).ToList();
            }

            foreach (var item in Listagestion)
            {
                int idGestion = item.Pk_Id_BateriaGestion;
                item.ListaUsuarios = ConsultarUsuarioRegistrados(idGestion);
            }

            return Listagestion;

        }
        public EDBateriaUsuario ConsultarConvocadoKey(string key, int Form)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.TokenPrivado == key && s.NumeroIntentos==0
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    if (Listavar.BateriaGestion.Bateria.Pk_Id_Bateria== Form)
                    {
                        EDBateriaUsuario.Nombre = Listavar.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                        EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                        EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                        EDBateriaUsuario.MailBody = Listavar.MailBody;
                        EDBateriaUsuario.DocumentoDigitado = Listavar.DocumentoDigitado;
                        EDBateriaUsuario.ConfirmacionParticipacion = Listavar.ConfirmacionParticipacion;
                        EDBateriaUsuario.CheckPag9 = Listavar.CheckPag9;
                        EDBateriaUsuario.CheckPag10 = Listavar.CheckPag10;

                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 1 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 2 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria==1 && Listavar.BateriaGestion.bateriaExtra == 3)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre + " y Cuestionario Extralaboral";
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 2 && Listavar.BateriaGestion.bateriaExtra == 3)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre + " y Cuestionario Extralaboral";
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 4 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }

                        
                    }
                    else
                    {
                        EDBateriaUsuario = null;
                    }
                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }

            return EDBateriaUsuario;
        }
        public EDBateriaUsuario ConsultarConvocadoKeyExtra(string key, int Form)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.TokenPrivado == key && s.NumeroIntentos == 1
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    if (Listavar.BateriaGestion.Bateria.Pk_Id_Bateria == Form)
                    {
                        EDBateriaUsuario.Nombre = Listavar.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                        EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                        EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                        EDBateriaUsuario.MailBody = Listavar.MailBody;
                        EDBateriaUsuario.DocumentoDigitado = Listavar.DocumentoDigitado;
                        EDBateriaUsuario.ConfirmacionParticipacion = Listavar.ConfirmacionParticipacion;
                        EDBateriaUsuario.CheckPag9 = Listavar.CheckPag9;
                        EDBateriaUsuario.CheckPag10 = Listavar.CheckPag10;

                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 1 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 2 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 1 && Listavar.BateriaGestion.bateriaExtra == 3)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre + " y Cuestionario Extralaboral";
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 2 && Listavar.BateriaGestion.bateriaExtra == 3)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre + " y Cuestionario Extralaboral";
                        }
                        if (Listavar.BateriaGestion.Fk_Id_Bateria == 4 && Listavar.BateriaGestion.bateriaExtra == 0)
                        {
                            EDBateriaUsuario.NombreEncuesta = Listavar.BateriaGestion.Bateria.Nombre;
                        }


                    }
                    else
                    {
                        EDBateriaUsuario = null;
                    }
                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }

            return EDBateriaUsuario;
        }
        public bool EditarEstadoGestion(int Idgestion, int Estado)
        {
            bool probar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Pk_Id_BateriaGestion == Idgestion
                                select s).FirstOrDefault<BateriaGestion>();
                if (Listavar != null)
                {
                    if (Estado==3)
                    {
                        Listavar.FechaFinalizacion = DateTime.Today;
                    }
                    Listavar.Estado = Estado;
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return probar;
        }
        public bool EliminarGestion(int Idgestion, int IdEmpresa)
        {
            bool probar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Pk_Id_BateriaGestion == Idgestion && s.Fk_Id_Empresa== IdEmpresa
                                select s).FirstOrDefault<BateriaGestion>();
                if (Listavar != null)
                {
                    db.Entry(Listavar).State = EntityState.Deleted;
                    try
                    {
                        db.SaveChanges();
                        probar = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return probar;
        }
        public bool GestionConResultados(int Idgestion, int IdEmpresa)
        {
            bool probar = false;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaResultado
                                join d in db.Tbl_BateriaUsuario on s.Fk_Id_BateriaUsuario equals d.Pk_Id_BateriaUsuario
                                join e in db.Tbl_BateriaGestion on d.Fk_Id_BateriaGestion equals e.Pk_Id_BateriaGestion
                                where e.Pk_Id_BateriaGestion == Idgestion && e.Fk_Id_Empresa == IdEmpresa
                                select s).FirstOrDefault<BateriaResultado>();
                if (Listavar != null)
                {
                    probar = true;
                }
            }
            return probar;
        }
        public EDBateriaGestion ConsultarGestionKey(string key, int Form)
        {
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.TokenPublico == key
                                select s).FirstOrDefault<BateriaGestion>();
                if (Listavar != null)
                {
                    if (Listavar.Bateria.Pk_Id_Bateria == Form)
                    {
                        EDBateriaGestion.Pk_Id_BateriaGestion = Listavar.Pk_Id_BateriaGestion;
                        EDBateriaGestion.EstadoInt = Listavar.Estado;
                        EDBateriaGestion.NombreBateria = Listavar.Bateria.Nombre;
                        EDBateriaGestion.Fk_Id_Empresa = Listavar.Fk_Id_Empresa;
                        EDBateriaGestion.Fk_Id_Bateria = Listavar.Fk_Id_Bateria;
                        EDBateriaGestion.bateriaExtra = Listavar.bateriaExtra;
                    }
                    else
                    {
                        EDBateriaGestion = null;
                    }
                }
                else
                {
                    EDBateriaGestion = null;
                }
            }

            return EDBateriaGestion;
        }
        public EDBateriaGestion ConsultarGestionKey1(EDBateriaUsuario EDBateriaUsuario)
        {
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Pk_Id_BateriaGestion == EDBateriaUsuario.Fk_Id_BateriaGestion
                                select s).FirstOrDefault<BateriaGestion>();
                if (Listavar != null)
                {
                    EDBateriaGestion.Pk_Id_BateriaGestion = Listavar.Pk_Id_BateriaGestion;
                    EDBateriaGestion.EstadoInt = Listavar.Estado;
                    EDBateriaGestion.NombreBateria = Listavar.Bateria.Nombre;
                    EDBateriaGestion.Fk_Id_Empresa = Listavar.Fk_Id_Empresa;
                    EDBateriaGestion.Fk_Id_Bateria = Listavar.Fk_Id_Bateria;
                }
                else
                {
                    EDBateriaGestion = null;
                }
            }

            return EDBateriaGestion;
        }

        public bool ActualizarResultados(EDBateriaUsuario EDBateriaUsuario, int pkEmpresa)
        {
            bool guardar = false;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                join e in db.Tbl_BateriaGestion on s.Fk_Id_BateriaGestion equals e.Pk_Id_BateriaGestion
                                where s.Pk_Id_BateriaUsuario == EDBateriaUsuario.Pk_Id_BateriaUsuario && e.Fk_Id_Empresa== pkEmpresa
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    var Listavar1 = (from s in db.Tbl_BateriaUsuario
                                    where s.Pk_Id_BateriaUsuario == EDBateriaUsuario.Pk_Id_BateriaUsuario
                                    select s).FirstOrDefault<BateriaUsuario>();
                    if (Listavar1!=null)
                    {
                        Listavar1.Edad = EDBateriaUsuario.Edad;
                        Listavar1.NombreEvaluador = EDBateriaUsuario.NombreEvaluador;
                        Listavar1.IdEvaluador = EDBateriaUsuario.IdEvaluador;
                        Listavar1.Profesion = EDBateriaUsuario.Profesion;
                        Listavar1.Postgrado = EDBateriaUsuario.Postgrado;
                        Listavar1.TarjetaProfesional = EDBateriaUsuario.TarjetaProfesional;
                        Listavar1.Licencia = EDBateriaUsuario.Licencia;
                        Listavar1.Observaciones = EDBateriaUsuario.Observaciones;
                        Listavar1.Recomendaciones = EDBateriaUsuario.Recomendaciones;
                        Listavar1.FechaExpedicion = EDBateriaUsuario.FechaExpedicion;
                        db.Entry(Listavar1).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        guardar = true;
                    }
                }
            }

            return guardar;
        }


        //Encuesta Inicial
        public EDBateriaInicial ConsultarInicialKey(int Fk_IdUsuario)
        {
            EDBateriaInicial EDBateriaInicial = new EDBateriaInicial();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaInicial
                                where s.Fk_Id_BateriaUsuario == Fk_IdUsuario
                                select s).FirstOrDefault<BateriaInicial>();
                if (Listavar != null)
                {
                    EDBateriaInicial.Pk_Id_BateriaInicial = Listavar.Pk_Id_BateriaInicial;
                    EDBateriaInicial.Nombre = Listavar.Nombre;
                    EDBateriaInicial.Sexo = Listavar.Sexo;
                    EDBateriaInicial.AnioNacS = Listavar.AñoNac;

                    int año = 0;
                    if (int.TryParse(EDBateriaInicial.AnioNacS, out año))
                    {
                        EDBateriaInicial.AñoNac = año;
                    }
                    

                    EDBateriaInicial.EstadoCivil = Listavar.EstadoCivil;
                    EDBateriaInicial.NivEstudios = Listavar.NivEstudios;
                    EDBateriaInicial.Profesion = Listavar.Profesion;
                    EDBateriaInicial.ResidenciaMun = Listavar.ResidenciaMun;
                    EDBateriaInicial.ResidenciaDep = Listavar.ResidenciaDep;
                    EDBateriaInicial.Estrato = Listavar.Estrato;
                    EDBateriaInicial.TipoVivienda = Listavar.TipoVivienda;

                    EDBateriaInicial.PersonasDependenS = Listavar.PersonasDependen;
                    int personas = 0;
                    if (int.TryParse(EDBateriaInicial.PersonasDependenS, out personas))
                    {
                        EDBateriaInicial.PersonasDependen = personas;
                    }

                    

                    EDBateriaInicial.LugarTrabajoMun = Listavar.LugarTrabajoMun;
                    EDBateriaInicial.LugarTrabajoDep = Listavar.LugarTrabajoDep;
                    EDBateriaInicial.AñosConEmpresa = Listavar.AñosConEmpresa;
                    EDBateriaInicial.AñosConEmpresaNumS = Listavar.AñosConEmpresaNum;

                    int añoemp = 0;
                    if (int.TryParse(EDBateriaInicial.AñosConEmpresaNumS, out añoemp))
                    {
                        EDBateriaInicial.AñosConEmpresaNum = añoemp;
                    }


                    EDBateriaInicial.CargoConEmpresa = Listavar.CargoConEmpresa;


                    EDBateriaInicial.TipoCargo = Listavar.TipoCargo;
                    EDBateriaInicial.AñosOficio = Listavar.AñosOficio;
                    EDBateriaInicial.AñosOficioNumS = Listavar.AñosOficioNum;
                    int añoofi = 0;
                    if (int.TryParse(EDBateriaInicial.AñosOficioNumS, out añoofi))
                    {
                        EDBateriaInicial.AñosOficioNum = añoofi;
                    }


                    EDBateriaInicial.AreaEmpresa = Listavar.AreaEmpresa;
                    EDBateriaInicial.TipoContrato = Listavar.TipoContrato;
                    EDBateriaInicial.HorasEstablecidasS = Listavar.HorasEstablecidas;
                    int horas = 0;
                    if (int.TryParse(EDBateriaInicial.HorasEstablecidasS, out horas))
                    {
                        EDBateriaInicial.HorasEstablecidas = horas;
                    }
                    EDBateriaInicial.TipoSalario = Listavar.TipoSalario;
                    EDBateriaInicial.Fk_Id_BateriaUsuario = Listavar.Fk_Id_BateriaUsuario;
                }
                else
                {
                    EDBateriaInicial = null;
                }
            }

            return EDBateriaInicial;
        }
        public bool[] GuardarInicial(EDBateriaInicial EDBateriaInicial)
        {
            bool[] ProbarGuardar = new bool[5] {false,false,false,false, false };
            string[] Pagina1 = new string[4];
            string[] Pagina2 = new string[7];
            string[] Pagina3 = new string[9];
            string[] Pagina4 = new string[3];


            if (EDBateriaInicial!=null)
            {
                BateriaInicial BateriaInicial = new BateriaInicial();

                BateriaInicial.Nombre = EDBateriaInicial.Nombre;
                BateriaInicial.Sexo = EDBateriaInicial.Sexo;
                BateriaInicial.AñoNac = EDBateriaInicial.AñoNac.ToString();
                BateriaInicial.EstadoCivil = EDBateriaInicial.EstadoCivil;

                Pagina1[0] = EDBateriaInicial.Nombre;
                Pagina1[1] = EDBateriaInicial.Sexo;
                Pagina1[2] = EDBateriaInicial.AñoNac.ToString();
                Pagina1[3] = EDBateriaInicial.EstadoCivil;

                int cont = 0;
                foreach (var item in Pagina1)
                {
                    if (item!=null)
                    {
                        cont++;
                    }
                }
                if (cont==4)
                {
                    ProbarGuardar[1] = true;
                }

                BateriaInicial.NivEstudios = EDBateriaInicial.NivEstudios;
                BateriaInicial.Profesion = EDBateriaInicial.Profesion;
                BateriaInicial.ResidenciaMun = EDBateriaInicial.ResidenciaMun;
                BateriaInicial.ResidenciaDep = EDBateriaInicial.ResidenciaDep;
                BateriaInicial.Estrato = EDBateriaInicial.Estrato;
                BateriaInicial.TipoVivienda = EDBateriaInicial.TipoVivienda;
                BateriaInicial.PersonasDependen = EDBateriaInicial.PersonasDependen.ToString();

                Pagina2[0] = EDBateriaInicial.NivEstudios;
                Pagina2[1] = EDBateriaInicial.Profesion;
                Pagina2[2] = EDBateriaInicial.ResidenciaMun;
                Pagina2[3] = EDBateriaInicial.ResidenciaDep;
                Pagina2[4] = EDBateriaInicial.Estrato;
                Pagina2[5] = EDBateriaInicial.TipoVivienda;
                Pagina2[6] = EDBateriaInicial.PersonasDependen.ToString();

                cont = 0;
                foreach (var item in Pagina2)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont == 7)
                {
                    ProbarGuardar[2] = true;
                }

                BateriaInicial.LugarTrabajoMun = EDBateriaInicial.LugarTrabajoMun;
                BateriaInicial.LugarTrabajoDep = EDBateriaInicial.LugarTrabajoDep;
                BateriaInicial.AñosConEmpresa = EDBateriaInicial.AñosConEmpresa;
                BateriaInicial.AñosConEmpresaNum = EDBateriaInicial.AñosConEmpresaNum.ToString();
                BateriaInicial.CargoConEmpresa = EDBateriaInicial.CargoConEmpresa;
                BateriaInicial.TipoCargo = EDBateriaInicial.TipoCargo;
                BateriaInicial.AñosOficio = EDBateriaInicial.AñosOficio;
                BateriaInicial.AñosOficioNum = EDBateriaInicial.AñosOficioNum.ToString();
                BateriaInicial.AreaEmpresa = EDBateriaInicial.AreaEmpresa;

                Pagina3[0] = EDBateriaInicial.LugarTrabajoMun;
                Pagina3[1] = EDBateriaInicial.LugarTrabajoDep;
                Pagina3[2] = EDBateriaInicial.AñosConEmpresa;
                Pagina3[3] = EDBateriaInicial.AñosConEmpresaNum.ToString();
                Pagina3[4] = EDBateriaInicial.CargoConEmpresa;
                Pagina3[5] = EDBateriaInicial.TipoCargo;
                Pagina3[6] = EDBateriaInicial.AñosOficio;
                Pagina3[7] = EDBateriaInicial.AñosOficioNum.ToString();
                Pagina3[8] = EDBateriaInicial.AreaEmpresa;

                int cont1 = 0;
                cont = 0;
                foreach (var item in Pagina3)
                {
                    if (cont1 >= 0 && cont1 <= 2)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 3)
                    {
                        if (Pagina3[2] != null)
                        {
                            if (Pagina3[2] != "Si lleva menos de un año marque esta opción")
                            {
                                if (item != null)
                                {
                                    cont++;
                                }
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    if (cont1 >= 4 && cont1 <= 6)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 7)
                    {
                        if (Pagina3[6] != null)
                        {
                            if (Pagina3[6] != "Si lleva menos de un año marque esta opción")
                            {
                                if (item != null)
                                {
                                    cont++;
                                }
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 8)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    cont1++;
                }
                if (cont == 9)
                {
                    ProbarGuardar[3] = true;
                }

                BateriaInicial.TipoContrato = EDBateriaInicial.TipoContrato;
                BateriaInicial.HorasEstablecidas = EDBateriaInicial.HorasEstablecidas.ToString();
                BateriaInicial.TipoSalario = EDBateriaInicial.TipoSalario;

                Pagina4[0] = EDBateriaInicial.TipoContrato;
                Pagina4[1] = EDBateriaInicial.HorasEstablecidas.ToString();
                Pagina4[2] = EDBateriaInicial.TipoSalario;

                cont = 0;
                foreach (var item in Pagina4)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont == 3)
                {
                    ProbarGuardar[4] = true;
                }

                BateriaInicial.Fk_Id_BateriaUsuario = EDBateriaInicial.Fk_Id_BateriaUsuario;

                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_BateriaInicial.Add(BateriaInicial);
                    try
                    {
                        db.SaveChanges();
                        ProbarGuardar[0] = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return ProbarGuardar;
        }
        public bool[] ActualizarInicial(EDBateriaInicial EDBateriaInicial)
        {
            bool[] ProbarGuardar = new bool[5] { false, false, false, false, false };
            string[] Pagina1 = new string[4];
            string[] Pagina2 = new string[7];
            string[] Pagina3 = new string[9];
            string[] Pagina4 = new string[3];

            EDBateriaInicial EDBateriaInicialC = ConsultarInicialKey(EDBateriaInicial.Fk_Id_BateriaUsuario);


            if (EDBateriaInicial != null)
            {
                BateriaInicial BateriaInicial = new BateriaInicial();

                BateriaInicial.Pk_Id_BateriaInicial = EDBateriaInicialC.Pk_Id_BateriaInicial;

                BateriaInicial.Nombre = EDBateriaInicial.Nombre;
                BateriaInicial.Sexo = EDBateriaInicial.Sexo;
                BateriaInicial.AñoNac = EDBateriaInicial.AnioNacS;
                BateriaInicial.EstadoCivil = EDBateriaInicial.EstadoCivil;

                Pagina1[0] = EDBateriaInicial.Nombre;
                Pagina1[1] = EDBateriaInicial.Sexo;
                Pagina1[2] = EDBateriaInicial.AnioNacS;
                Pagina1[3] = EDBateriaInicial.EstadoCivil;

                int cont = 0;
                int cont1 = 0;
                foreach (var item in Pagina1)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                    cont1++;
                }
                if (cont == 4)
                {
                    ProbarGuardar[1] = true;
                }

                BateriaInicial.NivEstudios = EDBateriaInicial.NivEstudios;
                BateriaInicial.Profesion = EDBateriaInicial.Profesion;
                BateriaInicial.ResidenciaMun = EDBateriaInicial.ResidenciaMun;
                BateriaInicial.ResidenciaDep = EDBateriaInicial.ResidenciaDep;
                BateriaInicial.Estrato = EDBateriaInicial.Estrato;
                BateriaInicial.TipoVivienda = EDBateriaInicial.TipoVivienda;
                BateriaInicial.PersonasDependen = EDBateriaInicial.PersonasDependenS;

                Pagina2[0] = EDBateriaInicial.NivEstudios;
                Pagina2[1] = EDBateriaInicial.Profesion;
                Pagina2[2] = EDBateriaInicial.ResidenciaMun;
                Pagina2[3] = EDBateriaInicial.ResidenciaDep;
                Pagina2[4] = EDBateriaInicial.Estrato;
                Pagina2[5] = EDBateriaInicial.TipoVivienda;
                Pagina2[6] = EDBateriaInicial.PersonasDependenS;

                cont = 0;
                foreach (var item in Pagina2)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont == 7)
                {
                    ProbarGuardar[2] = true;
                }

                BateriaInicial.LugarTrabajoMun = EDBateriaInicial.LugarTrabajoMun;
                BateriaInicial.LugarTrabajoDep = EDBateriaInicial.LugarTrabajoDep;
                BateriaInicial.AñosConEmpresa = EDBateriaInicial.AñosConEmpresa;
                BateriaInicial.AñosConEmpresaNum = EDBateriaInicial.AñosConEmpresaNumS;
                BateriaInicial.CargoConEmpresa = EDBateriaInicial.CargoConEmpresa;
                BateriaInicial.TipoCargo = EDBateriaInicial.TipoCargo;
                BateriaInicial.AñosOficio = EDBateriaInicial.AñosOficio;
                BateriaInicial.AñosOficioNum = EDBateriaInicial.AñosOficioNumS;
                BateriaInicial.AreaEmpresa = EDBateriaInicial.AreaEmpresa;

                Pagina3[0] = EDBateriaInicial.LugarTrabajoMun;
                Pagina3[1] = EDBateriaInicial.LugarTrabajoDep;
                Pagina3[2] = EDBateriaInicial.AñosConEmpresa;
                Pagina3[3] = EDBateriaInicial.AñosConEmpresaNumS;
                Pagina3[4] = EDBateriaInicial.CargoConEmpresa;
                Pagina3[5] = EDBateriaInicial.TipoCargo;
                Pagina3[6] = EDBateriaInicial.AñosOficio;
                Pagina3[7] = EDBateriaInicial.AñosOficioNumS;
                Pagina3[8] = EDBateriaInicial.AreaEmpresa;

                cont1 = 0;
                cont = 0;
                foreach (var item in Pagina3)
                {
                    if (cont1>=0 && cont1<=2)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 3)
                    {
                        if (Pagina3[2] != null)
                        {
                            if (Pagina3[2] != "Si lleva menos de un año marque esta opción")
                            {
                                if (item != null)
                                {
                                    cont++;
                                }
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    if (cont1 >= 4 && cont1 <= 6)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 7)
                    {
                        if (Pagina3[6] != null)
                        {
                            if (Pagina3[6] != "Si lleva menos de un año marque esta opción")
                            {
                                if (item != null)
                                {
                                    cont++;
                                }
                            }
                            else
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    if (cont1 == 8)
                    {
                        if (item != null)
                        {
                            cont++;
                        }
                    }
                    cont1++;
                }
                if (cont == 9)
                {
                    ProbarGuardar[3] = true;
                }

                BateriaInicial.TipoContrato = EDBateriaInicial.TipoContrato;
                BateriaInicial.HorasEstablecidas = EDBateriaInicial.HorasEstablecidasS;
                BateriaInicial.TipoSalario = EDBateriaInicial.TipoSalario;

                Pagina4[0] = EDBateriaInicial.TipoContrato;
                Pagina4[1] = EDBateriaInicial.HorasEstablecidasS;
                Pagina4[2] = EDBateriaInicial.TipoSalario;

                cont = 0;
                foreach (var item in Pagina4)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont == 3)
                {
                    ProbarGuardar[4] = true;
                }

                BateriaInicial.Fk_Id_BateriaUsuario = EDBateriaInicial.Fk_Id_BateriaUsuario;

                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(BateriaInicial).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        ProbarGuardar[0] = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return ProbarGuardar;
        }
        public int NumeroPagina(EDBateriaInicial EDBateriaInicial)
        {
            int pagina = 1;


            bool[] ProbarGuardar = new bool[5] { false, false, false, false, false };


            string[] Pagina1 = new string[4];
            string[] Pagina2 = new string[7];
            string[] Pagina3 = new string[9];
            string[] Pagina4 = new string[3];

            Pagina1[0] = EDBateriaInicial.Nombre;
            Pagina1[1] = EDBateriaInicial.Sexo;
            Pagina1[2] = EDBateriaInicial.AnioNacS;
            Pagina1[3] = EDBateriaInicial.EstadoCivil;

            Pagina2[0] = EDBateriaInicial.NivEstudios;
            Pagina2[1] = EDBateriaInicial.Profesion;
            Pagina2[2] = EDBateriaInicial.ResidenciaMun;
            Pagina2[3] = EDBateriaInicial.ResidenciaDep;
            Pagina2[4] = EDBateriaInicial.Estrato;
            Pagina2[5] = EDBateriaInicial.TipoVivienda;
            Pagina2[6] = EDBateriaInicial.PersonasDependenS;

            Pagina3[0] = EDBateriaInicial.LugarTrabajoMun;
            Pagina3[1] = EDBateriaInicial.LugarTrabajoDep;
            Pagina3[2] = EDBateriaInicial.AñosConEmpresa;
            Pagina3[3] = EDBateriaInicial.AñosConEmpresaNumS;
            Pagina3[4] = EDBateriaInicial.CargoConEmpresa;
            Pagina3[5] = EDBateriaInicial.TipoCargo;
            Pagina3[6] = EDBateriaInicial.AñosOficio;
            Pagina3[7] = EDBateriaInicial.AñosOficioNumS;
            Pagina3[8] = EDBateriaInicial.AreaEmpresa;

            Pagina4[0] = EDBateriaInicial.TipoContrato;
            Pagina4[1] = EDBateriaInicial.HorasEstablecidasS;
            Pagina4[2] = EDBateriaInicial.TipoSalario;


            int cont = 0;
            int cont1 = 0;
            foreach (var item in Pagina1)
            {
                if (cont1 != 2)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                else
                {
                    if (item != "0")
                    {
                        cont++;
                    }
                }
                cont1++;
            }
            if (cont == 4)
            {
                ProbarGuardar[1] = true;
            }

            cont = 0;
            foreach (var item in Pagina2)
            {
                if (item != null)
                {
                    cont++;
                }
            }
            if (cont == 7)
            {
                ProbarGuardar[2] = true;
            }
            cont1 = 0;
            cont = 0;
            foreach (var item in Pagina3)
            {
                if (cont1 >= 0 && cont1 <= 2)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont1 == 3)
                {
                    if (Pagina3[2] != null)
                    {
                        if (Pagina3[2] != "Si lleva menos de un año marque esta opción")
                        {
                            if (item != null)
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    else
                    {
                        cont++;
                    }
                }
                if (cont1 >= 4 && cont1 <= 6)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                if (cont1 == 7)
                {
                    if (Pagina3[6] != null)
                    {
                        if (Pagina3[6] != "Si lleva menos de un año marque esta opción")
                        {
                            if (item != null)
                            {
                                cont++;
                            }
                        }
                        else
                        {
                            cont++;
                        }
                    }
                    else
                    {
                        cont++;
                    }
                }
                if (cont1 == 8)
                {
                    if (item != null)
                    {
                        cont++;
                    }
                }
                cont1++;
            }
            if (cont == 9)
            {
                ProbarGuardar[3] = true;
            }


            cont = 0;
            foreach (var item in Pagina4)
            {
                if (item != null)
                {
                    cont++;
                }
            }
            if (cont == 3)
            {
                ProbarGuardar[4] = true;
            }


            if (ProbarGuardar[1])
            {
                if (ProbarGuardar[2])
                {
                    if (ProbarGuardar[3])
                    {
                        if (ProbarGuardar[4])
                        {
                            pagina = 5;
                        }
                        else
                        {
                            pagina = 4;
                        }
                    }
                    else
                    {
                        pagina = 3;
                    }
                }
                else
                {
                    pagina = 2;
                }
            }
            else
            {
                pagina = 1;
            }


            return pagina;
        }
        public bool EncuestaCompleta(EDBateriaUsuario EDBateriaUsuario)
        {
            bool probar = false;
            if (EDBateriaUsuario.RegistroOperacion=="Fin" )
            {
                probar = true;
            }
            if (EDBateriaUsuario.RegistroOperacion == "Rechazo")
            {
                probar = true;
            }
            
            return probar;
        }
        public bool EncuestaCompletaExtra(EDBateriaUsuario EDBateriaUsuario)
        {
            bool probar = false;
            if (EDBateriaUsuario.RegistroOperacion == "Fin")
            {
                probar = true;
            }
            if (EDBateriaUsuario.RegistroOperacion == "Rechazo")
            {
                probar = true;
            }

            return probar;
        }
        public bool TieneExtra(EDBateriaUsuario EDBateriaUsuario)
        {
            bool probar = false;
            EDBateriaGestion EDBateriaGestion = new EDBateriaGestion();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == EDBateriaUsuario.Pk_Id_BateriaUsuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    if (Listavar.BateriaGestion.bateriaExtra==3 && Listavar.RegistroOperacion=="Fin")
                    {
                        probar = true;
                    }
                }
            }
            return probar;
        }
        public string PlantillaCorreo(string nombre)
        {
            string Plantilla = string.Empty;
            using (var db = new SG_SSTContext())
            {
                var resultado = db.Tbl_PlantillasCorreosSistema.Where(pc => pc.NombrePlantilla == nombre).Select(pc => new EDParametroSistema()
                {
                    IdParametro = pc.IdPlantilla,
                    NombreParametro = pc.NombrePlantilla,
                    Valor = pc.Plantilla
                }).FirstOrDefault();
                if (resultado!=null)
                {
                    Plantilla = resultado.Valor;
                }
            }
            return Plantilla;
        }
        public void EditarEstadoCorreo(int pkusuario, string plantilla)
        {
            DateTime now = DateTime.Now;
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                where s.Pk_Id_BateriaUsuario == pkusuario
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    Listavar.MailBody = plantilla;
                    Listavar.EstadoEnvio = 1;
                    Listavar.FechaEnvio = now;
                    db.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        public void CambiarEstadosInactivos()
        {
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaGestion
                                where s.Estado == 4
                                select s).ToList<BateriaGestion>();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        item.Estado = 1;
                        db.Entry(item).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }


        public List<EDRelacionesLaborales> ConsultarConvocadosRol(int rol)
        {
            List<EDRelacionesLaborales> ListaRelLab = new List<EDRelacionesLaborales>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Empleado_Tematica
                                join d in db.Tbl_Empleado_Por_Tematica on s.Pk_Id_EmpleadoTematica equals d.Fk_Id_Tematica
                                join e in db.Tbl_Rol on d.Fk_Id_Rol equals e.Pk_Id_Rol
                                where e.Pk_Id_Rol== rol
                                select s).ToList<EmpleadoTematica>().Distinct();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDRelacionesLaborales EDRelacionesLaborales = new EDRelacionesLaborales();
                        EDRelacionesLaborales.Nombre1 = item.Nombre_Empleado;
                        EDRelacionesLaborales.Apellido1 = item.Apellidos_Empleado;
                        EDRelacionesLaborales.NumeroDocumento = item.Numero_Documento.ToString();
                        EDRelacionesLaborales.Email = item.Email_Persona;
                        ListaRelLab.Add(EDRelacionesLaborales);
                    }
                }
            }
            return ListaRelLab;
        }

        public bool EmpresaCoincide(string nitempresa, int fkidempresa)
        {
            bool probar = false;

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Empresa
                                where s.Nit_Empresa == nitempresa && s.Pk_Id_Empresa== fkidempresa
                                select s).FirstOrDefault<Empresa>();
                if (Listavar != null)
                {
                    probar = true;
                }
            }
            return probar;
        }

        public EDBateriaUsuario ConsultarConvocadoCedula(string cedula, int pkIdgestion)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                join d in db.Tbl_BateriaGestion on s.Fk_Id_BateriaGestion equals d.Pk_Id_BateriaGestion
                                where s.NumeroIdentificacion == cedula && s.CorreoElectronico==null && d.Pk_Id_BateriaGestion== pkIdgestion
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                        EDBateriaUsuario.Nombre = Listavar.Nombre;
                        EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                        EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                        EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                        EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                        EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                        EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                        EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                        EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                        EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                        EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                        EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                        EDBateriaUsuario.MailBody = Listavar.MailBody;
                        EDBateriaUsuario.DocumentoDigitado = Listavar.DocumentoDigitado;
                        EDBateriaUsuario.ConfirmacionParticipacion = Listavar.ConfirmacionParticipacion;
                        EDBateriaUsuario.CheckPag9 = Listavar.CheckPag9;
                        EDBateriaUsuario.CheckPag10 = Listavar.CheckPag10;
                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }
            return EDBateriaUsuario;
        }


        public EDBateriaUsuario ConsultarConvocadoId(int PkIdUsuario, int FkEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                join d in db.Tbl_BateriaGestion on s.Fk_Id_BateriaGestion equals d.Pk_Id_BateriaGestion
                                where s.Pk_Id_BateriaUsuario == PkIdUsuario && d.Fk_Id_Empresa == FkEmpresa
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    EDBateriaUsuario.Nombre = Listavar.Nombre;
                    EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                    EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                    EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                    EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                    EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                    EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                    EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                    EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                    EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                    EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                    EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                    EDBateriaUsuario.MailBody = Listavar.MailBody;
                    EDBateriaUsuario.DocumentoDigitado = Listavar.DocumentoDigitado;
                    EDBateriaUsuario.ConfirmacionParticipacion = Listavar.ConfirmacionParticipacion;
                    EDBateriaUsuario.CheckPag9 = Listavar.CheckPag9;
                    EDBateriaUsuario.CheckPag10 = Listavar.CheckPag10;

                    EDBateriaUsuario.Edad = Listavar.Edad;
                    EDBateriaUsuario.NombreEvaluador = Listavar.NombreEvaluador;
                    EDBateriaUsuario.IdEvaluador = Listavar.IdEvaluador;
                    EDBateriaUsuario.Profesion = Listavar.Profesion;
                    EDBateriaUsuario.Postgrado = Listavar.Postgrado;
                    EDBateriaUsuario.TarjetaProfesional = Listavar.TarjetaProfesional;
                    EDBateriaUsuario.Licencia = Listavar.Licencia;
                    EDBateriaUsuario.Observaciones = Listavar.Observaciones;
                    EDBateriaUsuario.Recomendaciones = Listavar.Recomendaciones;
                    EDBateriaUsuario.FechaExpedicion = Listavar.FechaExpedicion;

                    DateTime fe = Listavar.FechaEnvio ?? DateTime.MinValue;
                    if (fe != DateTime.MinValue)
                    {
                        EDBateriaUsuario.FechaEnvio = fe;

                    }
                    DateTime fr = Listavar.FechaRespuesta ?? DateTime.MinValue;
                    if (fr != DateTime.MinValue)
                    {
                        EDBateriaUsuario.FechaRespuesta = fr;
                    }
                    
                    
                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }

            if (EDBateriaUsuario!=null)
            {
                EDBateriaUsuario.BateriaInicial = ConsultarInicialKey(PkIdUsuario);
                EDBateriaUsuario.ListaResultados = ListaResultados(PkIdUsuario);
            }

            return EDBateriaUsuario;
        }

        public EDBateriaUsuario ConsultarConvocadoId1(int PkIdUsuario, int FkEmpresa)
        {
            EDBateriaUsuario EDBateriaUsuario = new EDBateriaUsuario();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaUsuario
                                join d in db.Tbl_BateriaGestion on s.Fk_Id_BateriaGestion equals d.Pk_Id_BateriaGestion
                                where s.Pk_Id_BateriaUsuario == PkIdUsuario && d.Fk_Id_Empresa == FkEmpresa
                                select s).FirstOrDefault<BateriaUsuario>();
                if (Listavar != null)
                {
                    EDBateriaUsuario.Nombre = Listavar.Nombre;
                    EDBateriaUsuario.NumeroIdentificacion = Listavar.NumeroIdentificacion;
                    EDBateriaUsuario.TipoDocumento = Listavar.TipoDocumento;
                    EDBateriaUsuario.CorreoElectronico = Listavar.CorreoElectronico;
                    EDBateriaUsuario.Id_Empresa = Listavar.Id_Empresa;
                    EDBateriaUsuario.TokenPrivado = Listavar.TokenPrivado;
                    EDBateriaUsuario.Fk_Id_BateriaGestion = Listavar.Fk_Id_BateriaGestion;
                    EDBateriaUsuario.TipoConv = Listavar.TipoConv;
                    EDBateriaUsuario.Pk_Id_BateriaUsuario = Listavar.Pk_Id_BateriaUsuario;
                    EDBateriaUsuario.EstadoEnvio = Listavar.EstadoEnvio;
                    EDBateriaUsuario.NumeroIntentos = Listavar.NumeroIntentos;
                    EDBateriaUsuario.RegistroOperacion = Listavar.RegistroOperacion;
                    EDBateriaUsuario.MailBody = Listavar.MailBody;
                    EDBateriaUsuario.DocumentoDigitado = Listavar.DocumentoDigitado;
                    EDBateriaUsuario.ConfirmacionParticipacion = Listavar.ConfirmacionParticipacion;
                    EDBateriaUsuario.CheckPag9 = Listavar.CheckPag9;
                    EDBateriaUsuario.CheckPag10 = Listavar.CheckPag10;

                    EDBateriaUsuario.Edad = Listavar.Edad;
                    EDBateriaUsuario.NombreEvaluador = Listavar.NombreEvaluador;
                    EDBateriaUsuario.IdEvaluador = Listavar.IdEvaluador;
                    EDBateriaUsuario.Profesion = Listavar.Profesion;
                    EDBateriaUsuario.Postgrado = Listavar.Postgrado;
                    EDBateriaUsuario.TarjetaProfesional = Listavar.TarjetaProfesional;
                    EDBateriaUsuario.Licencia = Listavar.Licencia;
                    EDBateriaUsuario.Observaciones = Listavar.Observaciones;
                    EDBateriaUsuario.Recomendaciones = Listavar.Recomendaciones;
                    EDBateriaUsuario.FechaExpedicion = Listavar.FechaExpedicion;

                    DateTime fe = Listavar.FechaEnvio ?? DateTime.MinValue;
                    if (fe != DateTime.MinValue)
                    {
                        EDBateriaUsuario.FechaEnvio = fe;

                    }
                    DateTime fr = Listavar.FechaRespuesta ?? DateTime.MinValue;
                    if (fr != DateTime.MinValue)
                    {
                        EDBateriaUsuario.FechaRespuesta = fr;
                    }


                }
                else
                {
                    EDBateriaUsuario = null;
                }
            }

            if (EDBateriaUsuario != null)
            {
                EDBateriaUsuario.BateriaInicial = ConsultarInicialKey(PkIdUsuario);
                EDBateriaUsuario.ListaResultados = ListaResultados1(PkIdUsuario);
            }

            return EDBateriaUsuario;
        }

        public List<EDBateriaResultado> ListaResultados(int fkUsuario)
        {
            List<EDBateriaResultado> ListaResultados = new List<EDBateriaResultado>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaResultado
                                join d in db.Tbl_BateriaUsuario on s.Fk_Id_BateriaUsuario equals d.Pk_Id_BateriaUsuario
                                where d.Pk_Id_BateriaUsuario== fkUsuario
                                select s).ToList<BateriaResultado>().Distinct();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Pk_Id_BateriaResultado = item.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = item.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = item.Fk_Id_BateriaCuestionario;
                        EDBateriaResultado.Valor = item.Valor;
                        EDBateriaResultado.Dimension = item.BateriaCuestionario.BateriaDimension.Nombre;
                        EDBateriaResultado.DimensionInt = item.BateriaCuestionario.BateriaDimension.Pk_Id_BateriaDimension;
                        EDBateriaResultado.DominioInt = item.BateriaCuestionario.Dominio;
                        EDBateriaResultado.Dominio = NombreDominio(item.BateriaCuestionario.Dominio, item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria);
                        EDBateriaResultado.ValorResultado = ValorReal(item.Valor, item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria, item.BateriaCuestionario.Orden);
                        if (item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria==1 && item.BateriaUsuario.NumeroIntentos==1)
                        {
                            EDBateriaResultado.ValorResultado = ValorReal(item.Valor, 3, item.BateriaCuestionario.Orden);
                        }
                        if (item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria == 2 && item.BateriaUsuario.NumeroIntentos == 1)
                        {
                            EDBateriaResultado.ValorResultado = ValorReal(item.Valor, 3, item.BateriaCuestionario.Orden);
                        }
                        
                        ListaResultados.Add(EDBateriaResultado);
                    }
                }
            }
            return ListaResultados;
        }
        public List<EDBateriaResultado> ListaResultados1(int fkUsuario)
        {
            List<EDBateriaResultado> ListaResultados = new List<EDBateriaResultado>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaResultado
                                join d in db.Tbl_BateriaUsuario on s.Fk_Id_BateriaUsuario equals d.Pk_Id_BateriaUsuario
                                where d.Pk_Id_BateriaUsuario == fkUsuario
                                select s).ToList<BateriaResultado>().Distinct().OrderBy(s=>s.BateriaCuestionario.Orden);
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        EDBateriaResultado EDBateriaResultado = new EDBateriaResultado();
                        EDBateriaResultado.Pk_Id_BateriaResultado = item.Pk_Id_BateriaResultado;
                        EDBateriaResultado.Fk_Id_BateriaUsuario = item.Fk_Id_BateriaUsuario;
                        EDBateriaResultado.Fk_Id_BateriaCuestionario = item.Fk_Id_BateriaCuestionario;
                        EDBateriaResultado.Valor = item.Valor;
                        EDBateriaResultado.Dimension = item.BateriaCuestionario.BateriaDimension.Nombre;
                        EDBateriaResultado.DimensionInt = item.BateriaCuestionario.BateriaDimension.Pk_Id_BateriaDimension;
                        EDBateriaResultado.DominioInt = item.BateriaCuestionario.Dominio;
                        EDBateriaResultado.Dominio = NombreDominio(item.BateriaCuestionario.Dominio, item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria);
                        EDBateriaResultado.ValorResultado = ValorReal(item.Valor, item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria, item.BateriaCuestionario.Orden);
                        if (item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria == 1 && item.BateriaUsuario.NumeroIntentos == 1)
                        {
                            EDBateriaResultado.ValorResultado = ValorReal(item.Valor, 3, item.BateriaCuestionario.Orden);
                        }
                        if (item.BateriaUsuario.BateriaGestion.Bateria.Pk_Id_Bateria == 2 && item.BateriaUsuario.NumeroIntentos == 1)
                        {
                            EDBateriaResultado.ValorResultado = ValorReal(item.Valor, 3, item.BateriaCuestionario.Orden);
                        }

                        ListaResultados.Add(EDBateriaResultado);
                    }
                }
            }
            return ListaResultados;
        }
        private string NombreDominio(int IdDominio, int IdBateria)
        {
            string Nombre = "";

            List<EDBateriaDimension> ListaDominiosA = new List<EDBateriaDimension>();
            List<EDBateriaDimension> ListaDominiosB = new List<EDBateriaDimension>();
            List<EDBateriaDimension> ListaDominiosC = new List<EDBateriaDimension>();
            List<EDBateriaDimension> ListaDominiosD = new List<EDBateriaDimension>();

            if (IdBateria==1)
            {
                if (IdDominio==1)
                {
                    Nombre = "Liderazgo y relaciones sociales en el trabajo";
                }
                if (IdDominio == 2)
                {
                    Nombre = "Control sobre el trabajo";
                }
                if (IdDominio == 3)
                {
                    Nombre = "Demandas del trabajo";
                }
                if (IdDominio == 4)
                {
                    Nombre = "Recompensas";
                }
            }
            if (IdBateria == 2)
            {
                if (IdDominio == 1)
                {
                    Nombre = "Liderazgo y relaciones sociales en el trabajo";
                }
                if (IdDominio == 2)
                {
                    Nombre = "Control sobre el trabajo";
                }
                if (IdDominio == 3)
                {
                    Nombre = "Demandas del trabajo";
                }
                if (IdDominio == 4)
                {
                    Nombre = "Recompensas";
                }
            }
            if (IdBateria == 3)
            {
                if (IdDominio == 1)
                {
                    Nombre = "Unico";
                }
            }
            if (IdBateria == 4)
            {
                if (IdDominio == 1)
                {
                    Nombre = "Unico";
                }
            }
            return Nombre;
        }
        private int ValorReal(int Valor, int IdBateria, int pregunta)
        {
            int ValorReal = -1;

            if (IdBateria==1)
            {
                int[] Ascendente = new int[73] { 4, 5, 6, 9, 12, 14, 32, 34, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105 };
                int[] Descendente = new int[50] { 1, 2, 3, 7, 8, 10, 11, 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 33, 35, 36, 37, 38, 52, 80, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123 };
                int tipo = 0;
                foreach (var item in Ascendente)
                {
                    if (pregunta== item)
                    {
                        tipo = 1;
                    }
                }
                foreach (var item in Descendente)
                {
                    if (pregunta == item)
                    {
                        tipo = 2;
                    }
                }
                if (tipo==1)
                {
                    if (Valor==1)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                }
                if (tipo == 2)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                }
            }
            if (IdBateria == 2)
            {
                int[] Ascendente = new int[68] { 4, 5, 6, 9, 12, 14, 22, 24, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 97 };
                int[] Descendente = new int[29] { 1, 2, 3, 7, 8, 10, 11, 13, 15, 16, 17, 18, 19, 20, 21, 23, 25, 26, 27, 28, 66, 89, 90, 91, 92, 93, 94, 95, 96 };
                int tipo = 0;
                foreach (var item in Ascendente)
                {
                    if (pregunta == item)
                    {
                        tipo = 1;
                    }
                }
                foreach (var item in Descendente)
                {
                    if (pregunta == item)
                    {
                        tipo = 2;
                    }
                }
                if (tipo == 1)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                }
                if (tipo == 2)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                }
            }
            if (IdBateria == 3)
            {
                int[] Ascendente = new int[23] { 1, 4, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 27, 29 };
                int[] Descendente = new int[8] { 2, 3, 6, 24, 26, 28, 30, 31 };
                int tipo = 0;
                foreach (var item in Ascendente)
                {
                    if (pregunta == item)
                    {
                        tipo = 1;
                    }
                }
                foreach (var item in Descendente)
                {
                    if (pregunta == item)
                    {
                        tipo = 2;
                    }
                }
                if (tipo == 1)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                }
                if (tipo == 2)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 5)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }
                }
            }
            if (IdBateria == 4)
            {
                int[] Tipo1 = new int[9] { 1, 2, 3, 9, 13, 14, 15, 23 , 24 };
                int[] Tipo2 = new int[13] { 4, 5, 6, 10, 11, 16, 17, 18, 19, 25, 26, 27, 28 };
                int[] Tipo3 = new int[9] { 7, 8, 12, 20, 21, 22, 29, 30 , 31 };
                int tipo = 0;
                foreach (var item in Tipo1)
                {
                    if (pregunta == item)
                    {
                        tipo = 1;
                    }
                }
                foreach (var item in Tipo2)
                {
                    if (pregunta == item)
                    {
                        tipo = 2;
                    }
                }
                foreach (var item in Tipo3)
                {
                    if (pregunta == item)
                    {
                        tipo = 3;
                    }
                }
                if (tipo == 1)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 9;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 6;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }

                }
                if (tipo == 2)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 6;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 4;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }

                }
                if (tipo == 3)
                {
                    if (Valor == 1)
                    {
                        ValorReal = 3;
                        return ValorReal;
                    }
                    if (Valor == 2)
                    {
                        ValorReal = 2;
                        return ValorReal;
                    }
                    if (Valor == 3)
                    {
                        ValorReal = 1;
                        return ValorReal;
                    }
                    if (Valor == 4)
                    {
                        ValorReal = 0;
                        return ValorReal;
                    }

                }
            }
            return ValorReal;
        }


        public List<EDBateriaDimension> ListaDimensiones(int Iddominio, int bateria)
        {
            List<EDBateriaDimension> ListaDimensiones = new List<EDBateriaDimension>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_BateriaDimension
                                join e in db.Tbl_BateriaCuestionario on s.Pk_Id_BateriaDimension equals e.Fk_Id_BateriaDimension
                                where e.Dominio == Iddominio
                                select s).ToList<BateriaDimension>().Distinct();
                if (Listavar != null)
                {
                    foreach (var item in Listavar)
                    {
                        if (item.Bateria.Pk_Id_Bateria== bateria)
                        {
                            EDBateriaDimension EDBateriaDimension = new EDBateriaDimension();
                            EDBateriaDimension.Nombre = item.Nombre;
                            EDBateriaDimension.Pk_Id_BateriaDimension = item.Pk_Id_BateriaDimension;
                            EDBateriaDimension.FactorTransformacion = item.FactorTransformacion;
                            ListaDimensiones.Add(EDBateriaDimension);
                        }
                        
                    }
                }
            }
            return ListaDimensiones;
        }


    }
}
