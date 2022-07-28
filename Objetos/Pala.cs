using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINALSIMPROGRAMADO.Objetos
{
    internal class Pala
    {
        private string estado;
        private bool estuvoLibre;
        private double tiempoLibre;
        private double comienzoLibre;

        public Pala()
        {
            this.estado = "Cargando";
            this.estuvoLibre = false;
            this.tiempoLibre = 0;
        }

        public void liberar(double reloj)
        {
            this.estado = "Libre";
            this.estuvoLibre = true;
            this.comienzoLibre = reloj;
        }

        public void ocupar(double reloj)
        {
            this.estado = "Cargando";

            if (estuvoLibre)
            {
                this.tiempoLibre += (reloj - this.comienzoLibre);
                this.estuvoLibre = false;
                this.comienzoLibre = 0;
            }
            
        }

        public string getEstado()
        {
            return this.estado;
        }

        public double getTiempoLibre()
        {
            return this.tiempoLibre;
        }
    }
}
