using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Rentas
{
    public partial class FormProveedores : Form
    {
        ProveedoresBL _proveedoresBL;


        public FormProveedores()
        {
            InitializeComponent();
            _proveedoresBL = new ProveedoresBL();

            // ListaProveedoresBindingSource.DataSource = _proveedoresBL.ObtenerProverdores();
            listaProveedoresBindingSource.DataSource = _proveedoresBL.ObtenerProverdores();

        }

        

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }


            }
        }

        private void Eliminar(int id)
        {
            var resultado = _proveedoresBL.EliminarProveedores(id);

            if (resultado == true)
            {
                listaProveedoresBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el Proveedor");
            }
        }

        private void listaProveedoresBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaProveedoresBindingSource.EndEdit();
            var proveedores = (Proveedores)listaProveedoresBindingSource.Current;

            var resultado = _proveedoresBL.GuardarProveedores(proveedores);

            if (resultado.Exitoso == true)
            {
                listaProveedoresBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Proveedor guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            Cancelar.Visible = !valor;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _proveedoresBL.AgregarProveedores();
            listaProveedoresBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {

            _proveedoresBL.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
        }

        private void activoLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
