﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Proj_PB
{
    public partial class Form2 : Form
    {
        public string tabKom = "";
        public string tab = "Table";
        public int id = 0;
        public string AddMarka = "";
        public string AddModel = "";
        public string AddSilnik = "";
        public string AddKolor = "";
        public bool AddMetalic = false;
        public bool AddABS = false;
        public bool AddKlimatyzacja = false;
        public bool AddWspomaganie = false;
        public string AddZdjecie = "default.jpg";   

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string nazwa)
        {
            this.UserName = nazwa;
            InitializeComponent();

            string[] komis = { "komis Kraków", "komis Warszawa" };
            comboBox1.Items.AddRange(komis);
            comboBox1.SelectedIndex = 0;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private string UserName { get; set; }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            AddModel = textBox2.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 a1 = new Form1(UserName);
            a1.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logowanie a1 = new Logowanie();
            a1.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                tabKom = comboBox1.GetItemText(comboBox1.SelectedIndex);

                switch (tabKom)
                {
                    case "0":
                        {
                            tab = "Table";
                            break;
                        }
                    case "1":
                        {
                            tab = "Table_1";
                            break;
                        }
                };

                reload_DGV();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bug Report");
            }
}


        public void reload_DGV()
        {
            try
            { 
                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                SqlCommand com = new SqlCommand("SELECT * FROM [dbo].[" + tab + "]", conn);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                adapter.Update(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Id"].Visible = false;
                dataGridView1.Columns["zdjecie"].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bug Report");
            }
}



        private void button2_Click(object sender, EventArgs e)
        {
            try 
            { 
                SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                conn1.Open();
                SqlCommand command = new SqlCommand("DELETE [dbo].[" + tab + "] where Id = '" + id + "'", conn1);
                command.ExecuteNonQuery();
                conn1.Close();
                MessageBox.Show("Pojazd został poprawnie usunięty z bazy");
                reload_DGV();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bug Report");
            }
}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue);
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (AddMarka.Equals("") || AddModel.Equals("") || AddSilnik.Equals("") || AddKolor.Equals(""))
                {
                    MessageBox.Show("Proszę o uzupełnienie danych");
                }
                else
                {            
                SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                SqlCommand command = new SqlCommand("INSERT INTO dbo.[" + tab + "] (marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie)" +
                " VALUES ('" + AddMarka + "', '" + AddModel + "', '" + AddSilnik + "', '" + AddKolor + "', '" + AddMetalic + "', '" + AddABS + "', '" + AddKlimatyzacja + "', '" + AddWspomaganie + "', '" + AddZdjecie + "')", conn1);
                conn1.Open();
                command.ExecuteNonQuery();
                conn1.Close();
                conn1.Dispose();

                MessageBox.Show("Pojazd został dodany do bazy!");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                AddZdjecie = "";

                reload_DGV();     
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bug Report");
            }
}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AddMarka = textBox1.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            AddSilnik = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            AddKolor = textBox4.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                AddMetalic = true;
            }
            else
            {
                AddMetalic = false;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                AddABS = true;
            }
            else
            {
                AddABS = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                AddKlimatyzacja = true;
            }
            else
            {
                AddKlimatyzacja = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                AddWspomaganie = true;
            }
            else
            {
                AddWspomaganie = false;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "photos");  
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Jpg files (*.jpg) | *.jpg";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    AddZdjecie = openFileDialog1.SafeFileName;
                    AddZdjecie = AddZdjecie.Replace(@"\\", @"\");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bug Report");
            }
        }
    }
}
