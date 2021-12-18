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
    public partial class FrmUsuarioRecuperarContra : Form
    {
        public Logica.Email MyEmail { get; set; }
        public Logica.Models.Usuario MyUser { get; set; }
        public FrmUsuarioRecuperarContra()
        {
            InitializeComponent();
            MyEmail = new Logica.Email();
            MyUser = new Logica.Models.Usuario();
        }

        private void FrmUsuarioRecuperarContra_Load(object sender, EventArgs e)
        {
            TxtCodigoEnviado.Enabled = false;
            TxtPass1.Enabled = false;
            TxtPass2.Enabled = false;
            BtnAceptar.Enabled = false;

            TxtCodigoEnviado.Clear();
            TxtPass1.Clear();
            TxtPass2.Clear();

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnEnviarCodigo_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(TxtUsuario.Text.Trim()) && Commons.ObjetosGlobales.ValidarEmail(TxtUsuario.Text.Trim()))
            {
                MyUser.Email = TxtUsuario.Text.Trim();
                if (MyUser.ConsultarPorEmail())
                {
                    //si el correo existe para un usuario activo se procede a enviar el correo a un código
                    Random r = new Random();
                    int numero = r.Next(1000,20000);
                    string CodigoVerificacion = "ABC*" + numero.ToString();

                    if (MyUser.EnviarCodigoRecuperacion(CodigoVerificacion))
                    {
                        //Se procede a enviar el correo al usuario con el código

                        MyEmail.Asunto = "Su Código de Recuperación de contraseña para AUTO DRAGON es:";
                        MyEmail.CorreoDestino = MyUser.Email;

                        string Mensaje = string.Format("Su código de recuperación de contraseña es: {0}", CodigoVerificacion);

                        MyEmail.Mensaje = Mensaje;

                        if (MyEmail.EnviarCorreo_Net_Mail_SmtpClient())
                        {
                            MessageBox.Show("Correo Enviado Correctamente", ":)", MessageBoxButtons.OK);
                            TxtPass1.Enabled = true;
                            TxtPass2.Enabled = true;
                            BtnAceptar.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("El correo no se logró enviar!", ":(", MessageBoxButtons.OK);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("No Existe o el usuario esta Desactivado!", "Error de Validación", MessageBoxButtons.OK);
                }
            }

        }
    }
}
