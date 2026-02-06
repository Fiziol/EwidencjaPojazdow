using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EwidencjaPojazdow
{
    public partial class Form1 : Form
    {
        private BindingList<Pojazd> listaPojazdow = new BindingList<Pojazd>();
        private DataGridView dgv = new DataGridView();
        private TextBox txtNr = new TextBox { Text = "Nr Rej" };
        private TextBox txtMarka = new TextBox { Text = "Marka" };
        private Button btnDodaj = new Button { Text = "DODAJ POJAZD", Top = 100 };
        private Button btnRaport = new Button { Text = "RAPORT (LINQ)", Top = 130 };

        public Form1()
        {
            this.Width = 600;
            // Układanie okienek na szybko
            var flow = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 100 };
            flow.Controls.AddRange(new Control[] { txtNr, txtMarka });
            this.Controls.Add(btnRaport);
            this.Controls.Add(btnDodaj);
            this.Controls.Add(flow);
            this.Controls.Add(dgv);
            dgv.Dock = DockStyle.Bottom;
            dgv.Height = 200;

            btnDodaj.Click += (s, e) =>
            {
                listaPojazdow.Add(new Pojazd
                {
                    NrRejestracyjny = txtNr.Text,
                    Marka = txtMarka.Text,
                    DataPrzegladu = DateTime.Now // Domyślnie dziś
                });
                dgv.DataSource = null;
                dgv.DataSource = listaPojazdow;
            };

            // ETAP 2: Raport za pomocą LINQ
            btnRaport.Click += (s, e) =>
            {
                var doPrzegladu = listaPojazdow
                    .Where(p => p.DataPrzegladu.Month == DateTime.Now.Month)
                    .ToList();

                string msg = "Pojazdy do przeglądu w tym miesiącu:\n";
                doPrzegladu.ForEach(p => msg += $"- {p.NrRejestracyjny} ({p.Marka})\n");
                MessageBox.Show(msg, "Raport RDLC/LINQ");
            };
        }
    }

    public class Pojazd
    {
        public string NrRejestracyjny { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int RokProdukcji { get; set; }
        public DateTime DataPrzegladu { get; set; }
        public string Wlasciciel { get; set; } = "Jan Kowalski";
    }
}