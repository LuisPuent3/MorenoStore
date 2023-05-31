using System.Data;
using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CNUsuarios
    {
        private readonly CDUsuarios objDatos = new CDUsuarios();
        
        public CEUsuarios Consultar(int idUsuarios)
        {
            return objDatos.CDConsulta(idUsuarios);
        }

        public void Insertar(CEUsuarios Usuarios) {
        

            objDatos.CDinsertar(Usuarios);

        }

        public void Eliminar(CEUsuarios Usuarios)
        {


            objDatos.CDEliminar(Usuarios);

        }

        public void ActualizarDatos(CEUsuarios Usuarios)
        {


            objDatos.CDActualizarDatos(Usuarios);

        }

        public void ActualizarPass(CEUsuarios Usuarios)
        {


            objDatos.CDActualizarPass(Usuarios);

        }
        public void ActualizarIMG(CEUsuarios Usuarios)
        {


            objDatos.CDActualizarIMG(Usuarios);

        }



    }
}