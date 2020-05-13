using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;

namespace WindowsFormsApplication17
{
    public partial class skor : Form
    {
        string tur;
        public skor(string turr)
        {
            InitializeComponent();
            tur = turr;
        }

        private void skor_Load(object sender, EventArgs e)
        {
            this.Text = "Oyuncu Skoarlar";
            label2.Text = tur;
            islem m = new islem();
            string bg = m.bg(Application.StartupPath);

            OleDbConnection bgl = new OleDbConnection(bg);
            try
            {
                bgl.Open();
                string x = "Select Top 5 * From  "+tur+"  Order By puan Desc, zaman Asc,ad Asc";

                //"Select Top 5 * From " + alanadi + " Order By puan Desc, zaman Asc,ad Asc"
                OleDbDataAdapter adp1 = new OleDbDataAdapter(x, bg);
                DataSet ds1 = new DataSet();

                try
                {
                    adp1.Fill(ds1, "x");
                }
                catch
                {
                    MessageBox.Show("hata2");
                }

                DataTable sonuc = ds1.Tables[0];
                CrystalReport1 de = new CrystalReport1();
                de.SetDataSource(sonuc);
                crystalReportViewer1.ReportSource = de;
                ReportDocument raporDoc = de;

                crystalReportViewer1.ReportSource = de;



            }
            catch
            {
                MessageBox.Show("hata");
            }
        }


        
        private OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\vt1.accdb");


        
        private DataTable GetTablolar(string alanadi)
        {
            string sql = "Select Top 5 * From " + alanadi + " Order By puan Desc, zaman Asc,ad Asc";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, baglan);
            DataTable tablo = new DataTable();
            adp.Fill(tablo);
            return tablo;
        }
    }
}
