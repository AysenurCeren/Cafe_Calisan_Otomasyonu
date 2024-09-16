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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Temizle()
        {
            Txtid.Text= string.Empty;
            Txtad.Text= string.Empty;
        }
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Urun", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void MenuListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select menu_id,Menu_Ad From Menu", bgl.baglanti());
            da.Fill(dt);
            if(selectedmenu != 1)
            {
                lokpkategori.EditValue = selectedmenu;
            }
            lokpmenu.Properties.ValueMember = "menu_id";
            lokpmenu.Properties.DisplayMember = "Menu_Ad";
            lokpmenu.Properties.DataSource = dt;
        }
        public void KategoriListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select kategori_id,Kategori_Ad From Kategori", bgl.baglanti());
            da.Fill(dt);
            if(selectedkategori != -1)
            {
                lokpkategori.EditValue = selectedkategori;
            }
            lokpkategori.Properties.ValueMember = "kategori_id";
            lokpkategori.Properties.DisplayMember = "Kategori_Ad";
            lokpkategori.Properties.DataSource = dt;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            Listele();
            MenuListesi();
            KategoriListesi();
            Temizle();
        }
        private int selectedkategori = -1;
        private int selectedmenu = -1;
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Urun (Urun_Ad, Menu_id, Kategori_id) values (@p1, @p2, @p3)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", lokpmenu.EditValue);
            komut.Parameters.AddWithValue("@p3", lokpkategori.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Urun Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Urun where urun_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Urun Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Urun set Urun_Ad=@p1, Menu_id=@p2, Kategori_id=@p3 where urun_id=@p4", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", lokpmenu.EditValue);
            komut.Parameters.AddWithValue("@p3", lokpkategori.EditValue);
            komut.Parameters.AddWithValue("@p4", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Urun Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["urun_id"].ToString();
                Txtad.Text = dr["Urun_Ad"].ToString();
                selectedkategori = Convert.ToInt32(dr["Kategori_id"]);
                lokpkategori.EditValue = selectedkategori;
                selectedmenu = Convert.ToInt32(dr["Menu_id"]);
                lokpmenu.EditValue = selectedmenu;
            }
        }
    }
}
