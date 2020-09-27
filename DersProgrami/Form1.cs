using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;

namespace DersProgrami
{
    public partial class Form1 : Form
    {
        static string connstr = "data source=localhost;port=3306;username=root;password=12345678";
        static string mysql = "SELECT * FROM okulveritabani.dersler";
        

        

        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            liste.Columns.Add("Ders Adı", 250);
            liste.Columns.Add("Hoca ID", 75);
            liste.Columns.Add("Derslik", 100);
            liste.Columns.Add("Başlangıç", 125);
            liste.Columns.Add("Bitiş", 125);
            liste.Columns.Add("Gün", 125);
        }

        private void btn_uyg_Click(object sender, EventArgs e)
        {
            liste.Clear();
            //liste.Columns.Add("Ders ID", 100);
            liste.Columns.Add("Ders Adı", 250);
            liste.Columns.Add("Hoca ID", 75);
            liste.Columns.Add("Derslik", 100);
            liste.Columns.Add("Başlangıç", 125);
            liste.Columns.Add("Bitiş", 125);
            liste.Columns.Add("Gün", 125);
            //bağlantı
            MySqlConnection conn = new MySqlConnection(connstr);
            MySqlCommand cmd = new MySqlCommand(mysql, conn);
            MySqlDataReader reader;
            //gerekli degiskenler
            
            string id,ad,hoca;
            string[] dersler = new string[100];
            int sinifSay=0;
            if (tb_sinifSay.Text != "")
            {
                sinifSay = Convert.ToInt32(tb_sinifSay.Text);
                
            }
           
            
            int i = 0;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader["DersID"].ToString();
                    dersler[i] = id;
                    i++;
                }
                reader.Close();
                Random rnd = new Random();
                string[] karisik = dersler.OrderBy(x => rnd.Next()).ToArray();

                int j = 0;
                for (i = 0; i < karisik.Length; i++)
                {
                    id = karisik[i];
                    if (id != null)
                    {
                        dersler[j] = id;
                        j++;
                    }
                }

                int sec = 0, t, u, derslikSay = 1, baslangic = 8, dersSuresi, bitis, gun = 1, k = 0;
                string teorik, uygulamali, cmdid, derslikAdi, dersBaslama, dersBitis, strGun = "";


                if (rb_guz.Checked == true) sec = 1;
                else if (rb_bahar.Checked == true) sec = 2;

            if (cb_Secim.Text=="Teorik+Uygulama") 
            {
                    if (sec == 1) //guz yariyili dersleri
                    {


                        for (i = 0; i < dersler.Length; i++)
                        {
                            if (tb_sinifSay.Text == "0")
                            {
                                MessageBox.Show("Derslik sayısı 0 olamaz.");
                                break;
                            }
                            if (tb_sinifSay.Text == "")
                            {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                            }
                            
                            id = dersler[i];
                            if (id == null) break;
                            cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=1 OR Yariyil=3 OR Yariyil=5 OR Yariyil=7) AND DersID=" + id);

                            cmd = new MySqlCommand(cmdid, conn);
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                

                                //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                derslikAdi = "Derslik-" + derslikSay;
                                ad = reader["DersAdi"].ToString();
                                hoca = reader["HocaID"].ToString();
                                teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                uygulamali = reader["Uygulamali"].ToString(); u = Convert.ToInt32(uygulamali);
                                dersSuresi = t + u;
                                dersBaslama = baslangic + ":30";
                                bitis = baslangic + dersSuresi;
                                dersBitis = bitis + ":20";
                                baslangic = baslangic + dersSuresi;
                                //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                if (bitis > 17)
                                {
                                    derslikSay++;
                                    if (derslikSay > sinifSay)
                                    {
                                        derslikSay = 1;
                                        gun++;
                                        baslangic = 8;
                                        bitis = 0;
                                        k = 1;

                                    }
                                    derslikAdi = "Derslik-" + derslikSay;
                                    baslangic = 8;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    k = 0;
                                }
                                

                                //GUNLERIN BELIRLENMESI

                                if (gun % 5 == 1) strGun = "Pazartesi";
                                else if (gun % 5 == 2) strGun = "Salı";
                                else if (gun % 5 == 3) strGun = "Çarşamba";
                                else if (gun % 5 == 4) strGun = "Perşembe";
                                else if (gun % 5 == 0) strGun = "Cuma";
                                //EKRANDA GOSTERILECEK BILGILER
                                string[] bilgiler = { ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                ListViewItem lst = new ListViewItem(bilgiler);
                                liste.Items.Add(lst);
                            }
                            if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                            reader.Close();

                        }
                    }

