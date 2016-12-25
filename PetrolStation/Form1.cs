using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetrolStation
{
    public partial class Form1 : Form
    {
        StationContext db;
        AutoContext db2;
        public Form1()
        {
            InitializeComponent();

            db = new StationContext();
            db2 = new AutoContext();
            db.Stations.Load();
            db2.AutomobileSet.Load();

            dataGridView1.DataSource = db.Stations.Local.ToBindingList();
            dataGridView2.DataSource = db2.AutomobileSet.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)//добавити заправку
        {
            AddStation AdSt = new AddStation();
            DialogResult result = AdSt.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Station station = new Station();
            station.Name = AdSt.textBox1.Text;
            station.Adress = AdSt.textBox2.Text;
            station.Volume_92 = Convert.ToDouble(AdSt.textBox3.Text);
            station.Volume_95 = Convert.ToDouble(AdSt.textBox4.Text);
            station.Volume_Dp = Convert.ToDouble(AdSt.textBox5.Text);
            station.Price_92 = Convert.ToDouble(AdSt.textBox6.Text);
            station.Price_95 = Convert.ToDouble(AdSt.textBox7.Text);
            station.Price_Dp = Convert.ToDouble(AdSt.textBox8.Text);

            db.Stations.Add(station);
            db.SaveChanges();
            MessageBox.Show("Добавлено нову заправку");
        }

        private void button3_Click(object sender, EventArgs e)//редагувати дані
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Station station = db.Stations.Find(id);
                AddStation AdSt = new AddStation();

                AdSt.textBox1.Text = station.Name;
                AdSt.textBox2.Text = station.Adress;
                AdSt.textBox3.Text = Convert.ToString(station.Volume_92);
                AdSt.textBox4.Text = Convert.ToString(station.Volume_95);
                AdSt.textBox5.Text = Convert.ToString(station.Volume_Dp);
                AdSt.textBox6.Text = Convert.ToString(station.Price_92);
                AdSt.textBox7.Text = Convert.ToString(station.Price_95);
                AdSt.textBox8.Text = Convert.ToString(station.Price_Dp);


                DialogResult result = AdSt.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                station.Name = AdSt.textBox1.Text;
                station.Adress = AdSt.textBox2.Text;
                station.Volume_92 = Convert.ToDouble(AdSt.textBox3.Text);
                station.Volume_95 = Convert.ToDouble(AdSt.textBox4.Text);
                station.Volume_Dp = Convert.ToDouble(AdSt.textBox5.Text);
                station.Price_92 = Convert.ToDouble(AdSt.textBox6.Text);
                station.Price_95 = Convert.ToDouble(AdSt.textBox7.Text);
                station.Price_Dp = Convert.ToDouble(AdSt.textBox8.Text);
                
                db.SaveChanges();
                dataGridView1.Refresh();
                MessageBox.Show("Дані змінено");
            }
        }

        private void button2_Click(object sender, EventArgs e)//видалити заправку
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;

                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Station station = db.Stations.Find(id);
                db.Stations.Remove(station);
                db.SaveChanges();

                MessageBox.Show("заправку видалено");
            }
        }

        private void button4_Click(object sender, EventArgs e)//створити автомобіль
        {
            AutomobilForm autoform = new AutomobilForm();
            DialogResult result = autoform.ShowDialog(this);
            if (result == DialogResult.Cancel)
             return;
            Auto auto = new Auto();
            auto.SerialNumber = autoform.textBox1.Text;
            auto.Number = autoform.textBox2.Text;
            auto.Petrol = autoform.comboBox1.SelectedItem.ToString();
            auto.TankVolume = Convert.ToDouble(autoform.textBox3.Text);
            auto.CurrentVolume = Convert.ToDouble(autoform.textBox4.Text);

            db2.AutomobileSet.Add(auto);
            db2.SaveChanges();
            MessageBox.Show("Добавлено автомобіль");
        }
        private void button5_Click(object sender, EventArgs e)//видалити автомобіль
        {
            if (dataGridView2.SelectedRows.Count >0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;

                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Auto auto = db2.AutomobileSet.Find(id);
                db2.AutomobileSet.Remove(auto);
                db2.SaveChanges();
                MessageBox.Show("автомобіль видалено");
            }
        }

        private void button6_Click(object sender, EventArgs e)//заправити автомобіль
        {
            double liter = Convert.ToDouble(textBox3.Text);
            
            if ((dataGridView1.SelectedRows.Count > 0)&&(dataGridView2.SelectedRows.Count > 0))
            {
                int index1 = dataGridView1.SelectedRows[0].Index;
                int id1 = 0;

                int index2 = dataGridView2.SelectedRows[0].Index;
                int id2 = 0;

                bool converted1 = Int32.TryParse(dataGridView1[0, index1].Value.ToString(), out id1);
                if (converted1 == false)
                    return;

                bool converted2 = Int32.TryParse(dataGridView2[0, index2].Value.ToString(), out id2);
                if (converted2 == false)
                    return;

                Station station = db.Stations.Find(id1);
                Auto auto = db2.AutomobileSet.Find(id2);

                if (auto.Petrol == "А92")
                {
                    station.Volume_92 -= liter;
                    auto.CurrentVolume += liter;
                }
                if (auto.Petrol == "А95")
                {
                    station.Volume_95 -= liter;
                    auto.CurrentVolume += liter;
                }
                if (auto.Petrol == "ДП")
                {
                    station.Volume_Dp -= liter;
                    auto.CurrentVolume += liter;
                }
                db.SaveChanges();
                db2.SaveChanges();
                dataGridView1.Refresh();
                dataGridView2.Refresh();
            }
        }
    }
}
