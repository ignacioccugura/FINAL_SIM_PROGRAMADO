using FINALSIMPROGRAMADO.Objetos.Camiones;
using FINALSIMPROGRAMADO.Objetos.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALSIMPROGRAMADO.Objetos.Controllers
{
    internal class Controller_Eventos
    {
        SortedList<double, List<Evento>> eventosOrdenados = new SortedList<double, List<Evento>>();

        public List<Evento> getProxEvento()
        {
            return this.eventosOrdenados.First().Value;
        }

        private void verificarSiHayOtroEvento(Evento ev_a_guardar)
        {
            if (this.eventosOrdenados.ContainsKey(ev_a_guardar.getTiempoOcurrencia()))
            {
                eventosOrdenados[ev_a_guardar.getTiempoOcurrencia()].Add(ev_a_guardar);
            }
            else
            {
                List<Evento> l = new List<Evento>();
                l.Add(ev_a_guardar);
                eventosOrdenados.Add(ev_a_guardar.getTiempoOcurrencia(), l);
            }
        }

        public void crearEventoFinCarga(Camion camion)
        {
            Evento evento = new Evento("fin carga",camion,camion.getFinCarga());
            verificarSiHayOtroEvento(evento);
        }

        public void crearEventoLlegada(Camion camion)
        {
            Evento evento = new Evento("llegada",camion,camion.getFinViaje());
            verificarSiHayOtroEvento(evento);
        }

        public bool removeEvento(double reloj)
        {
            return this.eventosOrdenados.Remove(reloj);
        }
    }
}
