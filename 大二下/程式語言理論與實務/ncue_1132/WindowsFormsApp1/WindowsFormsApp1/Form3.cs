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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            total = 0; // 確保 total 初始為 0
            textBox4.Text = "NT $ " + total.ToString(); // 顯示初始金額
        }

        public Form2 preForm;
        public int total = 0;

        private void Button_Click(object sender, EventArgs e)
        {
            this.preForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.preForm.preForm.Close();
            this.preForm.Close();
            this.Close();
        }

        /// <summary>
        /// 更新 total 金額
        /// </summary>
        private void UpdateTotal()
        {
            total = 0; // 重新計算 total，確保從 0 開始

            // 計算 CheckBox 被選中的金額
            if (checkBox1.Checked) total += 69;
            if (checkBox2.Checked) total += 49;
            if (checkBox3.Checked) total += 59;
            if (checkBox4.Checked) total += 79;

            // 計算 CheckBox8 (附加選項)
            if (checkBox8.Checked)
            {
                if (radioButton1.Checked) total += 35;
                else if (radioButton2.Checked) total += 45;
            }

            // 計算 CheckBox9 (附加選項)
            if (checkBox9.Checked)
            {
                if (radioButton3.Checked) total += 25;
                else if (radioButton4.Checked) total += 35;
                else if (radioButton5.Checked) total += 45;
            }

            // 更新 TextBox 顯示
            textBox4.Text = "NT $ " + total.ToString();
        }

        // ✅ 確保 CheckBox 和 RadioButton 變更時都會更新 total
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void checkBox2_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            UpdateTotal();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = true;
            }
            else
            {
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
            }
            UpdateTotal();
        }

        // ✅ 當選擇 RadioButton 時，也應該更新金額
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }
        private void radioButton5_CheckedChanged(object sender, EventArgs e) { UpdateTotal(); }

        // 不需要 textBox4_TextChanged 事件來更新金額，因此讓它留空
        private void textBox4_TextChanged(object sender, EventArgs e) { }
    }
}
