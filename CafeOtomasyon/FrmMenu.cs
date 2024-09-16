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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CafeOtomasyon
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        public void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Menu", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void Temizle()
        {
            Txtid.Text = string.Empty;
            Txtad.Text = string.Empty;
            Txtfiyat.Text = string.Empty;
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Menu where menu_id=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Menü Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.None);
            Listele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                decimal fiyat;

                if (decimal.TryParse(Txtfiyat.Text, out fiyat))
                {
                    using (SqlCommand komut = new SqlCommand("INSERT INTO Menu (Menu_Ad, Fiyat) VALUES (@p1, @p2)", bgl.baglanti()))
                    {
                        komut.Parameters.AddWithValue("@p1", Txtad.Text);
                        komut.Parameters.Add("@p2", SqlDbType.Decimal).Value = fiyat;

                        komut.ExecuteNonQuery();
                    }

                    bgl.baglanti().Close();
                    Listele();
                    Temizle();
                    MessageBox.Show("Menu Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Geçersiz fiyat formatı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                decimal fiyat;

                if (decimal.TryParse(Txtfiyat.Text, out fiyat))
                {
                    using (SqlCommand komut = new SqlCommand("UPDATE Menu SET Menu_Ad=@p1, Fiyat=@p2 WHERE menu_id=@p3", bgl.baglanti()))
                    {
                        komut.Parameters.AddWithValue("@p1", Txtad.Text);
                        komut.Parameters.Add("@p2", SqlDbType.Decimal).Value = fiyat;
                        komut.Parameters.AddWithValue("@p3", Txtid.Text);

                        komut.ExecuteNonQuery();
                    }

                    bgl.baglanti().Close();
                    Listele();
                    Temizle();
                    MessageBox.Show("Menu Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Geçersiz fiyat formatı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["menu_id"].ToString();
                Txtad.Text = dr["Menu_Ad"].ToString();
                Txtfiyat.Text = dr["Fiyat"].ToString();
            }
        }
    }
}
