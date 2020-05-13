using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace WindowsFormsApplication17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        int kere = 0;

        private void button1_Click(object sender, EventArgs e)
        {
          

            if(kere==0) timer3.Enabled = true;
            kere++;
            kolaytarla newForm = new kolaytarla();
            Thread.Sleep(500);
            newForm.ShowDialog();
        
        }

        private void button2_Click(object sender, EventArgs e)

        {
            if (kere == 0) timer3.Enabled = true;
            kere++;
            ortatarla newForm = new ortatarla();
            Thread.Sleep(500);
            if(timer==1) newForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kere == 0) timer3.Enabled = true;
            kere++;
            zortarla newForm = new zortarla();
            Thread.Sleep(700);
            if (timer == 1) newForm.ShowDialog();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Mayın Tarlası";
            pictureBox2.Visible = false; // burada bu niye görünmez olmuyor :D simdi gördüm ya :D heyy :D  yan canın sıkkın gibi geliyor öyleyse 
            this.Size = new Size(0, 0);
            timer2.Enabled = true;    
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Her açılan boş kutu ( bomba olmayan ) 1 puan , her koyulan doğru bayrak (bayrak olan yerde buton var ise) 2 puan.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 ye = new Form2();
            ye.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

      

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Size=new Size(200,300);

            

            for (int l = 300; l < 501; l++)
            {
                this.Size = new Size(l, 300);

                if (l == 500) timer2.Enabled = false;
            }
        }
        int timer = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 100;
          
            for (int l = 300; l < 501; l++)
            {
                this.Size = new Size(500, l);

             
            }


            for (int i = 164; i < 351; i++)
			{
			 
			
            button5.Location = new System.Drawing.Point(246, i);
            button4.Location = new System.Drawing.Point(110, i); 
            }


            SoundPlayer player = new SoundPlayer();
            string path = "C:\\windows\\media\\ding.wav"; // Müzik adresi
            player.SoundLocation = path;
            player.Play(); //play it


            pictureBox2.Visible = true;

            timer = 1;
            timer3.Enabled = false;
        }
    }
}
