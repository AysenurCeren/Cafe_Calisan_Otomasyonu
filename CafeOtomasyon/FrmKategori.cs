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
    public partial class FrmKategori : Form
    {
        public FrmKategori()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kategori", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void Temizle()
        {
            Txtid.Text = "";
            Txtad.Text = "";
        }
        private void FrmKategori_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand Komut = new SqlCommand("insert into Kategori (Kategori_Ad) values (@p1)",bgl.baglanti());
            Komut.Parameters.AddWithValue("@p1", Txtad.Text);
            Komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kategori Başarıyla Eklendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr !=null)
            {
                Txtid.Text = dr["kategori_id"].ToString();
                Txtad.Text = dr["Kategori_Ad"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Kategori where kategori_id=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kategori Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Kategori set Kategori_Ad=@p1 where kategori_id=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",Txtad.Text);
            komut.Parameters.AddWithValue("@p2",Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kategori Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }
    }
}
