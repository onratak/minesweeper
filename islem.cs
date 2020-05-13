using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace WindowsFormsApplication17
{
    class islem
    {
        public string bg(string app)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + app + "\\vt1.accdb"; //burada oynama yaptıydın sanki bir de bak 2 sey göstericem burada vt nin adı var
        }
        public bool acc_ac(string x, string baglan)
        {
            OleDbConnection bgl = new OleDbConnection(baglan);
            try
            {
                bgl.Open();
                try
                {
                    OleDbCommand cmdins = new OleDbCommand(x, bgl);
                    cmdins.ExecuteNonQuery();
                }
                catch
                {
                    bgl.Dispose();
                    return false;
                }
            }
            catch
            {
                bgl.Dispose();
                return false;
            }
            bgl.Dispose();
            return true;

        }

        public string[][] acc_ara(string x, string baglan)
        {
            string[][] gelen;
            OleDbConnection bgl = new OleDbConnection(baglan);
            try
            {
                bgl.Open();
            }
            catch
            {
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "!";
                bgl.Dispose();
                return gelen;
            }
            OleDbDataAdapter adp1 = new OleDbDataAdapter(x, baglan);
            DataSet ds1 = new DataSet();

            try
            {
                adp1.Fill(ds1, "x");
            }
            catch
            {
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "?";
                bgl.Dispose();
                return gelen;
            }
            

             
             DataTable sonuc = ds1.Tables[0]; //yemedi :D alla alla demin öyle olmustu qweqwe hocaya sorabılıyosan sorsana nasıl sorucam yokki bi cıkarım cıkar? cıakr yol yani sorabilecek bi kanalım yok :D kanal? :D :D yaz kızım ulaşamam hocaya bi fgacede ekli orada da hiç açık oldugunu görmedim bir kere yazdım cevap gelmedi :D:D ee napcaz ben normal mi yapmaya calıssam bıldıgın gıbı mı  evet mantklı en kısa yol :D bi deniyeyim boş durmak bir işe yaramaz ya da dur bı yenı bı formda dencem  tamam 
            int sayi = sonuc.Rows.Count;

            if (sayi == 0)
            {
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "+";
                bgl.Dispose();
                return gelen;
            }
            else
            {
                gelen = new string[sayi][];
                int sutun = sonuc.Columns.Count;
                int i = 0;
                foreach (DataRow satir in sonuc.Rows)
                {
                    gelen[i] = new string[sutun];
                    for (int ax = 0; ax < sutun; ax++)
                    {
                        gelen[i][ax] = satir[ax].ToString();
                    }
                    i++;
                }
                bgl.Dispose();
                return gelen;
            }
        }

        public string[][] ara(string x, string baglan)
        {
            string[][] gelen;
            SqlConnection sqlbaglan = new SqlConnection();
            try
            {
                sqlbaglan.ConnectionString = baglan;
                sqlbaglan.Open();
            }
            catch
            {
                sqlbaglan.Dispose();
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "!";
                return gelen;
            }

            SqlDataAdapter sqltablo = new SqlDataAdapter(x, sqlbaglan);

            DataTable sonuc = new DataTable();
            try
            {
                // Veri tablomuzu sql sorgumuzdan dönen kayıtlar ile dolduruyoruz.
                sqltablo.Fill(sonuc);
            }
            catch
            {
                sqlbaglan.Dispose();
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "!";
                return gelen;

            }
            int sayi = sonuc.Rows.Count;

            if (sayi == 0)
            {
                gelen = new string[1][];
                gelen[0] = new string[1];
                gelen[0][0] = "!";

                return gelen;
            }
            else
            {
                gelen = new string[sayi][];
                int sutun = sonuc.Columns.Count;
                int i = 0;
                foreach (DataRow satir in sonuc.Rows)
                {
                    gelen[i] = new string[sutun];
                    for (int ax = 0; ax < sutun; ax++)
                    {
                        gelen[i][ax] = satir[ax].ToString();
                    }
                    i++;
                }
                return gelen;
            }
        }

        public bool sorgu_calistir(string x, string baglan)
        {
            SqlConnection sqlbaglan = new SqlConnection();
            try
            {
                sqlbaglan.ConnectionString = baglan;
                sqlbaglan.Open();
            }
            catch
            {
                sqlbaglan.Dispose();
                return false;
            }
            try
            {
                SqlCommand cmdIns = new SqlCommand(x, sqlbaglan);
                cmdIns.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            sqlbaglan.Dispose();
            return true;
        }
    }
}
