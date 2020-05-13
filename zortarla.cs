using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication17
{
    public partial class zortarla : Form
    {
        public zortarla()
        {
            InitializeComponent();
        }


        Button[,] buton = new Button[16, 30];
        // bomba yerleri
        int[,] bomba = new int[16, 30];
        // Bomba etrafındaki sayilar
        int[,] sayilar = new int[16, 30];
        int[,] bayrak = new int[16, 30];  //bayrak bilgisi


        int bayrakkoy = 99;
        int tahminedilenbomba = 0;


        Random rndm = new Random();


        private void zortarla_Load(object sender, EventArgs e)
        {


            timer1.Enabled = true;
            timer2.Enabled = true;
            label1.Text = bayrakkoy.ToString();

            int locx = 30, locy = 50;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    buton[i, j] = new Button();


                    buton[i, j].Location = new System.Drawing.Point(locx, locy);
                    locx = locx + 30;
                    bayrak[i, j] = 0;

                    if (j == 29)
                    {
                        locy = locy + 30;

                        locx = 30;
                    }


                    buton[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                    buton[i, j].Name = j.ToString() + " " + i.ToString();
                    buton[i, j].Size = new System.Drawing.Size(30, 30);
                    buton[i, j].TabStop = false;
                    buton[i, j].Text = "";
                    buton[i, j].UseVisualStyleBackColor = true;
                    buton[i, j].BackColor = System.Drawing.SystemColors.Highlight;

                    buton[i, j].MouseDown += new MouseEventHandler(btn_MouseDown);
                    buton[i, j].MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
                    buton[i, j].MouseMove += new System.Windows.Forms.MouseEventHandler(btn_MouseMove);
                    buton[i, j].MouseLeave += new System.EventHandler(btn_MouseLeave);
                    this.Controls.Add(buton[i, j]);

                }
            }


          bombala();
         sayilariyerlestir();
 
        }


        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            string[] parcalar;
            parcalar = b.Name.Split(' ');
            int satir = Convert.ToInt32(parcalar[1]);
            int stun = Convert.ToInt32(parcalar[0]);

            if (buton[satir, stun].Text == "") buton[satir, stun].BackColor = System.Drawing.Color.DeepSkyBlue;

        }
        private void btn_MouseLeave(object sender, EventArgs e)
        {

            Button b = (Button)sender;
            string[] parcalar;
            parcalar = b.Name.Split(' ');
            int satir = Convert.ToInt32(parcalar[1]);
            int stun = Convert.ToInt32(parcalar[0]);
            if (buton[satir, stun].Enabled != false && buton[satir, stun].Text == "") buton[satir, stun].BackColor = System.Drawing.SystemColors.Highlight;
        }


        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            string[] parcalar;
            parcalar = b.Name.Split(' ');
            int satir = Convert.ToInt32(parcalar[1]);
            int stun = Convert.ToInt32(parcalar[0]);
            System.Drawing.Color renk = System.Drawing.SystemColors.Highlight;

            etrafi_goster(satir, stun, renk);

            if (e.Button == System.Windows.Forms.MouseButtons.Left) left = false;
            else if (e.Button == System.Windows.Forms.MouseButtons.Right) rgt = false;




            if (rgt == false && left == false)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (bayrak[satir, stun] == 0) bombaKontrol(satir, stun);

                }
            }
        }

        bool rgt = false;
        bool left = false;
        //  bool bas = true;
        int a = 0;
        void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            string[] parcalar;
            parcalar = b.Name.Split(' ');

            int satir = Convert.ToInt32(parcalar[1]);
            int stun = Convert.ToInt32(parcalar[0]);

            etrafi_goster(satir, stun, System.Drawing.SystemColors.Highlight);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            { left = true; }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { rgt = true; }




            if (rgt && left)
            {

                etrafi_goster(satir, stun, System.Drawing.Color.DeepSkyBlue);


            }

            if (rgt == true && left == false)
            {
                if (buton[satir, stun].Enabled == true && buton[satir, stun].Text == "")
                {  //sağ click

                    if (bayrak[satir, stun] == 0)
                    {

                        bayrak[satir, stun] = 1;

                        buton[satir, stun].ImageKey = "bayrak.png";
                        buton[satir, stun].ImageList = ımageList1;
                        bayrakkoy--;
                        label1.Text = bayrakkoy.ToString();

                    }



                    else if (bayrak[Convert.ToInt32(parcalar[1]), Convert.ToInt32(parcalar[0])] == 1)
                    {
                        bayrak[satir, stun] = -1; //-1 soru işareti
                        buton[satir, stun].ImageKey = "soru.png";
                        buton[satir, stun].ImageList = ımageList1;


                    }
                    else if (bayrak[Convert.ToInt32(parcalar[1]), Convert.ToInt32(parcalar[0])] == -1)
                    {
                        bayrak[satir, stun] = 0; //-1 soru işareti
                        buton[satir, stun].ImageKey = "";
                        buton[satir, stun].ImageList = ımageList1;
                        bayrakkoy++;
                        label1.Text = bayrakkoy.ToString();

                    }
                }
            }


            // bas = true;
        }
        private void bombaKontrol(int i, int j)
        {
            if (bomba[i, j] == 0)
            {
               bos_ac(i, j);

            }
            else if (bomba[i, j] == 1)
            {
                buton[i, j].ImageKey = "bomb.png";
                buton[i, j].ImageList = ımageList1;

                for (int k = 0; k <16; k++)  //bombaları göster
                {
                    Thread.Sleep(20);
                    for (int l = 0; l < 30; l++)
                    {
                        if (bomba[k, l] == 1)
                        {
                            buton[k, l].ImageKey = "bomb.png";
                            buton[k, l].ImageList = ımageList1;
                        }
                    }
                }
                timer1.Enabled = false;
                MessageBox.Show("Oyunu Kaybettiniz!");

                // kaydetmek icin degiskenleri isim cs ye yolluyor
                kaydet();
                //************

            }
            else if (bomba[i, j] == -1) //-1 sayi
            {

                buton[i, j].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));


                if (sayilar[i, j] == 1) buton[i, j].ForeColor = System.Drawing.Color.Blue;
                if (sayilar[i, j] == 2) buton[i, j].ForeColor = System.Drawing.Color.Lime;
                if (sayilar[i, j] == 3) buton[i, j].ForeColor = System.Drawing.Color.Red;
                if (sayilar[i, j] == 4) buton[i, j].ForeColor = System.Drawing.Color.Green;
                if (sayilar[i, j] == 5) buton[i, j].ForeColor = System.Drawing.Color.Black;

                buton[i, j].Text = sayilar[i, j].ToString();
                buton[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;

                oyunbitti();
            }

        }


        private void sayilariyerlestir()
        {
      
             for (int x = 0; x < 16; x++)
              {
                  for (int y =0; y < 30; y++)
                  {
                      if (bomba[x, y] == 1)
                      {
                       
                         

                             // y + 1 , y - 1
                            if (bomba[x, y + 1] != 1 && y != 29)
                            {
                                sayilar[x, y + 1]++;
                                bomba[x, y + 1] = -1;
                            }
                            if (bomba[x, y - 1] != 1 && y != 0)
                            {
                                sayilar[x, y - 1]++;
                                bomba[x, y - 1] = -1;
                            }
                          
                            // x + 1, x - 1
                          if (bomba[x + 1, y] != 1 && x != 15)
                            {
                                sayilar[x + 1, y]++;
                                bomba[x + 1, y] = -1;
                            }
                            if (bomba[x - 1, y] != 1 && x != 0)
                            {
                                sayilar[x - 1, y]++;
                                bomba[x - 1, y] = -1;
                            }
                          
                            //y + 1 ---- x+1,x-1
                            // 1: Boşluk
                            // 0: Bomba
                            //-1: Sayılar

                           if (x < 15 && y < 29)
                            {
                                if (bomba[x + 1, y + 1] != 1)
                                {
                                    sayilar[x + 1, y + 1]++;
                                    bomba[x + 1, y + 1] = -1;
                                }
                            }
                            if (x > 0 && y < 29)
                            {
                                if (bomba[x - 1, y + 1] != 1)
                                {
                                    sayilar[x - 1, y + 1]++;
                                    bomba[x - 1, y + 1] = -1;
                                }
                            }

                            // y - 1 ---- x+1,x-1
                            if (x < 15 && y > 0)
                            {
                                if (bomba[x + 1, y - 1] != 1)
                                {
                                    sayilar[x + 1, y - 1]++;
                                    bomba[x + 1, y - 1] = -1;
                                }
                            }
                            if (x > 0 && y > 0)
                            {
                                if (bomba[x - 1, y - 1] != 1)
                                {
                                    sayilar[x - 1, y - 1]++;
                                    bomba[x - 1, y - 1] = -1;
                                }
                            }
                      }
                  }
              }
          
        }


        private void bombala()
        {
            int x, y;
            int yerlestirilenbombasayisi = 0, toplambombasayisi = 99;

            for (int i = 0; i < toplambombasayisi; )
            {
                //0 bomba yok , 1 bomba var ,-1 sayi bilgisi

                x = rndm.Next(1, 15);
                y = rndm.Next(1, 29);
                if (bomba[x, y] == 0)
                {
                    bomba[x, y] = 1;
                    i++;
                    yerlestirilenbombasayisi++;
                }
            }

           
        }



        private void bos_ac(int i, int j) // 16 30
        {


            if (bomba[i, j] == 0)

            {
               

                buton[i, j].Enabled = false;
                buton[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;
                bomba[i, j] = 2; // o butonlara bir daha bakmasın 
                buton[i, j].ImageKey = "";


                //j
                if (j < 29)
                {
                    bos_ac(i, j + 1);

                }
                if (j > 0)
                {
                    bos_ac(i, j - 1);

                }
                //i
                if (i < 15)
                {
                    bos_ac(i + 1, j);

                }
                if (i > 0)
                {
                    bos_ac(i - 1, j);

                }

                if (i > 0 && j < 29)
                {
                    bos_ac(i - 1, j + 1);
                }
                if (i < 15 && j < 29)
                {
                    bos_ac(i + 1, j + 1);
                }
                if (i < 15 && j > 0)
                {
                    bos_ac(i + 1, j - 1);
                }
                if (i > 0 && j > 0)
                {
                    bos_ac(i - 1, j - 1);
                }



            }

            else if (bomba[i, j] == -1)
            {
                


                buton[i, j].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                if (sayilar[i, j] == 1) buton[i, j].ForeColor = System.Drawing.Color.Blue;
                if (sayilar[i, j] == 2) buton[i, j].ForeColor = System.Drawing.Color.Green;
                if (sayilar[i, j] == 3) buton[i, j].ForeColor = System.Drawing.Color.Red;
                if (sayilar[i, j] == 4) buton[i, j].ForeColor = System.Drawing.Color.LawnGreen;
                if (sayilar[i, j] == 5) buton[i, j].ForeColor = System.Drawing.Color.Black;
                buton[i, j].Text = sayilar[i, j].ToString();
                buton[i, j].ImageKey = "";
                buton[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;

                ;
            }

            oyunbitti(); // j 30 i 16
        }




        private void etrafi_goster(int i, int j, System.Drawing.Color r)
        {

            int etraftakibayrak = 0;
            //j

            if (j < 30 && i < 16)
            {
                if (buton[i, j].Enabled != false)
                {

                    /// Etrafını yak 

                    if (j < 29)
                    {
                        if (buton[i, j + 1].Enabled != false && buton[i, j + 1].Text == "") buton[i, j + 1].BackColor = r;
                        if (bayrak[i, j + 1] == 1) etraftakibayrak++;

                    }
                    if (j > 0)
                    {

                        if (buton[i, j - 1].Enabled != false && buton[i, j - 1].Text == "") buton[i, j - 1].BackColor = r;
                        if (bayrak[i, j - 1] == 1) etraftakibayrak++;

                    }
                    //i
                    if (i < 15)
                    {

                        if (buton[i + 1, j].Enabled != false && buton[i + 1, j].Text == "") buton[i + 1, j].BackColor = r;
                        if (bayrak[i + 1, j] == 1) etraftakibayrak++;


                    }
                    if (i > 0)
                    {

                        if (buton[i - 1, j].Enabled != false && buton[i - 1, j].Text == "") buton[i - 1, j].BackColor = r;
                        if (bayrak[i - 1, j] == 1) etraftakibayrak++;

                    }
                    if (i < 15 && j < 29)
                    {
                        if (buton[i + 1, j + 1].Enabled != false && buton[i + 1, j + 1].Text == "") buton[i + 1, j + 1].BackColor = r;
                        if (bayrak[i + 1, j + 1] == 1) etraftakibayrak++;

                    }
                    if (i < 15 && j > 0)
                    {
                        if (buton[i + 1, j - 1].Enabled != false && buton[i + 1, j - 1].Text == "") buton[i + 1, j - 1].BackColor = r;
                        if (bayrak[i + 1, j - 1] == 1) etraftakibayrak++;

                    }

                    if (i > 0 && j > 0)
                    {
                        if (buton[i - 1, j - 1].Enabled != false && buton[i - 1, j - 1].Text == "") buton[i - 1, j - 1].BackColor = r;
                        if (bayrak[i - 1, j - 1] == 1) etraftakibayrak++;

                    }

                    if (i > 0 && j < 29)
                    {
                        if (buton[i - 1, j + 1].Enabled != false && buton[i - 1, j + 1].Text == "") buton[i - 1, j + 1].BackColor = r;
                        if (bayrak[i - 1, j + 1] == 1) etraftakibayrak++;

                    }

                }

                if (buton[i, j].Text != "" && sayilar[i, j] <= etraftakibayrak)
                {


                    etrafi_ac(i, j);


                }


            }
        }

        private void etraf_bomba(int i, int j)
        {
            if (bomba[i, j] == 1)
            {
                buton[i, j].ImageKey = "bomb.png";
                buton[i, j].ImageList = ımageList1;

                for (int k = 0; k < 16; k++)  //bombaları göster
                {
                    for (int l = 0; l < 30; l++)
                    {
                        if (bomba[k, l] == 1)
                        {
                            buton[k, l].ImageKey = "bomb.png";
                            buton[k, l].ImageList = ımageList1;
                        }
                    }
                }
                timer1.Enabled = false;
                MessageBox.Show("Oyunu Kaybettiniz!");

                // kaydetmek icin degiskenleri isim cs ye yolluyor
                kaydet();
                //************

            }
            else if (bomba[i, j] == -1) //-1 sayi
            {

                buton[i, j].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));


                if (sayilar[i, j] == 1) buton[i, j].ForeColor = System.Drawing.Color.Aqua;
                if (sayilar[i, j] == 2) buton[i, j].ForeColor = System.Drawing.Color.Lime;
                if (sayilar[i, j] == 3) buton[i, j].ForeColor = System.Drawing.Color.Red;
                if (sayilar[i, j] == 4) buton[i, j].ForeColor = System.Drawing.Color.Green;
                if (sayilar[i, j] == 5) buton[i, j].ForeColor = System.Drawing.Color.Black;

                buton[i, j].Text = sayilar[i, j].ToString();
                buton[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;


                oyunbitti();
            }

        }





        private void etrafi_ac(int i, int j)
        {


            if (j < 29)
            {
                if (buton[i, j + 1].Enabled != false && buton[i, j + 1].Text == "")
                {

                    if ((bomba[i, j + 1] == 1 || sayilar[i, j + 1] != Convert.ToInt32(null)) && bayrak[i, j + 1] != 1) etraf_bomba(i, j + 1);

                }



            }
            if (j > 0)
            {

                if (buton[i, j - 1].Enabled != false && buton[i, j - 1].Text == "")
                {

                    if ((bomba[i, j - 1] == 1 || sayilar[i, j - 1] != Convert.ToInt32(null)) && bayrak[i, j - 1] != 1) etraf_bomba(i, j - 1);

                }

            }
            //i

            if (i < 15)
            {

                if (buton[i + 1, j].Enabled != false && buton[i + 1, j].Text == "")
                {
                    if ((bomba[i + 1, j] == 1 || sayilar[i + 1, j] != Convert.ToInt32(null)) && bayrak[i + 1, j] != 1) etraf_bomba(i + 1, j);

                }

            }
            if (i > 0)
            {

                if (buton[i - 1, j].Enabled != false && buton[i - 1, j].Text == "")
                {
                    if ((bomba[i - 1, j] == 1 || sayilar[i - 1, j] != Convert.ToInt32(null)) && bayrak[i - 1, j] != 1) etraf_bomba(i - 1, j);

                }

            }
            if (i < 15 && j < 29)
            {
                if (buton[i + 1, j + 1].Enabled != false && buton[i + 1, j + 1].Text == "")
                {
                    if ((bomba[i + 1, j + 1] == 1 || sayilar[i + 1, j + 1] != Convert.ToInt32(null)) && bayrak[i + 1, j + 1] != 1) etraf_bomba(i + 1, j + 1);


                }
            }
            if (i < 15 && j > 0)
            {
                if (buton[i + 1, j - 1].Enabled != false && buton[i + 1, j - 1].Text == "")
                {
                    if ((bomba[i + 1, j - 1] == 1 || sayilar[i + 1, j - 1] != Convert.ToInt32(null)) && bayrak[i + 1, j - 1] != 1) etraf_bomba(i + 1, j - 1);

                }
            }

            if (i > 0 && j > 0)
            {
                if (buton[i - 1, j - 1].Enabled != false && buton[i - 1, j - 1].Text == "")
                {
                    if ((bomba[i - 1, j - 1] == 1 || sayilar[i - 1, j - 1] != Convert.ToInt32(null)) && bayrak[i - 1, j - 1] != 1) etraf_bomba(i - 1, j - 1);

                }
            }

            if (i > 0 && j < 29)
            {
                if (buton[i - 1, j + 1].Enabled != false && buton[i - 1, j + 1].Text == "")
                {
                    if ((bomba[i - 1, j + 1] == 1 || sayilar[i - 1, j + 1] != Convert.ToInt32(null)) && bayrak[i - 1, j + 1] != 1) etraf_bomba(i - 1, j + 1);

                }
            }


        }





        private void oyunbitti()
        {
            int acilanbuton = 0;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {

                    if (buton[i, j].Enabled == false)
                    {
                        acilanbuton++;
                    }

                    if (buton[i, j].Text != "")
                    {
                        acilanbuton++;
                    }
                }

            }

         

            if (acilanbuton == 381)
            {
                timer1.Enabled = false;
                MessageBox.Show("Oyun Bitti Tebrikler");
               kaydet();


            }


        }




        string zorluk = "zor";
        private void kaydet()
        {


            int acilanbuton = 0;
            for (int aee = 0; aee < 10; aee++)
            {
                for (int abb = 0; abb < 10; abb++)
                {

                    if (buton[aee, abb].Enabled == false)
                    {
                        acilanbuton++;
                    }

                    if (buton[aee, abb].Text != "")
                    {
                        acilanbuton++;
                    }
                    if ((bayrak[aee, abb] == 1 && bomba[aee, abb] == 1))
                    {
                        tahminedilenbomba++;
                    }
                }

                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        buton[i, j].Enabled = false;
                        
                    }

                }


            }


            isim kaydet = new isim(zaman, tahminedilenbomba, acilanbuton, zorluk);
            kaydet.Show();

        }








        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++) //oyunu yeniden hazırla
            {
                Thread.Sleep(20);
                for (int j = 0; j < 30; j++)
                {
                    buton[i, j].Enabled = true;
                    bayrak[i, j] = 0;
                    buton[i, j].Text = "";
                    buton[i, j].ImageKey = "";
                    buton[i, j].BackColor = System.Drawing.SystemColors.Highlight;
                    buton[i, j].ImageList = ımageList1;
                    sayilar[i, j] = Convert.ToInt32(null);
                    bomba[i, j] = 0;

                }
            }
            tahminedilenbomba = 0;
            bayrakkoy = 99;
            label1.Text = bayrakkoy.ToString();
            zaman = 0;
            dk = 0;
            label2.Text = dk.ToString() + " : " + zaman.ToString();
            bombala();
            sayilariyerlestir();
            timer1.Enabled = true;

            
        }


        int zaman = 0, dk = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;

            if (zaman == 59)
            {
                dk++;
                zaman = 0;
            }

            label2.Text = dk.ToString() + " : " + zaman.ToString();
            zaman = zaman + 1;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 100;
            if (this.Opacity < 1.0)
            {

                this.Opacity += 0.1;

            }

            if (this.Opacity == 1.0)
            {
                timer2.Enabled = false;
            }

        }


        public void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 50;

            if (this.Opacity > 0)
            {

                this.Opacity -= 0.1;

            }
            if (this.Opacity == 0) this.Dispose();


        }

    }
    }