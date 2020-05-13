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
    public partial class isim : Form
    {
        //süre int mi evet
        int sure,tahminedilenbomba,ab;
        string zorlukseviyesi, sorgu;
        public isim(int sr,int teb,int acilanbuton,string seviye)
        {
            InitializeComponent();
            sure = sr;
            tahminedilenbomba = teb;
            ab = acilanbuton;


            zorlukseviyesi = seviye;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            int puan = tahminedilenbomba * 3 + ab * 2;
            string isim = "";

            islem x = new islem();
            string bg = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\vt1.accdb";//buradada vt nin adı var ya dedım ya ıslem cs ye ben attım bunu kopyalayıp dıye bu aslında formda kullanıldıgında boyle kullanılıyo islemden cekereken
            string bg2 = x.bg(Application.StartupPath);//2si aynı sey fark etmez hata vermiyeni işimize yarar :D:D:D 2si de vermez:D benim hata neden kaynaklı yav :D cozemedım ya :D baska formda verıtabanı ıslemı denedın mı yoo deniyordum sen böyle yap diyince sildim hayr bu islem cs yle denıdn mı hayır bı denesene orda da vercek mı tamam
            isim = textBox1.Text;

            
            if (textBox1.Text.Equals(""))
            {
                this.Dispose();
                MessageBox.Show("Kayıt Yapılmadı");
                this.Dispose();
            }
            else //kaydı yapıyor ama hata veriyor işte islem cs benım attgm mı 
            {
                if (zorlukseviyesi == "kolay") sorgu = "INSERT INTO kolay (ad,zaman,puan) Values ('" + isim + "','" + sure + "','" + puan + "')";

                else if (zorlukseviyesi == "orta") sorgu = "INSERT INTO orta (ad,zaman,puan) Values ('" + isim + "','" + sure + "','" + puan + "')";
               
                else sorgu = "INSERT INTO zor (ad,zaman,puan) Values ('" + isim + "','" + sure + "','" + puan + "')";


                if (x.acc_ac(sorgu, bg) == true) MessageBox.Show("Kaydedildi");
                else MessageBox.Show("hata");

               
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
