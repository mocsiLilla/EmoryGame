using System;
using System.Drawing;
using System.Windows.Forms;

namespace EmoryGame
{
    public partial class Form1 : Form
    {
        Kártya utolsóKártya;
        int felfordítva = 0;
        int nehézség;
        int idő;
        int jó = 0;
        int rossz = 0;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Formázás

            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.háttérkép);
            panel1.Width = ClientRectangle.Width;
            panel1.Height = ClientRectangle.Height - label2.Height;
            textBox1.Left = ClientRectangle.Width - textBox1.Width / 2;
            textBox2.Left = ClientRectangle.Width - textBox1.Width;
            label1.Left = textBox2.Left - label1.Width / 2 + textBox2.Width / 2 - 10;
            label2.AutoSize = true;
            label2.MaximumSize = new Size(350, 0);
            label2.Left = 0;
            label2.Top = 0;
            b1.Left = label2.Width + b1.Width;
            b2.Left = label2.Width + 2 * b1.Width + b1.Width / 2;
            b3.Left = label2.Width + 4 * b1.Width;

            MessageBox.Show("A játék elkezdéséhez válassza ki hány kártyával szeretne játszani!");

            KártyaKirakás();
        }

        //Kártyák kirakása
        private void KártyaKirakás()
        {

            panel1.Controls.Clear();

            int Kártyaszám = 6 * nehézség;
            int összkártya = 50;
            int mozgástér = rnd.Next(összkártya - Kártyaszám - 1);

            int[] t = Keverés(6 * nehézség, mozgástér + 1);

            int sorszám = 0;

            b1.Click += B1_Click;
            b2.Click += B2_Click;
            b3.Click += B3_Click;

            for (int s = 0; s < nehézség; s++)
            {
                for (int o = 0; o < 6; o++)
                {
                    Kártya k = new Kártya(s, o, t[sorszám]);

                    k.Left = ((k.Width + 10) * o + ClientRectangle.Width / 2 - 6 * (k.Width + 10) / 2);
                    k.Top = ((k.Height + 10) * s + panel1.Height / 2 - nehézség * (k.Height + 10) / 2);

                    panel1.Controls.Add(k);

                    sorszám++;

                    k.Click += K_Click;

                }
            }

        }

        //24 db-os mező
        private void B3_Click(object sender, EventArgs e)
        {
            idő = 0;
            timer1.Enabled = true;
            nehézség = 4;
            jó = 0;
            rossz = 0;
            KártyaKirakás();
        }

        //18 db-os mező
        private void B2_Click(object sender, EventArgs e)
        {
            idő = 0;
            timer1.Enabled = true;
            nehézség = 3;
            jó = 0;
            rossz = 0;
            KártyaKirakás();
        }

        //12 db-os mező
        private void B1_Click(object sender, EventArgs e)
        {
            idő = 0;
            timer1.Enabled = true;
            nehézség = 2;
            jó = 0;
            rossz = 0;
            KártyaKirakás();
        }

        private void K_Click(object sender, EventArgs e)
        {
            if (sender is Kártya)
            {
                Kártya megkattintottKártya = (Kártya)sender;

                //Hibakezelés: ha ugyanarra a kártyára kattintok ne számítson találatnak

                if (megkattintottKártya == utolsóKártya) return;

                //visszaforgatás

                if (felfordítva == 2)
                {
                    foreach (var item in panel1.Controls)
                    {
                        if (item is Kártya)
                        {
                            ((Kártya)item).Lefordít();
                        }
                    }

                    felfordítva = 0;
                    rossz++;
                }

                felfordítva++;
                megkattintottKártya.Felfordít();

                //Párok eltüntetése

                if (utolsóKártya != null)
                {
                    if (megkattintottKártya.kártyaSzám == utolsóKártya.kártyaSzám && megkattintottKártya != utolsóKártya)
                    {
                        megkattintottKártya.Visible = false;
                        utolsóKártya.Visible = false;
                        jó++;

                    }
                }

                utolsóKártya = megkattintottKártya;

                textBox2.Text = (jó + " : " + rossz).ToString();

                if (jó == (6 * nehézség) / 2)
                {
                    MessageBox.Show("Ön nyert! :)");
                }

            }
        }

        //Kártyák megkeverése
        int[] Keverés(int n, int hánytól)
        {
            int[] tömb = new int[n];
            for (int i = 0; i < n / 2; i++)
            {
                tömb[i] = i + hánytól;
                tömb[i + n / 2] = i + hánytól;
            }

            for (int i = 0; i < n; i++)
            {
                int egyik = rnd.Next(n);
                int másik = rnd.Next(n);
                int köztes = tömb[egyik];
                tömb[egyik] = tömb[másik];
                tömb[másik] = köztes;
            }

            return tömb;
        }

        //Idő mérése
        private void timer1_Tick(object sender, EventArgs e)
        {
            idő++;

            TimeSpan ts = TimeSpan.FromSeconds(idő);

            textBox1.Text = ts.ToString();
        }
    }
}

