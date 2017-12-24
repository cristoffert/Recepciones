using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recepciones.Controllers
{

    public class IngresoController : Controller
    {
        RECEPCIONESEntities3 cnx;


        public IngresoController()
        {
            cnx = new RECEPCIONESEntities3();
        }
        public ActionResult Recep()
        {
            return View();
        }
        public ActionResult GuardarIngreso(string rut, string nombre, string apellido, string cantidad, string analisis, string envio, string muestreo)
        {

            Proyecto.Models.Ingreso ingreso = new Proyecto.Models.Ingreso
            {

                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Cantidad = cantidad,
                TipoAnalisis = analisis,
                FechaEnvio = envio,
                FechaMuestreo = muestreo

            };

            cnx.Ingreso.Add(ingreso);
            cnx.SaveChanges();

            return View("ListadoIngreso", ListadoIngreso());
        }

        public ActionResult ListadoIng()
        {

            return View("ListadoIngreso", ListadoIngreso());
        }
        public ActionResult FichaIngreso(int id)
        {

            return View(BuscarIngreso(id));
        }
        private Ingreso BuscarIngreso(int id)
        {
            Ingreso nuevoingreso = new Ingreso();
            foreach (Ingreso ingreso in cnx.Ingreso.ToList())
            {
                if (ingreso.Folio == id)
                {
                    nuevoingreso = ingreso;
                }
            }
            return nuevoingreso;
        }
        public ActionResult EliminarIngreso(int folio)

        {
            cnx.Ingreso.Remove(cnx.Ingreso.Where(x => x.Folio.Equals(folio)).First());

            cnx.SaveChanges();

            return View("ListadoIngreso", cnx.Ingreso.ToList());

        }
        private List<Proyecto.Models.Ingreso> ListadoIngreso()
        {

            cnx.Database.Connection.Open();


            List<Proyecto.Models.Ingreso> ing = cnx.Ingreso.ToList();

            cnx.Database.Connection.Close();

            return ing;
        }
    }
}