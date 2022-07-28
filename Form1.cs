using FINALSIMPROGRAMADO.Objetos.Controllers;
using FINALSIMPROGRAMADO.Objetos.Eventos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FINALSIMPROGRAMADO
{
    public partial class Form1 : Form
    {
        int qEventos;
        double reloj;
        bool segundaSim;
        int qCamionesViejo;
        public Form1()
        {
            InitializeComponent();
            segundaSim = false;
        }

        private void btn_simular_Click(object sender, EventArgs e)
        {
            if (!corroboraciones())
            {
                return;
            }

            grilla.Rows.Clear();

            if (segundaSim)
            {
                borrarColumnas(qCamionesViejo);
            }

            int cantCamiones = (int) Math.Truncate(double.Parse(txt_qCamiones.Text));
            agregarCamiones(cantCamiones);

            qEventos = 0;
            reloj = 0;

            dynamic[] vActual = new dynamic[11 + cantCamiones * 2]; 

            cargarValoresInicialesVector(vActual);

            Controller_Eventos controllerEventos = new Controller_Eventos();
            Controller_Pala controllerPala = new Controller_Pala();
            Controller_Camiones controllerCamiones = new Controller_Camiones(cantCamiones,double.Parse(txt_a.Text),double.Parse(txt_b.Text),double.Parse(txt_media.Text));

            controllerPala.crearFinCarga(controllerEventos,controllerCamiones,reloj,vActual);

            vActual[8] = controllerCamiones.getCantEnCola();

            while (qEventos < Math.Truncate(double.Parse(txt_qEventos.Text)))
            {
                grilla.Rows.Add(vActual[0], vActual[1], vActual[2], vActual[3], vActual[4], vActual[5], vActual[6], vActual[7], vActual[8], vActual[9],vActual[10]);
                cargarValoresCamiones(vActual);
                simular(controllerEventos,controllerPala,controllerCamiones,vActual);
                
            }
            this.segundaSim = true;
            this.qCamionesViejo = (int) Math.Truncate(double.Parse(txt_qCamiones.Text));
            txt_tiempoLibre.Text = controllerPala.getEstadisticas().ToString();
        }

        private void simular(Controller_Eventos controllerEventos, Controller_Pala controllerPala, Controller_Camiones controllerCamiones, dynamic[] vActual)
        {
            vActual[2] = 0; vActual[3] = 0; vActual[5] = 0; vActual[6] = 0;
            
            List<Evento> eventoLista = controllerEventos.getProxEvento();

            reloj = eventoLista[0].getTiempoOcurrencia();
            vActual[1] = reloj;
            vActual[0] = "";

            
            qEventos++;
            

            for (int i = 0; i < eventoLista.Count; i++)
            {


                switch (eventoLista[i].getTipo())
                {
                    case "llegada":
                        vActual[0] = "Llegada de camion: " + eventoLista[i].getCamion().getId() + " / "; 
                        controllerCamiones.sumarACola(eventoLista[i].getCamion(),vActual);
                        if (controllerPala.verificarDisponibilidad())
                        {
                            controllerPala.crearFinCarga(controllerEventos,controllerCamiones,reloj,vActual);
                        }

                        break;
                    case "fin carga":
                        vActual[0] = "Fin Carga de camion: " + eventoLista[i].getCamion().getId() + " / ";
                        controllerCamiones.empezarViaje(reloj,controllerEventos,vActual);
                        controllerPala.crearFinCarga(controllerEventos, controllerCamiones, reloj,vActual);
                        break;

                    default:
                        MessageBox.Show("HAY ALGO MAL");
                        break;
                }

            }

            vActual[8] = controllerCamiones.getCantEnCola();
            controllerEventos.removeEvento(reloj);

        }

        private void convertirTxts(TextBox txt)
        {
            string nuevoValor = "";
            for (int i = 0; i < txt.Text.Length; i++)
            {
                if (txt.Text[i] == ',')
                {
                    nuevoValor += ".";
                }
                else
                {
                    nuevoValor += txt.Text[i];
                }
            }
            txt.Text = nuevoValor;
        }
        private bool corroboraciones()
        {
            if (txt_a.Text.Length == 0 || txt_b.Text.Length == 0 || txt_qEventos.Text.Length == 0 || txt_qCamiones.Text.Length == 0 || txt_media.Text.Length == 0)
            {
                MessageBox.Show("Alguno de los valores se encuentra vacio, por favor llene todos los campos");
                return false;
            }

            try
            {
                convertirTxts(txt_a);
                convertirTxts(txt_b);
                convertirTxts(txt_media);
                convertirTxts(txt_qCamiones);
                convertirTxts(txt_qEventos);

                double a = double.Parse(txt_a.Text);
                double b = double.Parse(txt_b.Text);
                double media = double.Parse(txt_media.Text);
                int qCamiones = (int)Math.Truncate(double.Parse(txt_qCamiones.Text));
                int qEventos = (int)Math.Truncate(double.Parse(txt_qEventos.Text));




                if (a > b)
                {
                    MessageBox.Show("B no puede ser menor a A en la carga de los camiones, vuelva a ingresar los valores...");
                    return false;
                }
                if (a < 0 || b < 0)
                {
                    MessageBox.Show("Alguno de los dos valores (A o B) de la carga de los camiones, vuelva a ingresar los valores...");
                    return false;
                }

                if (qCamiones <= 0)
                {
                    MessageBox.Show("La cantidad de camiones no puede ser negativa ni cero, vuelva a ingresar los valores...");
                    return false;
                }

                if (qEventos <= 0)
                {
                    MessageBox.Show("La cantidad de eventos no puede ser negativa ni cero, vuelva a ingresar los valores...");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Alguno de los formatos de entrada es invalido");
                return false;
            }

            
        }

        private void btn_defecto_Click(object sender, EventArgs e)
        {
            txt_a.Text = 10.ToString();
            txt_b.Text = 12.ToString();
            txt_media.Text = 15.ToString();
        }

        private void agregarCamiones(int qCamiones)
        {
            for (int i = 0; i < qCamiones; i++)
            {
                grilla.Columns.Add("camion" + (i + 1), "Camion " + (i + 1) + " Estado");
                grilla.Columns.Add("fin" + (i + 1), "Fin evento");
                if ((i % 2) == 0)
                {
                    grilla.Columns[11 + 2*i].DefaultCellStyle.BackColor = Color.AliceBlue;
                    grilla.Columns[12 + 2*i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                else
                {
                    grilla.Columns[11 + 2 * i].DefaultCellStyle.BackColor = Color.Khaki;
                    grilla.Columns[12 + 2 * i].DefaultCellStyle.BackColor = Color.Khaki;
                }
            }  
        }

        private void borrarColumnas(int qCamiones)
        {
            for(int i = 0; i < (qCamiones) ; i++)
            {
                grilla.Columns.Remove("camion"+(i+1));
                grilla.Columns.Remove("fin" + (i + 1));
            }
        }

        private void cargarValoresInicialesVector(dynamic[] vActual)
        {
            

            vActual[0] = "Inicio de la simulacion";

            for (int i = 1; i < 11; i++)
            {
                vActual[i] = 0;
            }

            for (int i = 11; i < vActual.Length; i = i+2)
            {
                vActual[i] = "En Espera";
                vActual[i + 1] = 0;
            }
        }

        private void cargarValoresCamiones(dynamic[] vActual)
        {
            for (int i = 11; i < vActual.Length; i++)
            {
                grilla.Rows[qEventos].Cells[i].Value = vActual[i];
            } 
        }

        private void btn_enunciado_Click(object sender, EventArgs e)
        {
            enunciado enun = new enunciado();
            enun.Show();
        }

        private void btn_interpretacion_Click(object sender, EventArgs e)
        {
            interpretacion inter = new interpretacion();
            inter.Show();
        }
    }
}
