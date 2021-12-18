using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class Usuario : ICrudBase
    {
        public int CodigoUsuario { get; set; }
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string CodigoRecuperacion { get; set; }
        
        

        //Composición de rol del usuario
        public RolUsuario MiRol { get; set; }

        //contructor
        public Usuario()
        {
            MiRol = new RolUsuario();
        }
        public byte Estado { get; set; }
        //public RolUsuario CodigoRol { get; set; }
       // public Usuario()
        //{
          //  CodigoRol = new RolUsuario();
        //}

        public bool Agregar()
        {
            bool R = false;
            Conexion MiCnnAdd = new Conexion();
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.NombreCompleto));
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
            Crypto MiEnCriptador = new Crypto();
            string PassEncriptado = MiEnCriptador.EncriptarEnUnSentido(this.Contrasenia);
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@Contrasenia",PassEncriptado));
            //Debemos enviar el valor del id del rol, usando la composicion de la clase UsuarioRol..
            MiCnnAdd.ListadoDeParametros.Add(new SqlParameter("@IdRol", this.MiRol.CodigoRol));
            int resultado = MiCnnAdd.DMLUpdateDeleteInsert("SPUsuarioAgregar");
            if (resultado > 0)
            {
                R = true;
            }
            return R;
        }

        public bool Editar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.NombreCompleto));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
            Crypto MiEnCriptador = new Crypto();


            string PassEncriptado = string.Empty;
            if (!string.IsNullOrEmpty(this.Contrasenia))
            {
                 PassEncriptado = MiEnCriptador.EncriptarEnUnSentido(this.Contrasenia);
            }
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Contrasenia", PassEncriptado));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdRol", this.MiRol.CodigoRol));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@ID", this.CodigoUsuario));
            int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioEditar");

            if (retorno == 1)
            {
                R = true;
            }

            return R;
        }

        public bool Eliminar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@ID", this.CodigoUsuario));

            int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioEliminar");

            if (retorno == 1)
            {
                R = true;
            }

            return R;
        }

        public Usuario ConsultarPorID(int ID)
        {
            Usuario R = new Usuario();

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@ID", ID));

            DataTable DatosUsuario = new DataTable();

            DatosUsuario = MiCnn.DMLSelect("SPUsuarioConsultarPorID");

            if (DatosUsuario != null && DatosUsuario.Rows.Count == 1)
            {
                DataRow Fila = DatosUsuario.Rows[0];

                R.CodigoUsuario = ID;
                R.NombreCompleto = Convert.ToString(Fila["nombreCompleto"]);
                R.Cedula = Convert.ToString(Fila["cedula"]);
                R.Telefono = Convert.ToString(Fila["telefono"]);
                R.Email = Convert.ToString(Fila["email"]);
                R.Contrasenia = string.Empty;
               // R.Contrasenia = Convert.ToString(Fila["contrasenia"]);
                R.MiRol.CodigoRol = Convert.ToInt32(Fila["codigoRol"]);

            }


            return R;
        }
        public bool ConsultarPorCedula()
        {
            bool R = false;

            return R;
        }
        public bool ConsultarPorEmail()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

            DataTable resultado = MiCnn.DMLSelect("SPUsuarioConsultarPorEmail");
            if (resultado != null && resultado.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public DataTable Listar(bool VerActivos, string Filtro = "")
        {
            DataTable R = new DataTable();


            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@VerActivos", VerActivos));

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Filtro", Filtro));


            R = MiCnn.DMLSelect("SPUsuariosListar");

            return R;
        }

        public bool EnviarCodigoRecuperacion(string CodigoVerif)
        {
            bool R = false;


                Conexion MyCnn = new Conexion();
                MyCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
                MyCnn.ListadoDeParametros.Add(new SqlParameter("@CodigoVerif", CodigoVerif));

                int Resultado = MyCnn.DMLUpdateDeleteInsert("SPUsuarioGuardarCodigoVerificacion");

                if (Resultado > 0)
                {
                    R = true;
                }



            return R;
        }

        public bool ConsultarPorCedula(string cedula)
        {
            bool R = false;

            Conexion MiConexion = new Conexion();

            MiConexion.ListadoDeParametros.Add(new SqlParameter("@Cedula", cedula));

            DataTable retorno = MiConexion.DMLSelect("SPUsuarioConsultarPorCedula");

            if (retorno != null && retorno.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public DataTable ListarActivos()
        {
            DataTable R = new DataTable();
            return R;
        }
        public DataTable ListarInactivos()
        {
            DataTable R = new DataTable();
            return R;
        }

        public bool Activar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@ID", this.CodigoUsuario));
            int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioActivar");
            if (retorno == 1)
            {
                R = true;
            }
            return R;
        }
        public Usuario ValidarIngreso(string user, string password)
        {
            Usuario R = new Usuario();
            Conexion MiCnn = new Conexion();
            Crypto MiEncriptador = new Crypto();
            string PassEncriptado = MiEncriptador.EncriptarEnUnSentido(password);
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@user", user));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@pass", PassEncriptado));
            DataTable DatosUsuario =  MiCnn.DMLSelect("SPUsuarioValidarIngreso");
            if (DatosUsuario != null && DatosUsuario.Rows.Count == 1)
            {
                DataRow Fila = DatosUsuario.Rows[0];
                R.CodigoUsuario = Convert.ToInt32(Fila["ID"]);
                R.NombreCompleto = Convert.ToString(Fila["nombreCompleto"]);
                R.Cedula = Convert.ToString(Fila["cedula"]);
                R.Telefono = Convert.ToString(Fila["telefono"]);
                R.Email = Convert.ToString(Fila["email"]);
                R.Contrasenia = string.Empty;
                R.MiRol.CodigoRol = Convert.ToInt32(Fila["codigoRol"]);
            }
            return R;
        }
    }
}
