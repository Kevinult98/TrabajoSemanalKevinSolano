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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void TmrHora_Tick(object sender, EventArgs e)
        {
            LblHora.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TmrHora.Enabled = true;
            LblUsuarioLogueado.Text = Commons.ObjetosGlobales.MiUsuarioDeSistema.Email;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //TODO : analizar si queremos hacer un logout cuando cerramos el principal

            Application.Exit();
        }

        private void gestiónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mostramos el formulario global de gestión de usuarios
            Commons.ObjetosGlobales.FormularioGestionDeUsuarios = new FrmUsuarioGestion();
            Commons.ObjetosGlobales.FormularioGestionDeUsuarios.Show();
        }
    }
}
