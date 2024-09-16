using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyon
{
    public partial class FrmGiris : Form
    {
        SqlBaglantisi bgl = new SqlBaglantisi();
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand Komut = new SqlCommand("Select * From Personel where personel_id=@p1 and tc_no=@p2", bgl.baglanti());
            Komut.Parameters.AddWithValue("@p1", textBox1.Text);
            Komut.Parameters.AddWithValue("@p2", textBox2.Text);
            SqlDataReader dr = Komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
