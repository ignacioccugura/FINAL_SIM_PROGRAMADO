using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINALSIMPROGRAMADO.Generadores;
using FINALSIMPROGRAMADO.Objetos.Camiones;

namespace FINALSIMPROGRAMADO.Objetos.Controllers
{
    internal class Controller_Camiones
    {
        private Queue<Camion> cola = new Queue<Camion> ();

        private double a;
        private double b;
        private double media;
        public Controller_Camiones(int qCamiones, double a, double b,double media)
        {
            this.a = a;
            this.b = b;
            this.media = media;

            for (int i = 0; i < qCamiones; i++)
            {
                cola.Enqueue(new Camion(i+1));
            }
        }

        public bool verificarCamionLibre()
        {
            if(cola.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Camion cargarCamion(double reloj, dynamic[] vActual)
        {
            Camion camion = cola.Peek();
            double tiempoCarga = Generador.generarUniforme(a,b,vActual);
            vActual[3] = tiempoCarga;
            vActual[4] = tiempoCarga + reloj;
            camion.cargar(tiempoCarga,reloj);

            //parte dinamica
            vActual[11+ ((camion.getId() - 1) * 2)] = camion.getEstado();
            vActual[12+ ((camion.getId() - 1) * 2)] = camion.getFinCarga();
            //parte dinamica

            return camion;
        }

        public void empezarViaje(double reloj, Controller_Eventos controllerEventos, dynamic[] vActual)
        {
            Camion camion = cola.Dequeue();
            double tiempoViaje = Generador.generarExponencial(media,vActual);
            vActual[6] = tiempoViaje;
            vActual[7] = tiempoViaje + reloj;
            camion.viajar(tiempoViaje,reloj);
            controllerEventos.crearEventoLlegada(camion);

            //parte dinamica
            vActual[11 + ((camion.getId() - 1) * 2)] = camion.getEstado();
            vActual[12 + ((camion.getId() - 1) * 2)] = camion.getFinViaje();
            //parte dinamica
        }

        public void sumarACola(Camion camion, dynamic[] vActual)
        {
            this.cola.Enqueue(camion);
            vActual[11+((camion.getId() - 1) * 2)] = "En Espera";
            vActual[12 + ((camion.getId() - 1) * 2)] = 0;
        }

        public int getCantEnCola()
        {
            if (this.cola.Count > 0)
            {
                if (this.cola.Peek().getEstado() == "Cargando")
                {
                    return this.cola.Count - 1;
                }
                else
                {
                    return this.cola.Count;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
