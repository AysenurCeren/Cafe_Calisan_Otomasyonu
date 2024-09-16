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
    public partial class FrmUnvan : Form
    {
        public FrmUnvan()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Unvan", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private int selectedpersonel = -1;
        void PersonelListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select personel_id,ad,soyad From Personel", bgl.baglanti());
            da.Fill(dt);
            if(selectedpersonel != -1) 
            {
                lookUpEdit1.EditValue = selectedpersonel;
            }
            lookUpEdit1.Properties.ValueMember = "personel_id";
            lookUpEdit1.Properties.DisplayMember = "ad " + "soyad";
            lookUpEdit1.Properties.DataSource = dt;
        }
        void Temizle()
        {
            Txtid.Text = string.Empty;
            Txtad.Text = string.Empty;
        }

        private void FrmUnvan_Load(object sender, EventArgs e)
        {
            Listele();
            PersonelListele();
            Temizle();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Unvan (personel_id,unvan_adi) values (@p1,@p2)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p2",Txtad.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Unvan Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Unvan where unvan_id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Unvan Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Unvan personel_id=@p1,unvan_adi=@p2 where unvan_id=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p2", Txtad.Text);
            komut.Parameters.AddWithValue("@p3", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Listele();
            Temizle();
            MessageBox.Show("Unvan Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Temizle();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr["unvan_id"].ToString();
                Txtad.Text = dr["unvan_adi"].ToString();
                selectedpersonel = Convert.ToInt32(dr["personel_id"]);
                lookUpEdit1.EditValue = selectedpersonel;
            }
        }
    }
}