                        if (sec == 2) //bahar yariyili dersleri
                        {
                            for (i = 0; i < dersler.Length; i++)
                            {
                                if (tb_sinifSay.Text == "0")
                                {
                                    MessageBox.Show("Derslik sayısı 0 olamaz.");
                                    break;
                                }
                                if (tb_sinifSay.Text == "")
                                {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                                }
                                
                                id = dersler[i];
                                if (id == null) break;
                                cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=2 OR Yariyil=4 OR Yariyil=6 OR Yariyil=8) AND DersID=" + id);

                                cmd = new MySqlCommand(cmdid, conn);
                                reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {


                                    //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                    derslikAdi = "Derslik-" + derslikSay;
                                    ad = reader["DersAdi"].ToString();
                                    hoca = reader["HocaID"].ToString();
                                    teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                    uygulamali = reader["Uygulamali"].ToString(); u = Convert.ToInt32(uygulamali);
                                    dersSuresi = t + u;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                    if (bitis > 17)
                                    {
                                        derslikSay++;
                                        if (derslikSay > sinifSay)
                                        {
                                            derslikSay = 1;
                                            gun++;
                                            baslangic = 8;
                                            bitis = 0;
                                            k = 1;

                                        }
                                        derslikAdi = "Derslik-" + derslikSay;
                                        baslangic = 8;
                                        dersBaslama = baslangic + ":30";
                                        bitis = baslangic + dersSuresi;
                                        dersBitis = bitis + ":20";
                                        baslangic = baslangic + dersSuresi;
                                        k = 0;
                                    }


                                    //GUNLERIN BELIRLENMESI

                                    if (gun % 5 == 1) strGun = "Pazartesi";
                                    else if (gun % 5 == 2) strGun = "Salı";
                                    else if (gun % 5 == 3) strGun = "Çarşamba";
                                    else if (gun % 5 == 4) strGun = "Perşembe";
                                    else if (gun % 5 == 0) strGun = "Cuma";
                                    //EKRANDA GOSTERILECEK BILGILER
                                    string[] bilgiler = { ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                    ListViewItem lst = new ListViewItem(bilgiler);
                                    liste.Items.Add(lst);
                                }
                                if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                                reader.Close();
                            }
                        }
                    }
                if (cb_Secim.Text == "Teorik")
                {
                    if (sec == 1) //guz yariyili dersleri
                    {

                        for (i = 0; i < dersler.Length; i++)
                        {
                            if (tb_sinifSay.Text == "0")
                            {
                                MessageBox.Show("Derslik sayısı 0 olamaz.");
                                break;
                            }
                            if (tb_sinifSay.Text == "")
                            {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                            }
                            
                            id = dersler[i];
                            if (id == null) break;
                            cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=1 OR Yariyil=3 OR Yariyil=5 OR Yariyil=7) AND DersID=" + id);

                            cmd = new MySqlCommand(cmdid, conn);
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {


                                //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                derslikAdi = "Derslik-" + derslikSay;
                                ad = reader["DersAdi"].ToString();
                                hoca = reader["HocaID"].ToString();
                                teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                dersSuresi = t ;
                                dersBaslama = baslangic + ":30";
                                bitis = baslangic + dersSuresi;
                                dersBitis = bitis + ":20";
                                baslangic = baslangic + dersSuresi;
                                //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                if (bitis > 17)
                                {
                                    derslikSay++;
                                    if (derslikSay > sinifSay)
                                    {
                                        derslikSay = 1;
                                        gun++;
                                        baslangic = 8;
                                        bitis = 0;
                                        k = 1;

                                    }
                                    derslikAdi = "Derslik-" + derslikSay;
                                    baslangic = 8;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    k = 0;
                                }


                                //GUNLERIN BELIRLENMESI

                                if (gun % 5 == 1) strGun = "Pazartesi";
                                else if (gun % 5 == 2) strGun = "Salı";
                                else if (gun % 5 == 3) strGun = "Çarşamba";
                                else if (gun % 5 == 4) strGun = "Perşembe";
                                else if (gun % 5 == 0) strGun = "Cuma";
                                //EKRANDA GOSTERILECEK BILGILER
                                string[] bilgiler = { ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                ListViewItem lst = new ListViewItem(bilgiler);
                                liste.Items.Add(lst);
                            }
                            if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                            reader.Close();

                        }
                    }

                    if (sec == 2) //bahar yariyili dersleri
                    {

                        for (i = 0; i < dersler.Length; i++)
                        {
                            if (tb_sinifSay.Text == "0")
                            {
                                MessageBox.Show("Derslik sayısı 0 olamaz.");
                                break;
                            }
                            if (tb_sinifSay.Text == "")
                            {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                            }
                            id = dersler[i];
                            if (id == null) break;
                            cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=2 OR Yariyil=4 OR Yariyil=6 OR Yariyil=8) AND DersID=" + id);

                            cmd = new MySqlCommand(cmdid, conn);
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {


                                //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                derslikAdi = "Derslik-" + derslikSay;
                                ad = reader["DersAdi"].ToString();
                                hoca = reader["HocaID"].ToString();
                                teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                dersSuresi = t;
                                dersBaslama = baslangic + ":30";
                                bitis = baslangic + dersSuresi;
                                dersBitis = bitis + ":20";
                                baslangic = baslangic + dersSuresi;
                                //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                if (bitis > 17)
                                {
                                    derslikSay++;
                                    if (derslikSay > sinifSay)
                                    {
                                        derslikSay = 1;
                                        gun++;
                                        baslangic = 8;
                                        bitis = 0;
                                        k = 1;

                                    }
                                    derslikAdi = "Derslik-" + derslikSay;
                                    baslangic = 8;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    k = 0;
                                }


                                //GUNLERIN BELIRLENMESI

                                if (gun % 5 == 1) strGun = "Pazartesi";
                                else if (gun % 5 == 2) strGun = "Salı";
                                else if (gun % 5 == 3) strGun = "Çarşamba";
                                else if (gun % 5 == 4) strGun = "Perşembe";
                                else if (gun % 5 == 0) strGun = "Cuma";
                                //EKRANDA GOSTERILECEK BILGILER
                                string[] bilgiler = { ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                ListViewItem lst = new ListViewItem(bilgiler);
                                liste.Items.Add(lst);
                            }
                            if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                            reader.Close();
                        }
                    }
                }
                if (cb_Secim.Text == "Uygulama")
                {
                    if (sec == 1) //guz yariyili dersleri
                    {

                        for (i = 0; i < dersler.Length; i++)
                        {
                            if (tb_sinifSay.Text == "0")
                            {
                                MessageBox.Show("Derslik sayısı 0 olamaz.");
                                break;
                            }
                            if (tb_sinifSay.Text == "")
                            {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                            }
                            id = dersler[i];
                            if (id == null) break;
                            cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=1 OR Yariyil=3 OR Yariyil=5 OR Yariyil=7) AND DersID=" + id);

                            cmd = new MySqlCommand(cmdid, conn);
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {


                                //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                derslikAdi = "Derslik-" + derslikSay;
                                ad = reader["DersAdi"].ToString();
                                hoca = reader["HocaID"].ToString();
                                uygulamali = reader["Uygulamali"].ToString(); u = Convert.ToInt32(uygulamali);
                                teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                if (u == 0) { goto atla; }
                                dersSuresi = u;
                                dersBaslama = baslangic + ":30";
                                bitis = baslangic + dersSuresi;
                                dersBitis = bitis + ":20";
                                baslangic = baslangic + dersSuresi;
                                //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                if (bitis > 17)
                                {
                                    derslikSay++;
                                    if (derslikSay > sinifSay)
                                    {
                                        derslikSay = 1;
                                        gun++;
                                        baslangic = 8;
                                        bitis = 0;
                                        k = 1;

                                    }
                                    derslikAdi = "Derslik-" + derslikSay;
                                    baslangic = 8;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    k = 0;
                                }


                                //GUNLERIN BELIRLENMESI

                                if (gun % 5 == 1) strGun = "Pazartesi";
                                else if (gun % 5 == 2) strGun = "Salı";
                                else if (gun % 5 == 3) strGun = "Çarşamba";
                                else if (gun % 5 == 4) strGun = "Perşembe";
                                else if (gun % 5 == 0) strGun = "Cuma";
                                //EKRANDA GOSTERILECEK BILGILER
                                string[] bilgiler = { ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                ListViewItem lst = new ListViewItem(bilgiler);
                                liste.Items.Add(lst);
                            atla:;

                            }
                            if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                            reader.Close();

                        }
                    }

                    if (sec == 2) //bahar yariyili dersleri
                    {
                        for (i = 0; i < dersler.Length; i++)
                        {
                            if (tb_sinifSay.Text == "0")
                            {
                                MessageBox.Show("Derslik sayısı 0 olamaz.");
                                break;
                            }
                            if (tb_sinifSay.Text == "")
                            {
                                MessageBox.Show("Derslik sayısı yazmalısınız. Derslik sayısı boş bırakılamaz.");
                                break;
                            }
                            id = dersler[i];
                            if (id == null) break;
                            cmdid = ("SELECT * FROM okulveritabani.dersler WHERE (Yariyil=2 OR Yariyil=4 OR Yariyil=6 OR Yariyil=8) AND DersID=" + id);

                            cmd = new MySqlCommand(cmdid, conn);
                            reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {


                                //OKUMA SIRASINDA YAPILACAK İŞLEMLER
                                derslikAdi = "Derslik-" + derslikSay;
                                ad = reader["DersAdi"].ToString();
                                hoca = reader["HocaID"].ToString();
                                teorik = reader["Teorik"].ToString(); t = Convert.ToInt32(teorik);
                                uygulamali = reader["Uygulamali"].ToString(); u = Convert.ToInt32(uygulamali);
                                if (u==0) { goto atla2; }
                                dersSuresi =  u;
                                dersBaslama = baslangic + ":30";
                                bitis = baslangic + dersSuresi;
                                dersBitis = bitis + ":20";
                                baslangic = baslangic + dersSuresi;
                                //EGER SAAT 17'YI GEÇERSE DERS BİR SONRAKİ GÜNE ALINMALI


                                if (bitis > 17)
                                {
                                    derslikSay++;
                                    if (derslikSay > sinifSay)
                                    {
                                        derslikSay = 1;
                                        gun++;
                                        baslangic = 8;
                                        bitis = 0;
                                        k = 1;

                                    }
                                    derslikAdi = "Derslik-" + derslikSay;
                                    baslangic = 8;
                                    dersBaslama = baslangic + ":30";
                                    bitis = baslangic + dersSuresi;
                                    dersBitis = bitis + ":20";
                                    baslangic = baslangic + dersSuresi;
                                    k = 0;
                                }


                                //GUNLERIN BELIRLENMESI

                                if (gun % 5 == 1) strGun = "Pazartesi";
                                else if (gun % 5 == 2) strGun = "Salı";
                                else if (gun % 5 == 3) strGun = "Çarşamba";
                                else if (gun % 5 == 4) strGun = "Perşembe";
                                else if (gun % 5 == 0) strGun = "Cuma";
                                //EKRANDA GOSTERILECEK BILGILER
                                string[] bilgiler = { id, ad, hoca, derslikAdi, dersBaslama, dersBitis, strGun };
                                ListViewItem lst = new ListViewItem(bilgiler);
                                liste.Items.Add(lst);
                            atla2:;
                            }
                            if (gun > 5)
                            {
                                MessageBox.Show("Ders programı hatalı lütfen sınıf sayısını arttırınız.");
                                break;
                            }

                            reader.Close();
                        }
                    }
                }

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

        private void tb_sinifSay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
