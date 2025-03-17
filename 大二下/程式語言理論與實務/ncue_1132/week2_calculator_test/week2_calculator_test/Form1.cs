using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace week2_calculator_test
{

    public partial class Form1 : Form
    {
        private List<string> expression = new List<string>(); // 儲存完整數學式
        private double n1 = 0, n2 = 0; // 儲存第一個數與第二個數
        private char op = ' '; // 儲存當前的運算符 (+, -, *, /)
        private bool num = true; // 是否清空輸入框
        private bool point = false; // 是否已輸入小數點
        private bool pre = false; // 是否已輸入數字
        private bool equ = false; // 是否已經執行過計算

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            n1 = 0; n2 = 0;
            op = ' ';
            num = true;
            point = false;
            pre = false;
            equ = false;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (num == true)

                textBox1.Text = "0.";
            else if (point == false)
                textBox1.Text += ".";
            point = true;
            num = false;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "1";
            else
                textBox1.Text += "1";
            num = false;
            equ = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "2";
            else
                textBox1.Text += "2";
            num = false;
            equ = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "3";
            else
                textBox1.Text += "3";
            num = false;
            equ = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "4";
            else
                textBox1.Text += "4";
            num = false;
            equ = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "5";
            else
                textBox1.Text += "5";
            num = false;
            equ = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "6";
            else
                textBox1.Text += "6";
            num = false;
            equ = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "7";
            else
                textBox1.Text += "7";
            num = false;
            equ = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "8";
            else
                textBox1.Text += "8";
            num = false;
            equ = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "9";
            else
                textBox1.Text += "9";
            num = false;
            equ = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (num == true || textBox1.Text == "0")
                textBox1.Text = "0";
            else
                textBox1.Text += "0";
            num = false;
            equ = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (!equ) // 只有當 equ 為 false 時才更新 n2，避免多次計算錯誤
                    n2 = Double.Parse(textBox1.Text);

                double result = n1;

                switch (op)
                {
                    case '+':
                        result += n2;
                        break;
                    case '-':
                        result -= n2;
                        break;
                    case '*':
                        result *= n2;
                        break;
                    case '/':
                        if (n2 == 0)
                            throw new Exception("除數不能為零！");
                        result /= n2;
                        break;
                }

                textBox1.Text = result.ToString();
                n1 = result; // ✅ 這行確保運算結果存回 n1，允許後續繼續運算
                num = true;
                equ = true;  // ✅ 這行確保等號按下後能繼續運算
                pre = false; // ✅ 避免影響 `PerformCalculation()`
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
            }
        }
        private void PerformCalculation()
        {
            if (pre)  // 確保有輸入數字
            {
                n2 = Double.Parse(textBox1.Text); // 更新 n2
                button12_Click(null, null); // 執行計算
                equ = false;
            }
        }


        private void button13_Click(object sender, EventArgs e)
        {
            PerformCalculation();
            op = '+';
            if (!pre || equ) // 如果 equ 為 true，則繼續使用 `n1`
            {
                n1 = Double.Parse(textBox1.Text);
            }
            num = true;
            point = false;
            equ = false;
            pre = true;


        }

        private void button14_Click(object sender, EventArgs e)
        {
            PerformCalculation();
            op = '-';
            if (!pre || equ) // 只有當 `pre` 為 `false` 時才更新 n1，避免覆蓋
                n1 = Double.Parse(textBox1.Text);
            num = true;
            point = false;
            equ = false;
            pre = true;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            PerformCalculation();
            op = '*';
            if (!pre || equ) // 只有當 `pre` 為 `false` 時才更新 n1，避免覆蓋
                n1 = Double.Parse(textBox1.Text);
            num = true;
            point = false;
            equ = false;
            pre = true;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            PerformCalculation();
            op = '/';
            if(!pre || equ) // 只有當 `pre` 為 `false` 時才更新 n1，避免覆蓋
                n1 = Double.Parse(textBox1.Text);
            num = true;
            point = false;
            equ = false;
            pre = true;
        }

        
    }
}