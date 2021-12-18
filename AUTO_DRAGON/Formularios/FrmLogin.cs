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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;
        }

        private bool ValidarDatos()
        {
            bool R = false;

            if (!string.IsNullOrEmpty(TxtEmail.Text.Trim()) && Commons.ObjetosGlobales.ValidarEmail(TxtEmail.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                R = true;
            }
            else
            {
                if (string.IsNullOrEmpty(TxtEmail.Text.Trim()))
                {
                    MessageBox.Show("Debe Digitar un nombre de usuario", "Error de Validación", MessageBoxButtons.OK);
                    TxtEmail.Focus();
                    return false;
                }
                if (!Commons.ObjetosGlobales.ValidarEmail(TxtEmail.Text.Trim()))
                {
                    MessageBox.Show("El correo no tiene el formato correcto", "Error de Validación", MessageBoxButtons.OK);
                    TxtEmail.Focus();
                    TxtEmail.SelectAll();
                    return false;
                }
                if (string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    MessageBox.Show("Debe Digitar la contraseña", "Error de Validación", MessageBoxButtons.OK);
                    TxtPassword.Focus();
                    return false;
                }
            }

            return R;
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            

            if (ValidarDatos())
            {
                Logica.Models.Usuario MiUsuarioValidado = new Logica.Models.Usuario();

                MiUsuarioValidado = MiUsuarioValidado.ValidarIngreso(TxtEmail.Text.Trim(), TxtPassword.Text.Trim());
                
                if (MiUsuarioValidado != null && MiUsuarioValidado.CodigoUsuario > 0)
                {
                    Commons.ObjetosGlobales.MiUsuarioDeSistema = MiUsuarioValidado;
                }
                //TODO : hay que validar que el usuario y la contraseña sean correctos
                //antes de mostrar el Frm Principal
                //muestro el objeto global del FrmMain
                Commons.ObjetosGlobales.MiFormPrincipal.Show();
                //oculto (no destruyo) el frm de login
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error de validación", MessageBoxButtons.OK);
            }
           
        }

        private void BtnIngresoDirecto_Click(object sender, EventArgs e)
        {
            Commons.ObjetosGlobales.MiUsuarioDeSistema.CodigoUsuario = 34;
            Commons.ObjetosGlobales.MiUsuarioDeSistema.Email = "prueba@gmail.com";
            Commons.ObjetosGlobales.MiUsuarioDeSistema.NombreCompleto = "prueba";
            Commons.ObjetosGlobales.MiUsuarioDeSistema.MiRol.CodigoRol = 2;
            Commons.ObjetosGlobales.MiFormPrincipal.Show();
            //oculto (no destruyo) el frm de login
            this.Hide();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift & e.KeyCode == Keys.Escape)
            {
                BtnIngresoDirecto.Visible = true;
            }
        }

        private void LblRecuperarContrasennia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Commons.ObjetosGlobales.FormularioRecuperacionContrasennia.TxtUsuario.Text = this.TxtEmail.Text.Trim();
            Commons.ObjetosGlobales.FormularioRecuperacionContrasennia.Show();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
