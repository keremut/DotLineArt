using Emgu.CV.Structure;
using Emgu.CV;

namespace noktacikarici
{
    public partial class Form1 : Form
    {
        private Bitmap orijinalResim;
        private Bitmap cizilmisBitmap;
        private string secilenDosyaYolu;
        Image<Gray, byte> edges;
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
            if (orijinalResim == null)
            {
                MessageBox.Show("Lütfen önce bir resim seçin.");
                return;
            }

            // ✅ Direkt dosya yolundan oku (Hatasız!)
            Image<Bgr, byte> imageCV = new Image<Bgr, byte>(secilenDosyaYolu);

            // 2. Griye çevir ve kenarları bul
            Image<Gray, byte> gray = imageCV.Convert<Gray, byte>();
            Image<Gray, byte> edges = gray.Canny(100, 200);

            // 3. Kenar piksellerini al
            List<Point> edgePoints = new List<Point>();
            for (int y = 0; y < edges.Height; y++)
            {
                for (int x = 0; x < edges.Width; x++)
                {
                    if (edges.Data[y, x, 0] > 0)
                        edgePoints.Add(new Point(x, y));
                }
            }

            // 4. Seyrekleştirme (örnek: her 10. noktayı al)
            //List<Point> sampledPoints = new List<Point>();
            //for (int i = 0; i < edgePoints.Count; i += 40)
            //    sampledPoints.Add(edgePoints[i]);
            List<Point> sampledPoints = new List<Point>();
            int hedefNoktaSayisi = 1000;

            if (edgePoints.Count < hedefNoktaSayisi)
            {
                sampledPoints = edgePoints;
            }
            else
            {
                double adim = (double)edgePoints.Count / hedefNoktaSayisi;

                for (int i = 0; i < hedefNoktaSayisi; i++)
                {
                    int index = (int)(i * adim);
                    sampledPoints.Add(edgePoints[index]);
                }
            }


            // 5. Yeni Bitmap'e noktaları ve numaraları çiz
            //Bitmap sonuc = new Bitmap(edges.Width, edges.Height);
            //using (Graphics g = Graphics.FromImage(sonuc))
            //{
            //    g.Clear(Color.White);
            //    Font font = new Font("Arial", 14);
            //    Brush brush = Brushes.Black;
            //    int index = 1;
            //    foreach (Point p in sampledPoints)
            //    {
            //        g.FillEllipse(brush, p.X - 2, p.Y - 2, 4, 4); // nokta
            //        g.DrawString(index.ToString(), font, brush, p.X + 4, p.Y + 4); // numara
            //        index++;
            //    }
            //}
            //int a4Genislik = 2480;
            //int a4Yukseklik = 3508;
            //Bitmap sonuc = new Bitmap(a4Genislik, a4Yukseklik);

            //using (Graphics g = Graphics.FromImage(sonuc))
            //{
            //    g.Clear(Color.White);
            //    Font font = new Font("Arial", 6);
            //    float oranX = (float)a4Genislik / edges.Width;
            //    float oranY = (float)a4Yukseklik / edges.Height;

            //    int index = 1;
            //    foreach (Point p in sampledPoints)
            //    {
            //        float x = p.X * oranX;
            //        float y = p.Y * oranY;

            //        g.FillEllipse(Brushes.Black, x - 2, y - 2, 4, 4);
            //        g.DrawString(index.ToString(), font, Brushes.Black, x + 4, y + 4);
            //        index++;
            //    }
            //}

            int a3Genislik = 3508;
            int a3Yukseklik = 4961;
            Bitmap sonuc = new Bitmap(a3Genislik, a3Yukseklik);

            using (Graphics g = Graphics.FromImage(sonuc))
            {
                g.Clear(Color.White);
                Font font = new Font("Arial", 12, FontStyle.Bold);
                float oranX = (float)a3Genislik / edges.Width;
                float oranY = (float)a3Yukseklik / edges.Height;

                int index = 1;
                foreach (Point p in sampledPoints)
                {
                    float x = p.X * oranX;
                    float y = p.Y * oranY;

                    g.FillEllipse(Brushes.Black, x - 6, y - 6, 12, 12);
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

        private void btnCizimiGoster_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu Özellik Geliştirilecek");
        }
    }
}
