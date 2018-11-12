using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class ProveedoresBL
    {
        Contexto _contexto;

        public BindingList<Proveedores> ListaProveedores { get; set; }

        public ProveedoresBL()
        {
            _contexto = new Contexto();
            ListaProveedores = new BindingList<Proveedores>();


        }

        public BindingList<Proveedores> ObtenerProverdores()
        {
            _contexto.Provedores.Load();
            ListaProveedores = _contexto.Provedores.Local.ToBindingList();

            return ListaProveedores;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultado GuardarProveedores(Proveedores proveedores)
        {
            var resultado = Validar(proveedores);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Exitoso = true;
            return resultado;
        }


        public void AgregarProveedores()
        {
            var nuevoProveedor = new Proveedores();
            _contexto.Provedores.Add(nuevoProveedor);
        }

        public bool EliminarProveedores(int id)
        {
            foreach (var proveedores in ListaProveedores.ToList())
            {
                if (proveedores.Id == id)
                {
                    ListaProveedores.Remove(proveedores);
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        private Resultado Validar(Proveedores proveedores)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (proveedores == null)
            {
                resultado.Mensaje = "Agregue un proveedor valido";
                resultado.Exitoso = false;

                return resultado;
            }

            if (string.IsNullOrEmpty(proveedores.Nombre) == true)
            {
                resultado.Mensaje = "Ingrese un Nombre";
                resultado.Exitoso = false;
            }

          


            return resultado;
        }

    }

   


   

   

    public class Proveedores
    {
        
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public int Telefono { get; set; }
            public string CorreoElectronico { get; set; }

        
           
        public bool Activo { get; set; }

            public Proveedores()
        {
            Activo = true;
        }

    }
}
