using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Personel
{
    public partial class Form1 : Form
    { 
        OleDbConnection conn= new OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=kisi.mdb");

        public void Kaydet()
        {
            OleDbCommand kaydet = new OleDbCommand("INSERT INTO Personel(Ad,Soyad,DogumTarihi,Adres,Sehir) VALUES ('"
                + txtAd.Text + "','" + txtSoyad.Text + "','" + txtDTarihi.Text + "','" + txtAdres.Text + "','" + txtSehir.Text + "')", conn);

            try
            {
                conn.Open();
                kaydet.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            { 
                conn.Close(); 
            } 
        } 
     public void Sil(int ID) 
     {
         OleDbCommand sil = new OleDbCommand("DELETE  FROM Personel WHERE Numara=" + ID + "", conn);


   try 
   { 
    conn.Open(); 
    sil.ExecuteNonQuery(); 
   } 
   catch(Exception ex) 
   { 
     MessageBox.Show(ex.Message); 
    } 
   finally 
   { 
    conn.Close(); 
   } 
  }
     public void IDDoldur()
     {
         cbNo.Items.Clear();

         OleDbCommand veri = new OleDbCommand("SELECT Numara FROM Personel ORDER BY Numara", conn);
         OleDbDataReader oku = null;
         try 
         { 
             conn.Open(); 
             oku=veri.ExecuteReader(); 
             while(oku.Read())
             { 
                 cbNo.Items.Add(oku.GetInt32(0)); 
             }
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message);
         }
         finally
         {
             oku.Close();
             conn.Close();
         } 
     }

     public void IDyeGoreFormDoldur(int ID)
     {
         OleDbCommand veri = new OleDbCommand("SELECT Numara, Ad, Soyad, DogumTarihi, Adres, Sehir FROM Personel where Numara="+ID+"", conn);
         OleDbDataReader oku=null;
         try
         {
             conn.Open();
             oku=veri.ExecuteReader();
             if (oku.Read())
             {
                 txtAd.Text = oku["Ad"].ToString();
                 txtSoyad.Text = oku["Soyad"].ToString();
                 txtAdres.Text = oku["Adres"].ToString();
                 txtSehir.Text = oku["Sehir"].ToString();
                 txtDTarihi.Text = oku["DogumTarihi"].ToString();

             }
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message);
         }
         finally
         {
             oku.Close();
             conn.Close();
         } 
     }

     public void Temizle()
     {
         txtAd.Text = ""; txtSoyad.Text = "";
         txtAdres.Text = ""; txtSehir.Text = ""; txtDTarihi.Text = "";
         txtAd.Focus(); 

     }

     public bool Kontrol()
     {
         if (txtAd.Text == "")
         {
             MessageBox.Show("Adı Giriniz");
             txtAd.Focus();
             return false;
         }
         else if (txtSoyad.Text == "")
         {
             MessageBox.Show("Soyadı Giriniz");
             txtSoyad.Focus();
             return false;
         }
         else if (txtDTarihi.Text== "")
         {

             MessageBox.Show("Doğum Tarihini Giriniz");
             txtDTarihi.Focus();
             return false;
         }

         else if (txtAdres.Text == "")
         {
             MessageBox.Show("Adresi Giriniz");
             txtAdres.Focus();
             return false;
         }
         else if (txtSehir.Text == "")
         {
             MessageBox.Show("Şehiri Giriniz");
             txtSehir.Focus();
             return false;
         }
         else
         {
             return true;
         }
     } 
 


        public Form1()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
                if(Kontrol() == true)
                {
                    Kaydet();
                    btnYeni.Enabled = true;
                    btnKaydet.Enabled = false;
                    btnIptal.Enabled = false;
                    IDDoldur();
                    cbNo.SelectedIndex = cbNo.Items.Count - 1;
                } 
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
            btnYeni.Enabled = false;
            btnKaydet.Enabled = true;
            btnIptal.Enabled = true;
            cbNo.SelectedIndex = -1; 
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {

            Temizle();
            btnYeni.Enabled = true;
            btnKaydet.Enabled = false;
            btnIptal.Enabled = false;
            cbNo.SelectedIndex = 0; 
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
         if(MessageBox.Show(cbNo.SelectedItem + " nolu kaydı silmek istiyor musunuz?", this.Text, 
MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) 
    { 
       Sil(Convert.ToInt32(cbNo.SelectedItem)); 
       IDDoldur(); 
       cbNo.SelectedIndex = cbNo.Items.Count - 1; 
    } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IDDoldur();
            cbNo.SelectedIndex = 0; 
        }

        private void cbNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDyeGoreFormDoldur(Convert.ToInt32(cbNo.SelectedItem)); 
        }

        


       
    }
}
