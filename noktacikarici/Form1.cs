using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.Util;


namespace noktacikarici
{
    public partial class Form1 : Form
    {
        private Bitmap orijinalResim;
        private Bitmap cizilmisBitmap;
        private string secilenDosyaYolu;
        Image<Gray, byte> edges;
        private int noktaSayi = 0;

        public Form1()
        {

            InitializeComponent();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Bir resim seçin";
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Resmi yükle
                orijinalResim = new Bitmap(ofd.FileName);

                // PictureBox'a resmi ata
                pictureBox1.Image = orijinalResim;

                // PictureBox sığdırma ayarı (bir kez Form_Load'da da verebilirsin)
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                // Dosya yolunu göster
                txtDosyaYolu.Text = ofd.FileName;

                secilenDosyaYolu = ofd.FileName;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            txtDosyaYolu.Enabled = false;
        }

        private void btnNoktaOlustur_Click(object sender, EventArgs e)
        {

            if (noktaSayi == 0)
            {
                noktaSayi = 500;
            }
            if (txtNoktaSayisi.Text != "")
            {
                int sayi = Convert.ToInt16(txtNoktaSayisi.Text);
                noktaSayi = sayi;
            }

            if (orijinalResim == null)
            {
                MessageBox.Show("Lütfen önce bir resim seçin.");
                return;
            }

            // ✅ Direkt dosya yolundan oku (Hatasız!)
            Image<Bgr, byte> imageCV = new Image<Bgr, byte>(secilenDosyaYolu);

            // 2. Griye çevir ve kenarları bul
            Image<Gray, byte> gray = imageCV.Convert<Gray, byte>();
            edges = gray.Canny(100, 200);

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(edges, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            List<Point> allSampledPoints = new List<Point>();

            // Her kontur için orantılı örnekleme yap
            int toplamKonturUzunlugu = 0;
            List<VectorOfPoint> konturListesi = new List<VectorOfPoint>();

            for (int i = 0; i < contours.Size; i++)
            {
                double len = CvInvoke.ArcLength(contours[i], false);
                if (len > 50) // Gürültüyü azaltmak için kısa konturları atla
                {
                    toplamKonturUzunlugu += (int)len;
                    konturListesi.Add(contours[i]);
                }
            }

            // Her konturdan orantılı sayıda nokta örnekle
            foreach (var kontur in konturListesi)
            {
                List<Point> konturNoktalari = kontur.ToArray().ToList();
                double konturUzunluk = CvInvoke.ArcLength(new VectorOfPoint(konturNoktalari.ToArray()), false);
                int buKonturIcinNoktaSayisi = (int)((konturUzunluk / toplamKonturUzunlugu) * noktaSayi);

                double adim = konturNoktalari.Count / (double)Math.Max(1, buKonturIcinNoktaSayisi);
                for (int i = 0; i < buKonturIcinNoktaSayisi; i++)
                {
                    int index = (int)(i * adim);
                    if (index < konturNoktalari.Count)
                        allSampledPoints.Add(konturNoktalari[index]);
                }
            }

            // Eğer nokta yetmezse en büyük konturden doldur
            if (allSampledPoints.Count < noktaSayi)
            {
                var eksik = noktaSayi - allSampledPoints.Count;
                var enBuyukKontur = konturListesi.OrderByDescending(c => CvInvoke.ArcLength(c, false)).FirstOrDefault();
                var ekNoktalar = enBuyukKontur?.ToArray()?.Take(eksik).ToList();
                if (ekNoktalar != null)
                    allSampledPoints.AddRange(ekNoktalar);
            }

            // Sonuç listesi
            List<Point> sampledPoints = allSampledPoints;


            
            int genislik, yukseklik;

            if (rbtnA4.Checked)
            {
                genislik = 2480;    // A4 boyutları - 300 DPI
                yukseklik = 3508;
            }
            else
            {
                genislik = 3508;    // A3 boyutları - 300 DPI
                yukseklik = 4961;
            }

            // 8. Yeni bitmap oluştur ve noktaları çiz
            Bitmap sonuc = new Bitmap(genislik, yukseklik);
            using (Graphics g = Graphics.FromImage(sonuc))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 10, FontStyle.Bold);
                float oranX = (float)genislik / edges.Width;
                float oranY = (float)yukseklik / edges.Height;

                int index = 1;
                foreach (Point p in sampledPoints)
                {
                    float x = p.X * oranX;
                    float y = p.Y * oranY;

                    g.FillEllipse(Brushes.Black, x - 5, y - 5, 10, 10);
                    g.DrawString(index.ToString(), font, Brushes.Black, x + 10, y + 10);
                    index++;
                }
            }

            // 6. PictureBox'ta göster
            pictureBox1.Image = sonuc;

            // 7. Dışa aktar için kaydet
            cizilmisBitmap = sonuc;
        }

        private void btnDisaAktar_Click(object sender, EventArgs e)
        {
            if (cizilmisBitmap == null)
            {
                MessageBox.Show("Önce bir çizim oluşturun.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Çizimi Kaydet";
            sfd.Filter = "PNG Dosyası|*.png";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cizilmisBitmap.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Çizim başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void txtNoktaSayisi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
