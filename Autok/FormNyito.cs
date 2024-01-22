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
    public partial class FormNyito : Form
    {
        public FormNyito()
        {
            InitializeComponent();
        }

        private void FormNyito_Load(object sender, EventArgs e)
        {
            // Márkák megjelenítése a panel ablakban
            foreach (string marka in Program.autok.Select(a => a.Marka).Distinct())
            {
                CheckBox cb = new CheckBox();
                cb.Text = marka;
                cb.Checked = true;
                cb.Location = new Point(10, panel_Markak.Controls.Count * 20);
                cb.CheckedChanged += new EventHandler(markaValtozott);
                panel_Markak.Controls.Add(cb);
            }
            updateAutokLista();
        }

        private void markaValtozott(object sender, EventArgs e)
        {
            updateAutokLista();
        }

        // Csak azokat jeleníti meg ahol a CheckBox ki van pipálva.
        private void updateAutokLista()
        {
            listBox_AutokLista.Items.Clear();
            List<string> kivalasztottak = new List<string>();
            foreach (CheckBox item in panel_Markak.Controls)
            {
                if (item.Checked) kivalasztottak.Add(item.Text);
            }

            foreach (Auto item in Program.autok)
            {
                if (kivalasztottak.Contains(item.Marka))
                {
                    listBox_AutokLista.Items.Add(item);
                }  
            }
        }
        // Új autó felvétele, kattintási esemény
        private void újToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAuto formAuto = new FormAuto("add");
            formAuto.ShowDialog();
            updateAutokLista();  
        }

        private void módosítToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox_AutokLista.SelectedIndex < 0)
            {
                MessageBox.Show("Nincs kiválasztott elem!");
                return;
            }
            FormAuto formAuto = new FormAuto("edit");
            formAuto.ShowDialog();
            updateAutokLista();
        }

        private void törölToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox_AutokLista.SelectedIndex < 0)
            {
                MessageBox.Show("Nincs kiválasztott elem!");
                return;
            }
            FormAuto formAuto = new FormAuto("delete");
            formAuto.ShowDialog();
        }
    }
}
