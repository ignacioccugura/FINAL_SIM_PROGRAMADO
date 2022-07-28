using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALSIMPROGRAMADO.Generadores
{
    internal static class Generador
    {
        public static double generarUniforme(double a, double b, dynamic[] vActual )
        {

            Random generadorRandom = new Random();
            double rnd = generadorRandom.NextDouble();
            vActual[2] = rnd;
            double nuevoUniforme = a + rnd * (b - a);
            return nuevoUniforme;
        }

        public static double generarExponencial(double media, dynamic[] vActual)
        {
            Random generadorRandom = new Random();
            double rnd = generadorRandom.NextDouble();
            vActual[5] = rnd;
            double valor = Math.Round(-media * Math.Log(1 - rnd), 4);
            return valor;
        }


    }
}
