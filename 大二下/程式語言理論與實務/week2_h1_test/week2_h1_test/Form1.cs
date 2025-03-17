using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week2_h1_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {


            if (textBox5.Text == "0919")
            {
                textBox6.Visible = true;
                label6.Visible = true;

            }
            else
            {
                textBox6.Visible = false;
                label6.Visible = false;


            }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == "0919")
            {
                button3.Enabled = true;

            }
            else

            {
                button3.Enabled = false;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.preForm = this;
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}


