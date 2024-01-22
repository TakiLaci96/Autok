using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    public partial class FormAuto : Form
    {
        string muvelet;
        public FormAuto(string muvelet)
        {
            InitializeComponent();
            this.muvelet = muvelet;
        }

        private void FormAuto_Load(object sender, EventArgs e)
        {
            switch (muvelet)
            {
                case "add":
                    this.Text = "Új autó";
                    button_Muvelet.Text = "Rögzítés";
                    button_Muvelet.BackColor = Color.LightGreen;
                    button_Muvelet.Click += new EventHandler(autoHozzaad);
                    break;
                case "edit":
                    this.Text = "Módosítás";
                    button_Muvelet.Text = "Módosítás";
                    button_Muvelet.BackColor = Color.LightSkyBlue;
                    button_Muvelet.Click += new EventHandler(autoModositas);
                    adatmezokFeltoltese();
                    break;
                case "delete":
                    this.Text = "Törlés";
                    button_Muvelet.Text = "Törlés";
                    button_Muvelet.BackColor = Color.Red;
                    button_Muvelet.Click += new EventHandler(autoTorles);
                    adatmezokFeltoltese();
                    break;
            }
        }

        private void adatmezokFeltoltese()
        {
            Auto auto = (Auto)Program.formNyito.listBox_AutokLista.SelectedItem;
            textBox_Rendszam.Text = auto.Rendszam.ToString();
            textBox_Marka.Text = auto.Marka.ToString();
            textBox_Modell.Text = auto.Modell.ToString();
            numericUpDown_GyartasiEv.Value = (decimal)auto.GyartasiEv;
            dateTimePicker_ForgErv.Value = auto.ForgalmiErvenyesseg;
            numericUpDown_VetelAr.Value = (decimal)auto.VetelAr;
            numericUpDown_KmAllas.Value = (decimal)auto.KmAllas;
            numericUpDown_Hengerurtartalom.Value = (decimal)auto.Hengerurtartalom;
            numericUpDown_Tomeg.Value = (decimal)auto.Tomeg;
            numericUpDown_Teljesitmeny.Value = (decimal)auto.Teljesitmeny;

        }

        private void autoTorles(object sender, EventArgs e)
        {
            Auto auto = deleteAuto();
            Program.db.autoTorles(auto);
            this.Close();
        }

        private Auto deleteAuto()
        {
            Auto torlesreVaroAuto = (Auto)Program.formNyito.listBox_AutokLista.SelectedItem;
            DialogResult result = MessageBox.Show("Biztos törölni szeretné az autót?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Program.db.autoTorles(torlesreVaroAuto);
                return torlesreVaroAuto;
            }
            else
            {
                Program.db.autoTorles(null);
            }
            return null;

        }

        private void autoModositas(object sender, EventArgs e)
        {

            Auto auto = editAuto();
            Program.db.autoModositas(auto);
            this.Close();

        }

        private Auto editAuto()
        {
            Auto kivalasztottAuto = (Auto)Program.formNyito.listBox_AutokLista.SelectedItem;
            kivalasztottAuto.Rendszam = textBox_Rendszam.Text;
            kivalasztottAuto.Marka = textBox_Marka.Text;
            kivalasztottAuto.Modell = textBox_Modell.Text;
            kivalasztottAuto.GyartasiEv = (int)numericUpDown_GyartasiEv.Value;
            kivalasztottAuto.ForgalmiErvenyesseg = (DateTime)dateTimePicker_ForgErv.Value;
            kivalasztottAuto.VetelAr = (int)numericUpDown_VetelAr.Value;
            kivalasztottAuto.KmAllas = (int)numericUpDown_KmAllas.Value;
            kivalasztottAuto.Hengerurtartalom = (int)numericUpDown_Hengerurtartalom.Value;
            kivalasztottAuto.Tomeg = (int)numericUpDown_Tomeg.Value;
            kivalasztottAuto.Teljesitmeny = (int)numericUpDown_Teljesitmeny.Value;

            return kivalasztottAuto;
        }

        private Auto addAuto()
        {
            Auto auto = new Auto();
            auto.Rendszam = textBox_Rendszam.Text;
            auto.Marka = textBox_Marka.Text;
            auto.Modell = textBox_Modell.Text;
            auto.GyartasiEv = (int)numericUpDown_GyartasiEv.Value;
            auto.GyartasiEv = (int)numericUpDown_GyartasiEv.Value;
            auto.ForgalmiErvenyesseg = (DateTime)dateTimePicker_ForgErv.Value;
            auto.VetelAr = (int)numericUpDown_VetelAr.Value;
            auto.KmAllas = (int)numericUpDown_KmAllas.Value;
            auto.Hengerurtartalom = (int)numericUpDown_Hengerurtartalom.Value;
            auto.Tomeg = (int)numericUpDown_Tomeg.Value;
            auto.Teljesitmeny = (int)numericUpDown_Teljesitmeny.Value;

            return auto;
        }
        private void autoHozzaad(object sender, EventArgs e)
        {
            Auto auto = addAuto();
            Program.db.autoHozzaadas(auto);
            this.Close();

        }

        private void button_Muvelet_Click(object sender, EventArgs e)
        {

        }
    }
}
