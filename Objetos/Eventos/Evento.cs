using FINALSIMPROGRAMADO.Objetos.Camiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALSIMPROGRAMADO.Objetos.Eventos
{
    internal class Evento
    {
        private double tiempoOcurrencia;
        private string tipo;
        Camion camion;
        public Evento(string tipo, Camion camion, double tiempoOc)
        {
            this.tipo = tipo;
            this.camion = camion;
            this.tiempoOcurrencia = tiempoOc;
        }
        public double getTiempoOcurrencia()
        {
            return this.tiempoOcurrencia;
        }

        public string getTipo()
        {
            return this.tipo;
        }

        public Camion getCamion()
        {
            return this.camion;
        }
    }
}
