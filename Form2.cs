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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skor y = new skor("kolay");
            y.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            skor y = new skor("orta");
            y.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            skor y = new skor("zor");
            y.Show();
        }
    }
}
