using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyon
{
    public partial class FrmSiparis : Form
    {
        public FrmSiparis()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Siparis", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void Temizle()
        {
            Txtid.Text = string.Empty;
            lookMenu.Text = string.Empty;
            lookMusteri.Text = string.Empty;
            TxtTarih.Text = string.Empty;
        }
        private int selectedmusteri = -1;
        private int selectedmenu = -1;
        public void MenuListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select menu_id,Menu_Ad From Menu", bgl.baglanti());
            da.Fill(dt);
            if(selectedmenu != -1)
            {
                lookMenu.EditValue= selectedmenu;
            }
            lookMenu.Properties.ValueMember = "menu_id";
            lookMenu.Properties.DisplayMember = "Menu_Ad";
            lookMenu.Properties.DataSource = dt;
        }
        public void MusteriListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select musteri_id,ad From Musteri", bgl.baglanti());
            da.Fill(dt);
            if(selectedmusteri != -1)
            {
                lookMusteri.EditValue= selectedmenu;
            }
            lookMusteri.Properties.ValueMember = "musteri_id";
            lookMusteri.Properties.DisplayMember = "ad";
            lookMusteri.Properties.DataSource = dt;
        }
        private void FrmSiparis_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
            MenuListesi();
            MusteriListesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["siparis_id"].ToString();

                // Tarih formatını belirle
                DateTime siparisTarihi = Convert.ToDateTime(dr["Siparis_Tarihi"]);
                TxtTarih.Text = siparisTarihi.ToString("dd.MM.yyyy");

                selectedmenu = Convert.ToInt32(dr["menuID"]);
                lookMenu.EditValue = selectedmenu;

                selectedmusteri = Convert.ToInt32(dr["musteri_id"]);
                lookMusteri.EditValue = selectedmusteri;
            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime siparisTarihi = DateTime.ParseExact(TxtTarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("INSERT INTO Siparis (Siparis_Tarihi, menuID, musteri_id) VALUES (@p1, @p2, @p3)", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", siparisTarihi);
                    komut.Parameters.AddWithValue("@p2", lookMenu.EditValue);
                    komut.Parameters.AddWithValue("@p3", lookMusteri.EditValue);

                    komut.ExecuteNonQuery();
                }

                Listele();
                Temizle();
                bgl.baglanti().Close();

                MessageBox.Show("Sipariş Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Siparis where siparis_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            Listele();
            Temizle();
            bgl.baglanti().Close();
            MessageBox.Show("Sipariş Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime siparisTarihi = DateTime.ParseExact(TxtTarih.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                using (SqlCommand komut = new SqlCommand("UPDATE Siparis SET Siparis_Tarihi=@p1, menuID=@p2, musteri_id=@p3 WHERE siparis_id=@p4", bgl.baglanti()))
                {
                    komut.Parameters.AddWithValue("@p1", siparisTarihi);
                    komut.Parameters.AddWithValue("@p2", lookMenu.EditValue);
                    komut.Parameters.AddWithValue("@p3", lookMusteri.EditValue);
                    komut.Parameters.AddWithValue("@p4", Txtid.Text);

                    komut.ExecuteNonQuery();
                }

                Listele();
                Temizle();
                bgl.baglanti().Close();
                MessageBox.Show("Sipariş Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
