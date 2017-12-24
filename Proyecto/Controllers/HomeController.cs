using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recepciones.Controllers
{

    public class HomeController : Controller
    {
        RECEPCIONESEntities3 cnx;


        public HomeController()
        {
            cnx = new RECEPCIONESEntities3();
        }
        public ActionResult Formulario()
        {
            return View();
        }
        public ActionResult Recep()
        {
            IngresoController ctl = new IngresoController();
            ActionResult resultado = ctl.Recep();
            return resultado;
        }
        public ActionResult EliminarIngreso(int folio)

        {
            cnx.Ingreso.Remove(cnx.Ingreso.Where(x => x.Folio.Equals(folio)).First());

            cnx.SaveChanges();

            return View("ListadoIngreso", cnx.Ingreso.ToList());

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
        public ActionResult ListadoIngreso()
        {
            IngresoController ctl = new IngresoController();
            ActionResult resultado2 = ctl.ListadoIng();
            return resultado2;
        }

        public ActionResult Guardar(string rut, string nombre, string apellido, string ciudad, string telefono, string predio, string direccion, string email, string tipo)
        {

            Proyecto.Models.Cliente cliente = new Proyecto.Models.Cliente
            {


                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Ciudad = ciudad,
                Telefono = telefono,
                Predio = predio,
                Direccion = direccion,
                Email = email,
                Tipo = tipo

            };

            cnx.Cliente.Add(cliente);
            cnx.SaveChanges();

            return View("Listado", ListadoClientes());
        }
        public ActionResult Listado()
        {

            return View("Listado", ListadoClientes());
        }
        public ActionResult Ficha(int id)
        {

            return View(BuscarCliente(id));
        }

        private Cliente BuscarCliente(int id)
        {
            Cliente nuevo = new Cliente();
            foreach (Cliente cliente in cnx.Cliente.ToList())
            {
                if (cliente.Id == id)
                {
                    nuevo = cliente;
                }
            }
            return nuevo;
        }
        public ActionResult Visualizar(int id)
        {
            Cliente nuevo = BuscarCliente(id);

            if (nuevo != null)
            {
                return View("Ficha", nuevo);
            }
            return View("Listado", cnx.Cliente.ToList());
        }
        public ActionResult Eliminar(int id)

        {
            cnx.Cliente.Remove(cnx.Cliente.Where(x => x.Id.Equals(id)).First());

            cnx.SaveChanges();

            return View("Listado", cnx.Cliente.ToList());

        }



        private List<Proyecto.Models.Cliente> ListadoClientes()
        {

            cnx.Database.Connection.Open();


            List<Proyecto.Models.Cliente> auto = cnx.Cliente.ToList();

            cnx.Database.Connection.Close();

            return auto;
        }

    }
}