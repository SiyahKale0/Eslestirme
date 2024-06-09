using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Eslestirme
{
    public partial class Form1 : Form
    {
        List<string> emojiler = new List<string>()
        {
            "😀", "🐶", "🐸", "🐰", "🌻", "🌵", "🏡", "🍂",
            "😀", "🐶", "🐸", "🐰", "🌻", "🌵", "🏡", "🍂"
        };
        int ciftSayisi;
        int eslesen;

        Label ilkTıklanan = null;
        Label ikinciTıklanan = null;
        int kalanSure = 60;
        bool oyunDevamEdiyor = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            oyunuBaslatDugmesi.Enabled = true;
        }

        private void oyunuBaslatDugmesi_Click(object sender, EventArgs e)
        {
            // Oyunu başlat düğmesini devre dışı bırak
            oyunuBaslatDugmesi.Enabled = false;

            // Oyunu başlat
            KarelereEmojileriAta();
            oyunZamanlayici.Start();

            kalanSure = 60; // Süreyi sıfırla
            sureLabel.Text = kalanSure.ToString();
            oyunDevamEdiyor = true;
        }

        private void KarelereEmojileriAta()
        {
            eslesen = 0;
            ciftSayisi = emojiler.Count/2;

            Random rastgele = new Random();

            foreach (Control kontrol in tabloPaneli.Controls)
            {
                Label emojiEtiketi = kontrol as Label;
                if (emojiEtiketi != null)
                {
                    int rastgeleSayi = rastgele.Next(emojiler.Count);
                    string seçilenEmoji = emojiler[rastgeleSayi];
                    emojiEtiketi.Text = seçilenEmoji;
                    emojiEtiketi.ForeColor = emojiEtiketi.BackColor;
                    emojiler.RemoveAt(rastgeleSayi);
                }
            }
        }

        private void Etiket_Click(object sender, EventArgs e)
        {
            if (!oyunDevamEdiyor)
                return;

            if (zamanlayici.Enabled == true)
                return;

            Label tıklananEtiket = sender as Label;

            if (tıklananEtiket != null)
            {
                if (tıklananEtiket.ForeColor == Color.Black)
                    return;

                if (ilkTıklanan == null)
                {
                    ilkTıklanan = tıklananEtiket;
                    ilkTıklanan.ForeColor = Color.Black;
                    return;
                }

                ikinciTıklanan = tıklananEtiket;
                ikinciTıklanan.ForeColor = Color.Black;

                if (ilkTıklanan.Text == ikinciTıklanan.Text)
                {
                    ilkTıklanan.BackColor = Color.Transparent;
                    ikinciTıklanan.BackColor = Color.Transparent;
                    eslesen++;
                    ilkTıklanan = null;
                    ikinciTıklanan = null;
                    KazananıKontrolEt();
                    return;
                }

                zamanlayici.Start();
            }
        }

        private void zamanlayici_Tick(object sender, EventArgs e)
        {
            zamanlayici.Stop();

            ilkTıklanan.ForeColor = ilkTıklanan.BackColor;
            ikinciTıklanan.ForeColor = ikinciTıklanan.BackColor;

            ilkTıklanan = null;
            ikinciTıklanan = null;
        }

        private void oyunZamanlayici_Tick(object sender, EventArgs e)
        {
            kalanSure--;
            sureLabel.Text = kalanSure.ToString();

            if (kalanSure <= 0)
            {
                oyunZamanlayici.Stop();
                MessageBox.Show("Süre doldu! Kaybettiniz.", "Oyun Bitti");
                OyunuSifirla();
            }
        }

        private void KazananıKontrolEt()
        {
            if (eslesen == ciftSayisi)
            {
                oyunZamanlayici.Stop();
                MessageBox.Show("Tebrikler! Tüm eşleşmeleri buldunuz.", "Oyun Bitti");
                OyunuSifirla();
            }
        }

        private void OyunuSifirla()
        {
            // Emojiler listesini sıfırla
            emojiler = new List<string>()
            {
                "😀", "🐶", "🐸", "🐰", "🌻", "🌵", "🏡", "🍂",
                "😀", "🐶", "🐸", "🐰", "🌻", "🌵", "🏡", "🍂"
            };

            // Kartların içeriğini sıfırla
            foreach (Control kontrol in tabloPaneli.Controls)
            {
                Label emojiEtiketi = kontrol as Label;
                if (emojiEtiketi != null)
                {
                    emojiEtiketi.Enabled = true;
                    emojiEtiketi.Text = "";
                    emojiEtiketi.ForeColor = Color.Black;
                    emojiEtiketi.BackColor = Color.LightBlue;
                }
            }

            // Oyunu başlat düğmesini etkinleştir
            oyunuBaslatDugmesi.Enabled = true;

            // Süreyi sıfırla
            kalanSure = 60;
            sureLabel.Text = kalanSure.ToString();

            oyunDevamEdiyor = false;
        }
    }
}
