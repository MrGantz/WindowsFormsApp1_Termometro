using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1_Termometro
{
    
    public partial class Form1 : Form
    {

        double cpx = 98;
        double cpy = 40;
        double hipo = 60;
        double angulo = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Image newImage = Image.FromFile("C:\\Users\\chino\\Downloads\\grados.gif");
            Pen plumaAzul = new Pen(Color.Blue, 3f);
            this.Refresh();
            Graphics superficie = CreateGraphics();
            angulo = (double)numericUpDown1.Value-360;
            angulo = (angulo + 20.0) * 2.5;
            cpx = (Math.Cos(angulo*(Math.PI/180)) * hipo) + 98.0F;
            cpy = (Math.Sin(angulo*(Math.PI/180)) * hipo) + 100.0F;
            this.Text = angulo.ToString();

            PointF punto1 = new PointF((float) cpx, (float) cpy);
            PointF punto2 = new PointF(98.0F, 100.0F);
            
            //superficie.DrawImage(newImage, 84, 84, 234, 234);
            superficie.DrawLine(plumaAzul, punto1, punto2);
            superficie.Dispose();
            label1.Text = (numericUpDown1.Value + 20).ToString();
        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            else
            {
                string buffer;
                buffer = serialPort1.ReadLine();
                buffer = buffer.Substring(0,2);
                if(buffer.Length <= 8)
                {
                    int temp = Convert.ToInt32(buffer);
                    numericUpDown1.Value = temp;
                    label3.Text = buffer + " " + "*C";
                    //this.Refresh();
                }
            }
        }
    }
}
