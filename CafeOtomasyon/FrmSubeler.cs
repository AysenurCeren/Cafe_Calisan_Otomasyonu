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
    public partial class FrmSubeler : Form
    {
        public FrmSubeler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Sube", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            Txtad.Text = string.Empty;
            Txtid.Text = string.Empty;
            Txttelefon.Text = string.Empty;
            Rchadres.Text = string.Empty;
        }
        private void FrmSubeler_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand Komut = new SqlCommand("insert into Sube (Sube_Ad,Sube_Tel,adres) values (@p1,@p2,@p3)", bgl.baglanti());
            Komut.Parameters.AddWithValue("@p1", Txtad.Text);
            Komut.Parameters.AddWithValue("@p2", Txttelefon.Text);
            Komut.Parameters.AddWithValue("@p3", Rchadres.Text);
            Komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Sube Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand Komut = new SqlCommand("delete from Sube where sube_id=@p1", bgl.baglanti());
            Komut.Parameters.AddWithValue("@p1", Txtid.Text);
            Komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Sube Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand Komut = new SqlCommand("update Sube set Sube_Ad=@p1,Sube_Tel=@p2,adres=@p3 where sube_id=@p4", bgl.baglanti());
            Komut.Parameters.AddWithValue("@p1", Txtad.Text);
            Komut.Parameters.AddWithValue("@p2", Txttelefon.Text);
            Komut.Parameters.AddWithValue("@p3", Rchadres.Text);
            Komut.Parameters.AddWithValue("@p4", Txtid.Text);
            Komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Sube Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["sube_id"].ToString();
                Txtad.Text = dr["Sube_Ad"].ToString();
                Txttelefon.Text = dr["Sube_Tel"].ToString();
                Rchadres.Text = dr["adres"].ToString();
            }
        }
    }
}
