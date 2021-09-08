/***************************************************************************
**
**                          SAKARYA ÜNİVERSİTESİ
**               BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**                    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**                   NESNEYE DAYALI PROGRAMLAMA DERSİ
**				   	      2019-2020 BAHAR DÖNEMİ
**
**          ÖDEV NUMARASI.........: ODEV 3
**          ÖĞRENCİ ADI...........: EMİRHAN ŞENSOY
**          ÖĞRENCİ NUMARASI......: G191210040
**          DERSİN ALINDIĞI GRUP..: 2B
**
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G191210040
{
    public partial class Form1 : Form
    {
        public Buzdolabi buzdolabi = new Buzdolabi("Buzdolabi", "Arcelik", "RL67H", "Ucuz", 1600, "270L", "A+", 0);
        public LedTv led = new LedTv("LED TV", "Samsung", "UE-43", "Curved", 2500, 43, "3840 x 2160", 0);
        public Laptop laptop = new Laptop("Laptop", "Asus", "FX50", "Gaming", 5000, 512, 8, 10000, 15.6, "1920 x 1080", 0);
        public CepTel tel = new CepTel("Cep Telefonu", "Xiaomi", "Red Mi Note 8", "F/P", 1500, 64, 4, 4000, 0);

        public Form1()
        {
            InitializeComponent();

            label1.Text = led.HamFiyat.ToString();
            label2.Text = led.StokAdedi.ToString();
            label3.Text = buzdolabi.HamFiyat.ToString();
            label4.Text = buzdolabi.StokAdedi.ToString();
            label5.Text = laptop.HamFiyat.ToString();
            label6.Text = laptop.StokAdedi.ToString();
            label7.Text = tel.HamFiyat.ToString();
            label8.Text = tel.StokAdedi.ToString();
        }

        private void SepeteUrunEkle(Urun urun)
        {
            //Parametre olarak alinan urunu siparis ozetindeki listbox'lara ekliyor.
            if(urun.SecilenAdet > 0)
            {
                lbx_Adet.Items.Add(urun.SecilenAdet);
                lbx_Urun.Items.Add(urun.Ad);
                lbx_KdvliFiyat.Items.Add(urun.KdvUygula());
            }
        }

        private void btn_sepeteEkle_Click(object sender, EventArgs e)
        {
            //Kullanici yanlis deger girdiginde tekrar secim yapmasi icin kullanilan dongu.
            while(true)
            {
                //Kullanicinin stok adetinden fazla siparis vermemesi icin hata mesaji cikartiliyor.
                if (Convert.ToInt32(nud_Led.Value) > led.StokAdedi)
                {
                    MessageBox.Show("Lutfen LED TV'nin stok adedine dikkat ediniz!");
                    break;
                }
                //Kullanicinin stok adetinden fazla siparis vermemesi icin hata mesaji cikartiliyor.
                if (Convert.ToInt32(nud_Buzdolabi.Value) > buzdolabi.StokAdedi)
                {
                    MessageBox.Show("Lutfen buzdolabinin stok adedine dikkat ediniz!");
                    break;
                }
                //Kullanicinin stok adetinden fazla siparis vermemesi icin hata mesaji cikartiliyor.
                if (Convert.ToInt32(nud_Laptop.Value) > laptop.StokAdedi)
                {
                    MessageBox.Show("Lutfen laptop'un stok adedine dikkat ediniz!");
                    break;
                }
                //Kullanicinin stok adetinden fazla siparis vermemesi icin hata mesaji cikartiliyor.
                if (Convert.ToInt32(nud_Tel.Value) > tel.StokAdedi)
                {
                    MessageBox.Show("Lutfen telefonun stok adedine dikkat ediniz!");
                    break;
                }

                lbx_Adet.Items.Clear();
                lbx_KdvliFiyat.Items.Clear();
                lbx_Urun.Items.Clear();



                led.SecilenAdet = Convert.ToInt32(nud_Led.Value);
                buzdolabi.SecilenAdet = Convert.ToInt32(nud_Buzdolabi.Value);
                laptop.SecilenAdet = Convert.ToInt32(nud_Laptop.Value);
                tel.SecilenAdet = Convert.ToInt32(nud_Tel.Value);

                Sepet sepet = new Sepet(led, buzdolabi, laptop, tel);

                SepeteUrunEkle(led);
                SepeteUrunEkle(buzdolabi);
                SepeteUrunEkle(laptop);
                SepeteUrunEkle(tel);

                label9.Text = sepet.kdvliFiyatToplam.ToString();

                led.StokAdedi -= led.SecilenAdet;
                buzdolabi.StokAdedi -= buzdolabi.SecilenAdet;
                laptop.StokAdedi -= laptop.SecilenAdet;
                tel.StokAdedi -= tel.SecilenAdet;

                label2.Text = led.StokAdedi.ToString();
                label4.Text = buzdolabi.StokAdedi.ToString();
                label6.Text = laptop.StokAdedi.ToString();
                label8.Text = tel.StokAdedi.ToString();

                led.StokAdedi += led.SecilenAdet;
                buzdolabi.StokAdedi += buzdolabi.SecilenAdet;
                laptop.StokAdedi += laptop.SecilenAdet;
                tel.StokAdedi += tel.SecilenAdet;

                break;
            }
            
        }

        private void btn_sepetiTemizle_Click(object sender, EventArgs e)
        {
            lbx_Adet.Items.Clear();
            lbx_Urun.Items.Clear();
            lbx_KdvliFiyat.Items.Clear();
            label9.Text = "";

            label2.Text = led.StokAdedi.ToString();
            label4.Text = buzdolabi.StokAdedi.ToString();
            label6.Text = laptop.StokAdedi.ToString();
            label8.Text = tel.StokAdedi.ToString();

        }
    }

    public class Sepet
    {
        public double kdvliFiyatLed;
        public double kdvliFiyatBuzdolabi;
        public double kdvliFiyatLaptop;
        public double kdvliFiyatTel;
        public double kdvliFiyatToplam;

        public Sepet(LedTv le1, Buzdolabi bu1, Laptop la1, CepTel ce1)
        {
            kdvliFiyatLed = le1.KdvUygula();
            kdvliFiyatBuzdolabi = bu1.KdvUygula();
            kdvliFiyatLaptop = la1.KdvUygula();
            kdvliFiyatTel = ce1.KdvUygula();
            kdvliFiyatToplam = kdvliFiyatLed + kdvliFiyatBuzdolabi + kdvliFiyatLaptop + kdvliFiyatTel;
        }

        public void SepeteUrunEkle(Urun urun)
        {
            //
            //   ?????
            //
        }
    }

    public class Urun
    {
        public string Ad;
        public string Marka;
        public string Model;
        public string Ozellik;
        public int StokAdedi;
        public double HamFiyat;
        public int SecilenAdet;

        public virtual double KdvUygula()
        {
            return HamFiyat * 1 * SecilenAdet;
        }

        public int RastgeleSayiGetir(int sayi)
        {
            Random rastgele = new Random();

            int[] dizi = new int[5];

            //Diziye rastgele sayilar ataniyor.
            for(int i = 0; i < 5; i++)
            {
                dizi[i] = rastgele.Next(1, 100);
            }

            return dizi[sayi];
        }

    }

    public class Buzdolabi : Urun
    {
        public string IcHacim;
        public string EnerjiSinifi;

        public Buzdolabi(string name, string brand, string model, string feature, double rawPrice, string volume, string energyClass, int chosenNumber)
        {
            Ad = name;
            Marka = brand;
            Model = model;
            Ozellik = feature;
            HamFiyat = rawPrice;
            StokAdedi = RastgeleSayiGetir(1);
            SecilenAdet = 0;

            IcHacim = volume;
            EnerjiSinifi = energyClass;
            SecilenAdet = chosenNumber;
        }

        public override double KdvUygula()
        {
            return HamFiyat * 1.05 * SecilenAdet;
        }
    }

    public class LedTv : Urun
    {
        public double EkranBoyutu;
        public string EkranCozunurlugu;

        public LedTv(string name, string brand, string model, string feature, double rawPrice, double screenSize, string resolution, int chosenNumber)
        {
            Ad = name;
            Marka = brand;
            Model = model;
            Ozellik = feature;
            HamFiyat = rawPrice;
            StokAdedi = RastgeleSayiGetir(2);
            SecilenAdet = 0;

            EkranBoyutu = screenSize;
            EkranCozunurlugu = resolution;
            SecilenAdet = chosenNumber;
        }

        public override double KdvUygula()
        {
            return HamFiyat * 1.18 * SecilenAdet;
        }
    }

    public class CepTel : Urun
    {
        public double DahiliHafiza;
        public double RamKapasitesi;
        public double PilGucu;

        public CepTel(string name, string brand, string model, string feature, double rawPrice, double internalMemory, double RAMcapacity, double batteryPower, int chosenNumber)
        {
            Ad = name;
            Marka = brand;
            Model = model;
            Ozellik = feature;
            HamFiyat = rawPrice;
            StokAdedi = RastgeleSayiGetir(3);
            SecilenAdet = 0;

            DahiliHafiza = internalMemory;
            RamKapasitesi = RAMcapacity;
            PilGucu = batteryPower;
            SecilenAdet = chosenNumber;
        }

        public override double KdvUygula()
        {
            return HamFiyat * 1.2 * SecilenAdet;
        }
    }

    public class Laptop : Urun
    {
        public double EkranBoyutu;
        public string EkranCozunurluk;
        public double DahiliHafiza;
        public double RamKapasitesi;
        public double PilGucu;

        public Laptop(string name, string brand, string model, string feature, double rawPrice, double internalMemory, double RAMcapacity, double batteryPower, double screenSize, string resolution, int chosenNumber)
        {
            Ad = name;
            Marka = brand;
            Model = model;
            Ozellik = feature;
            HamFiyat = rawPrice;
            StokAdedi = RastgeleSayiGetir(4);
            SecilenAdet = 0;

            DahiliHafiza = internalMemory;
            RamKapasitesi = RAMcapacity;
            PilGucu = batteryPower;
            EkranBoyutu = screenSize;
            EkranCozunurluk = resolution;
            SecilenAdet = chosenNumber;
        }

        public override double KdvUygula()
        {
            return HamFiyat * 1.15 * SecilenAdet;
        }
    }
}
