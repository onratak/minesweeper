using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication17
{
    public partial class ortatarla : Form
    {
        public ortatarla()
        {
            InitializeComponent();
        }

        Button[,] buton = new Button[16, 16];


        // bomba yerleri
        int[,] bomba = new int[16, 16];
        // Bomba etrafındaki sayilar
        int[,] sayilar = new int[16, 16];
        int[,] bayrak = new int[16, 16];  //bayrak bilgisi


        int bayrakkoy = 40;
        int tahminedilenbomba = 0;


        Random rndm = new Random();
        

        private void ortatarla_Load(object sender, EventArgs e)
        {


            timer1.Enabled = true;
            label1.Text = bayrakkoy.ToString();

            int locx = 50, locy = 50;


            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    buton[i, j] = new Button();


                    buton[i, j].Location = new System.Drawing.Point(locx, locy);
                    locx = locx + 35;
                    bayrak[i, j] = 0;

                    if (i == 15)
                    {
                        locy = locy + 35;

                        locx = 50;
                    }

                    buton[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                    buton[i, j].Name = j.ToString() + " " + i.ToString();
                    buton[i, j].Size = new System.Drawing.Size(30, 30);
                    buton[i, j].TabStop = false;
                    buton[i, j].UseVisualStyleBackColor = true;
                    buton[i, j].MouseDown += new MouseEventHandler(btn_MouseDown);
                   
                    this.Controls.Add(buton[i, j]);

                }
            }

            bombala();
            sayilariyerlestir();

        }
        private void bombala()
        {
            int x, y;
            int yerlestirilenbombasayisi = 0, toplambombasayisi = 40;

            for (int i = 0; i < toplambombasayisi; )
            {
                //0 bomba yok , 1 bomba var ,-1 sayi bilgisi

                x = rndm.Next(1, 15);
                y = rndm.Next(1, 15);
                if (bomba[x, y] == 0)
                {
                    bomba[x, y] = 1;
                    i++;
                    yerlestirilenbombasayisi++;
                }
            }
        }

         void btn_MouseDown(object sender, MouseEventArgs e)
            
        {
           
           
            Button b = (Button)sender;
            string[] parcalar;
            parcalar = b.Name.Split(' ');

            int satir = Convert.ToInt32(parcalar[1]);
            int stun = Convert.ToInt32(parcalar[0]);



                if (e.Button == MouseButtons.Left)
                {
                    if (bayrak[satir, stun] == 0) bombaKontrol(satir, stun);


                }
                else
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

                 for (int k = 0; k < 16; k++)  //bombaları göster
                 {
                     for (int l = 0; l < 16; l++)
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


                 oyunbitti();
             }

         }

         private void sayilariyerlestir()
         {
             for (int x = 0; x < 15; x++)
             {
                 for (int y = 0; y < 15; y++)
                 {
                     if (bomba[x, y] == 1)
                     {
                         // alt üst
                         if (bomba[x, y + 1] != 1 && y != 15)
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

                         //üst satir sağ sol
                         // 1: Boşluk
                         // 0: Bomba
                         //-1: Sayılar

                         if (x < 15 && y < 15)
                         {
                             if (bomba[x + 1, y + 1] != 1)
                             {
                                 sayilar[x + 1, y + 1]++;
                                 bomba[x + 1, y + 1] = -1;
                             }
                         }
                         if (x > 0 && y < 15)
                         {
                             if (bomba[x - 1, y + 1] != 1)
                             {
                                 sayilar[x - 1, y + 1]++;
                                 bomba[x - 1, y + 1] = -1;
                             }
                         }

                         // alt satir sağ sol
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

         private void etrafi_goster(int i, int j, System.Drawing.Color r)
         {

             

             //j

             if (j < 9 && i <9)
             { // zatem i ile j 9 dan büyük olamaz ki :D buton boyutu o kadar :D



                 if (buton[i, j].Enabled != false) 
                 {




                     if (j < 9)  
                     {
                         if (buton[i, j + 1].Enabled != false && buton[i, j + 1].Text == "") buton[i, j + 1].BackColor = r;  //yoo false değilse ve text ise red yapma dedik ama olmuyor :D cunku hepsinde i,j yazıyo :D asdasdasd a:ASD:ASD:QWE:QWE:SD:D: tamam düzeltem :D kafa gitmiş 

                     }
                     if (j > 0)
                     {

                         if (buton[i, j - 1].Enabled != false && buton[i, j - 1].Text == "") buton[i, j - 1].BackColor = r;

                     }
                     //i
                     if (i < 9)
                     {

                         if (buton[i + 1, j].Enabled != false && buton[i + 1, j].Text == "") buton[i + 1, j].BackColor = r;
                        

                     }
                     if (i > 0)
                     {

                         if (buton[i - 1, j].Enabled != false && buton[i - 1, j].Text == "") buton[i - 1, j].BackColor = r;

                     }
                     if (i < 9 && j < 9)
                     {
                         if (buton[i + 1, j + 1].Enabled != false && buton[i + 1, j + 1].Text == "") buton[i + 1, j + 1].BackColor = r;
                     }
                     if (i < 9 && j > 0)
                     {
                         if (buton[i + 1, j - 1].Enabled != false && buton[i + 1, j - 1].Text == "") buton[i + 1, j - 1].BackColor = r;
                     }

                     if (i > 0 && j > 0)
                     {
                         if (buton[i - 1, j - 1].Enabled != false && buton[i - 1, j - 1].Text == "") buton[i - 1, j - 1].BackColor = r;
                     }

                     if (i > 0 && j < 9) 
                     {
                         if (buton[i - 1, j + 1].Enabled != false && buton[i - 1, j + 1].Text == "") buton[i - 1, j + 1].BackColor = r;
                     }

                 }
             }
         }







         private void bos_ac(int i, int j)
         {


             if (bomba[i, j] == 0)
             {
                 buton[i, j].Enabled = false;
                 buton[i, j].BackColor = System.Drawing.SystemColors.ControlLightLight;
                 bomba[i, j] = 4;
                 buton[i, j].ImageKey = "";


                 //j
                 if (j < 15)
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

                 ;
             }

             oyunbitti();
         }


         string zorluk = "orta";
         private void kaydet()
         {


             int acilanbuton = 0;
             for (int aee = 0; aee < 16; aee++)
             {
                 for (int abb = 0; abb < 16; abb++)
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

             }
             for (int i = 0; i < 16; i++)
             {
                 for (int j = 0; j < 16; j++)
                 {
                     buton[i, j].Enabled = false;
                 }
                 
             }
            
         

             isim kaydet = new isim(zaman, tahminedilenbomba, acilanbuton, zorluk);
             kaydet.Show();

         }


         private void oyunbitti()
         {
             int acilanbuton = 0;
             for (int i = 0; i < 16; i++)
             {
                 for (int j = 0; j < 16; j++)
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



             if (acilanbuton == 216)
             {
                 timer1.Enabled = false;
                 MessageBox.Show("Oyun Bitti Tebrikler");
                 kaydet();


             }

         }




        int zaman = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;

            label2.Text = zaman.ToString();
            zaman = zaman + 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < 16; j++) //oyunu yeniden hazırla
            {
                for (int i = 0; i < 16; i++)
                {
                    buton[i, j].Enabled = true;
                    bayrak[i, j] = 0;
                    buton[i, j].Text = "";
                    buton[i, j].ImageKey = "";
                    buton[i, j].ImageList = ımageList1;
                    sayilar[i, j] = Convert.ToInt32(null);
                    bomba[i, j] = 0;

                }
            }
            tahminedilenbomba = 0;
            bayrakkoy = 10;
            label1.Text = bayrakkoy.ToString();
            zaman = 0;
            label2.Text = zaman.ToString();
            bombala();
            sayilariyerlestir();
        }
    }
}
