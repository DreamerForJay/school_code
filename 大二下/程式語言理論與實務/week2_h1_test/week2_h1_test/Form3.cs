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
    public partial class Form3 : Form
    {
        public Form2 preForm;
        private int totalPrice = 0;  // 總價
        private int friesPrice = 0;  // 薯條價格
        private int drinkPrice = 0;  // 飲料價格

        public Form3()
        {
            InitializeComponent();
            textBox1.Text = "0";  // 初始設定為 0

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) totalPrice += 79;
            else totalPrice -= 79;
            UpdateTotalPrice();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                totalPrice -= friesPrice;
                friesPrice = 25;
                totalPrice += friesPrice;
            }
            UpdateTotalPrice();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.preForm.preForm.Close();
            this.preForm.Close();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.preForm.Show();
            this.Close();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = true;
            }
            else
            {
                totalPrice -= drinkPrice;
                drinkPrice = 0;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
            }
            UpdateTotalPrice();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                totalPrice -= friesPrice;  // 先扣除舊價格
                friesPrice = 35;
                totalPrice += friesPrice;
            }
            UpdateTotalPrice();
        }

        private void checkBox7_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                totalPrice -= friesPrice;
                friesPrice = 0;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            UpdateTotalPrice();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                totalPrice -= drinkPrice;
                drinkPrice = 35;
                totalPrice += drinkPrice;
            }
            UpdateTotalPrice();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                totalPrice -= drinkPrice;
                drinkPrice = 25;
                totalPrice += drinkPrice;
            }
            UpdateTotalPrice();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                totalPrice -= drinkPrice;
                drinkPrice = 45;
                totalPrice += drinkPrice;
            }
            UpdateTotalPrice();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void UpdateTotalPrice()
        {
            textBox1.Text = totalPrice.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) totalPrice += 69;
            else totalPrice -= 69;
            UpdateTotalPrice();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) totalPrice += 49;
            else totalPrice -= 49;
            UpdateTotalPrice();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) totalPrice += 59;
            else totalPrice -= 59;
            UpdateTotalPrice();
        }
    }
}
