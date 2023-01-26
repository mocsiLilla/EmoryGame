using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmoryGame
{
    class Kártya : PictureBox
    {

        public int kártyaSzám;
        public Kártya(int sor, int oszlop, int kártyaSzám)
        {
            Top = sor * (Properties.Settings.Default.Méret + 10);
            Left = oszlop * (Properties.Settings.Default.Méret + 10);
            Height = Properties.Settings.Default.Méret;
            Width = Properties.Settings.Default.Méret;
            
            this.kártyaSzám = kártyaSzám;
            
            Lefordít();

        }

        private void Kártya_Click(object sender, EventArgs e)
        {
            Felfordít();
        }

        public void Felfordít()
        {
            Load($"./kepek/nagy/{kártyaSzám}@2x.png");
        }

        public void Lefordít()
        {
            Load("./kepek/nagy/card_back@2x.png");
        }
    }
}
