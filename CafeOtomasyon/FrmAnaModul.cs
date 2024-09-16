using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyon
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmUrunler fr;
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr == null)
            {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }
        }
        FrmMenu fr1;
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr1 == null)
            {
                fr1 = new FrmMenu();
                fr1.MdiParent = this;
                fr1.Show();
            }
        }
        FrmKategori fr2;
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr2 == null)
            {
                fr2 = new FrmKategori();
                fr2.MdiParent = this;
                fr2.Show();
            }
        }
        FrmMusteriler fr3;
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr3 == null)
            {
                fr3 = new FrmMusteriler();
                fr3.MdiParent = this;
                fr3.Show();
            }
        }
        FrmPersonel fr4;
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr4 == null)
            {
                fr4 = new FrmPersonel();
                fr4.MdiParent = this;
                fr4.Show();
            }
        }
        FrmSubeler fr5;
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr5 == null)
            {
                fr5 = new FrmSubeler();
                fr5.MdiParent = this;
                fr5.Show();
            }
        }
        FrmSiparis fr6;
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr6 == null)
            {
                fr6 = new FrmSiparis();
                fr6.MdiParent = this;
                fr6.Show();
            }
        }
        FrmFaturalar fr7;
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr7 == null)
            {
                fr7 = new FrmFaturalar();
                fr7.MdiParent = this;
                fr7.Show();
            }
        }
        FrmAnasayfa fr8;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fr8 == null)
            {
                fr8 = new FrmAnasayfa();
                fr8.MdiParent = this;
                fr8.Show();
            }
        }
        FrmUnvan fr9;
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr9 == null)
            {
                fr9 = new FrmUnvan();
                fr9.MdiParent = this;
                fr9.Show();
            }
        }
    }
}
