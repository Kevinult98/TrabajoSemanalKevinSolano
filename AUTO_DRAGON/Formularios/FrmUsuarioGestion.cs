using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AUTO_DRAGON.Formularios
{
    public partial class FrmUsuarioGestion : Form
    {
        //Este objeto será el que usa para asignar y obtener los valores que se 
        //mostrarán en el formulario (la parte gráfica)
        //debería contener toda la funcionalidad que se requiere cumplir
        //los requerimientos funcionaes
        private Logica.Models.Usuario MiUsuarioLocal { get; set; }

        private DataTable ListaUsuarios { get; set; }


        public FrmUsuarioGestion()
        {
            InitializeComponent();
            //se instancia el objeto local
            MiUsuarioLocal = new Logica.Models.Usuario();
            ListaUsuarios = new DataTable();

        }




        private void FrmUsuarioGestion_Load(object sender, EventArgs e)
        {
            //Este código se desencadena al mostrar el form gráficamente en pantalla
            //primero vamos a llenar la info de los tipos de roles que existen en BD
            CargarComboRoles();
            LlenarListaUsuarios(CboxVerActivos.Checked);
            LimpiarFormulario();
        }

        private void LlenarListaUsuarios(bool VerActivos, string FiltroBusqueda = "")
        {


            //El cuadro de búsqueda tiene escrita la palabra "Buscar.." para no usar un label
            //debemos considerar esa palabra como NO válida y cualquier otro texto
            //como válido para el parámetro de búsqueda
            string Filtro = "";

            if (!string.IsNullOrEmpty(FiltroBusqueda) && FiltroBusqueda != "Buscar..." )
            {
                Filtro = FiltroBusqueda;
            }
            ListaUsuarios = MiUsuarioLocal.Listar(VerActivos, Filtro);
            DgvListaUsuarios.DataSource = ListaUsuarios;
            DgvListaUsuarios.ClearSelection();
        }

        private void CargarComboRoles()
        {
            DataTable DatosDeRoles = new DataTable();
            DatosDeRoles = MiUsuarioLocal.MiRol.Listar();
            CbRol.ValueMember = "ID";
            CbRol.DisplayMember = "Descrip";
            CbRol.DataSource = DatosDeRoles;
            CbRol.SelectedIndex = -1;
        }

        private bool ValidarDatosRequeridos(bool ValidarPassword = true)
        //esta funcion valida los datos requeridos segun el diseño
        {
            bool R = false;
            if (!string.IsNullOrEmpty(MiUsuarioLocal.NombreCompleto) &&
                !string.IsNullOrEmpty(MiUsuarioLocal.Cedula) &&
                !string.IsNullOrEmpty(MiUsuarioLocal.Email) 
                && MiUsuarioLocal.MiRol.CodigoRol > 0)
            {

                //La contraseña No se ebe validar si estamos en modo eficion
                //y no hemos escrito algo en la contraseña
                if (ValidarPassword && !string.IsNullOrEmpty(MiUsuarioLocal.Contrasenia))
                {
                    R = true;
                }
                else
                {
                    //Si se cumplen los parámetros de validación se pasa el valor de R a true
                    R = true;
                }

              
              
            }
            else
            {
                if (string.IsNullOrEmpty(MiUsuarioLocal.NombreCompleto))
                {
                    MessageBox.Show("Debe Digitar el Nombre", "Error de Validación", MessageBoxButtons.OK);
                    TxtNombre.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(MiUsuarioLocal.Cedula))
                {
                    MessageBox.Show("Debe digitar la cédula", "Error de validación", MessageBoxButtons.OK);
                    TxtCedula.Focus();
                    return false;

                }
                if (string.IsNullOrEmpty(MiUsuarioLocal.Email))
                {
                    MessageBox.Show("Debe digitar email", "Error de validación", MessageBoxButtons.OK);
                    TxtEmail.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(MiUsuarioLocal.Contrasenia))
                {
                    MessageBox.Show("Debe digitar la contraseña", "Error de validación", MessageBoxButtons.OK);
                    TxtContrasennia.Focus();
                    return false;
                }
                if (MiUsuarioLocal.MiRol.CodigoRol <= 0)
                {
                    MessageBox.Show("Debe seleccionar un Rol", "Error de validación", MessageBoxButtons.OK);
                    CbRol.Focus();
                    return false;

                }
            }
            return R;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {

            if (ValidarDatosRequeridos())
            {
                bool OkCedula = MiUsuarioLocal.ConsultarPorCedula(MiUsuarioLocal.Cedula);
                bool OkEmail = MiUsuarioLocal.ConsultarPorEmail();
                if (!OkCedula && !OkEmail)
                {
                    string Mensaje = string.Format("Desea Continuar y Agregar al Usuario {0} ?", MiUsuarioLocal.NombreCompleto);
                    DialogResult Continuar = MessageBox.Show(Mensaje, "???", MessageBoxButtons.YesNo);

                    if (Continuar == DialogResult.Yes)
                    {
                        if (MiUsuarioLocal.Agregar())
                        {
                            MessageBox.Show("Usuario Agregado Correctamente", ":)", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            LlenarListaUsuarios(CboxVerActivos.Checked);
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error y no se ha guardado el usuario", ":(", MessageBoxButtons.OK);
                        }
                    }
                       
                }
                else
                {
                    //en caso que ya exista la cedula o el email, debe informarle al usuario
                    if (OkCedula)
                    {
                        MessageBox.Show("Ya existe un usuario con la cédula indicada", "Error de Validación", MessageBoxButtons.OK);
                    }
                    if (OkEmail)
                    {
                        MessageBox.Show("Ya existe un usuario con el Email digitado", "Error de Validación", MessageBoxButtons.OK);
                    }
                }
            }
        }
    

        private void LimpiarFormulario(bool LimpiarBusqueda = true)
        {
            TxtIDUsuario.Clear();
            TxtNombre.Clear();
            TxtCedula.Clear();
            TxtTelefono.Clear();
            TxtEmail.Clear();
            TxtContrasennia.Clear();
            CbRol.SelectedIndex = -1;
            //al reinstanciar el objeto local se eliminan todos los datos de los atributos
            MiUsuarioLocal = new Logica.Models.Usuario();
            ActivarAgregar();
            if (LimpiarBusqueda)
            {
                TxtBuscar.Text = "Buscar...";
            }
            
        }

        private void TxtNombre_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtNombre.Text.Trim()))
            {
                MiUsuarioLocal.NombreCompleto = TxtNombre.Text.Trim();
            }
            else
            {
                MiUsuarioLocal.NombreCompleto = "";
            }
        }

        private void TxtCedula_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCedula.Text.Trim()))
            {
                MiUsuarioLocal.Cedula = TxtCedula.Text.Trim();
            }
            else
            {
                MiUsuarioLocal.Cedula = "";
            }
        }

        private void TxtTelefono_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtTelefono.Text.Trim()))
            {
                MiUsuarioLocal.Telefono = TxtTelefono.Text.Trim();
            }
            else
            {
                MiUsuarioLocal.Telefono = "";
            }
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtEmail.Text.Trim()))
            {
                if (Commons.ObjetosGlobales.ValidarEmail(TxtEmail.Text.Trim()))
                {
                    MiUsuarioLocal.Email = TxtEmail.Text.Trim();
                }
                else
                {
                    MessageBox.Show("El Formato del correo no es correcto!", "Error de validación", MessageBoxButtons.OK) ;
                    TxtEmail.Focus();
                    TxtEmail.SelectAll();
                }
               
            }
            else
            {
                MiUsuarioLocal.Email = "";
            }
        }

        private void TxtContrasennia_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtContrasennia.Text.Trim()))
            {
                MiUsuarioLocal.Contrasenia = TxtContrasennia.Text.Trim();
            }
            else
            {
                MiUsuarioLocal.Contrasenia = "";
            }
        }

        private void CbRol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CbRol.SelectedIndex >= 0)
            {
                MiUsuarioLocal.MiRol.CodigoRol = Convert.ToInt32(CbRol.SelectedValue);
            }
            else
            {
                MiUsuarioLocal.MiRol.CodigoRol = 0;
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            LlenarListaUsuarios(CboxVerActivos.Checked);
        }

    

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosRequeridos(false))
            {
                //se usa objeto temporal para no tocar el usuario local y poder
                //evaluar si tiene datos los atributos que el usuario existe aún en BD
                Logica.Models.Usuario ObjUsuario = MiUsuarioLocal.ConsultarPorID(MiUsuarioLocal.CodigoUsuario);
                if (ObjUsuario.CodigoUsuario > 0)
                {
                    string Mensaje = string.Format("Desea Continuar con la Modificación Del Usuario {0} ?", MiUsuarioLocal.NombreCompleto);
                    DialogResult Continuar = MessageBox.Show(Mensaje, "???", MessageBoxButtons.YesNo);

                    if (Continuar == DialogResult.Yes)
                    {
                        if (MiUsuarioLocal.Editar())
                        {
                            MessageBox.Show("El Usuario Se ha actualizado Correctamente", ":D", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            LlenarListaUsuarios(CboxVerActivos.Checked);
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error y no se actualizó el usuario", ":(", MessageBoxButtons.OK);
                            LimpiarFormulario();
                            
                        }
                    }
                        
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
                Logica.Models.Usuario ObjUsuarioTemporal = MiUsuarioLocal.ConsultarPorID(MiUsuarioLocal.CodigoUsuario);

            //si se muestran los usuarios eliminados, el botón debe funcionar par aactivar de nuevo
            //al usuario y mostrarlo de nuevo en la lista de activos
            //1. crear el SPUsuarioActivar.
            // Modificar la clase Usuario para que tenga una función de activacion de usuario
            //3. modificar el código que este botón para que tenga ambas funcionalidades
                if (ObjUsuarioTemporal.CodigoUsuario > 0)
                {


                string Mensaje = "";
                    if (CboxVerActivos.Checked)
                {
                   Mensaje= string.Format("Desea Continuar con la Desactivación Del Usuario {0} ?", MiUsuarioLocal.NombreCompleto);
                }
                else
                {
                    Mensaje = string.Format("Desea Continuar con la Activación Del Usuario {0} ?", MiUsuarioLocal.NombreCompleto);
                }
                    
                DialogResult Continuar = MessageBox.Show(Mensaje, "???" , MessageBoxButtons.YesNo);

                if (Continuar == DialogResult.Yes)
                {
                    if (CboxVerActivos.Checked)
                    {
                        if (MiUsuarioLocal.Eliminar())
                        {
                            MessageBox.Show("El usuario se ha eliminado Correctamente!", ":D", MessageBoxButtons.OK);
                           
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al Eliminar!", ":(", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        if (MiUsuarioLocal.Activar())
                        {
                            MessageBox.Show("El usuario se ha activado Correctamente!", ":D", MessageBoxButtons.OK);

                        }
                        else
                        {
                            MessageBox.Show("Hubo un error y no se desactivó!", ":(", MessageBoxButtons.OK);
                        }
                    }

                    LimpiarFormulario();
                    LlenarListaUsuarios(CboxVerActivos.Checked);

                }              
           }
        }

        private void DgvListaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvListaUsuarios.SelectedRows.Count == 1)
            {
                LimpiarFormulario(false);

                DataGridViewRow MiFila = DgvListaUsuarios.SelectedRows[0];

                int CodigoUsuario = Convert.ToInt32(MiFila.Cells["CcodigoUsuario"].Value);

                MiUsuarioLocal = MiUsuarioLocal.ConsultarPorID(CodigoUsuario);

                TxtIDUsuario.Text = MiUsuarioLocal.CodigoUsuario.ToString();
                TxtNombre.Text = MiUsuarioLocal.NombreCompleto;
                TxtCedula.Text = MiUsuarioLocal.Cedula;
                TxtTelefono.Text = MiUsuarioLocal.Telefono;
                TxtEmail.Text = MiUsuarioLocal.Email;
                //TxtContrasennia.Text = MiUsuarioLocal.Contrasenia;
                CbRol.SelectedValue = MiUsuarioLocal.MiRol.CodigoRol;
                ActivarEditaryEliminar();

            }
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Commons.ObjetosGlobales.CaracteresTexto(e, true);
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Commons.ObjetosGlobales.CaracteresNumeros(e);
        }

        private void TxtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Commons.ObjetosGlobales.CaracteresTexto(e, false, true);
        }

        private void ActivarAgregar()
        {
            BtnAgregar.Enabled = true;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            LblPassRequerido.Visible = true;
        }
        private void ActivarEditaryEliminar()
        {
            BtnAgregar.Enabled = false;
            BtnEditar.Enabled = true;
            BtnEliminar.Enabled = true;
            LblPassRequerido.Visible = false;
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            //Cada que escribimos algo en el cuadro de texto debemos llamar al método
            //de carga de usuarios considerando el valor de filtrado
            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count()>= 2)
            {
                LlenarListaUsuarios(CboxVerActivos.Checked, TxtBuscar.Text.Trim());
            }
            else
            {
                LlenarListaUsuarios(CboxVerActivos.Checked);
            }
        }

        private void CboxVerActivos_Click(object sender, EventArgs e)
        {
            LlenarListaUsuarios(CboxVerActivos.Checked);
            if (CboxVerActivos.Checked)
            {
                BtnEliminar.Text = "Eliminar";
                BtnEliminar.BackColor = Color.Brown;
            }
            else
            {
                BtnEliminar.Text = "Activar";
                BtnEliminar.BackColor = Color.DarkViolet;
            }
        }
    }
}
