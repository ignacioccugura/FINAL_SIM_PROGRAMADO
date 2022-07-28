using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALSIMPROGRAMADO.Objetos.Camiones
{
    internal class Camion
    {
        private int id;
        private string estado;
        private double finCarga;
        private double finViaje;
        public Camion(int id)
        {
            this.id = id;
            this.estado = "En Espera";
        }

        public void cargar(double tiempoCarga, double reloj)
        {
            this.estado = "Cargando";
            this.finCarga = tiempoCarga + reloj;
        }

        public double getFinCarga()
        {
            return this.finCarga;
        }

        public void viajar(double tiempoViaje, double reloj)
        {
            this.finViaje = tiempoViaje + reloj;
            this.estado = "Viajando";
        }

        public double getFinViaje()
        {
            return this.finViaje;
        }

        public int getId()
        {
            return this.id;
        }

        public string getEstado()
        {
            return this.estado;
        }

        public void esperar()
        {
            this.estado = "En Espera";
        }
    }
}
