using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
public Form1 preForm;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = "1.上課認真聽\r\n2.作業認真寫\r\n3.晚上認真睡\r\n4.一天一程式，Fail遠離我";
         



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                button1.Enabled = true;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.preForm = this;
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.preForm.Show();
            this.Close();
        }
    }
}
