using FINALSIMPROGRAMADO.Objetos.Camiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALSIMPROGRAMADO.Objetos.Controllers
{
    internal class Controller_Pala
    {
        Pala pala;

        public Controller_Pala()
        {
            pala = new Pala();
        }

        public void crearFinCarga(Controller_Eventos controllerEventos, Controller_Camiones controllerCamiones,double reloj, dynamic[] vActual)
        {
            if (controllerCamiones.verificarCamionLibre())
            {
                Camion camion = controllerCamiones.cargarCamion(reloj, vActual);
                controllerEventos.crearEventoFinCarga(camion);
                pala.ocupar(reloj);
                vActual[9] = "Cargando";
                vActual[10] = "Camion id: " + camion.getId();
            }
            else
            {
                pala.liberar(reloj);
                vActual[9] = "Libre";
                vActual[10] = "Sin Camion";
            }
        }

        public bool verificarDisponibilidad()
        {
            if(pala.getEstado() == "Libre")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double getEstadisticas()
        {
            return pala.getTiempoLibre();
        }
    }
}
