using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week4_hw_test
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

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "文字檔案(*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //產生檔案對話方塊，回傳DialogResult.OK代表使用者已選取檔案
                FileInfo f = new FileInfo(openFileDialog1.FileName);
                StreamReader sr = f.OpenText();
            }
        }

        private void 載入資料_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 儲存資料_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "純文字檔案 (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                string[,] record = new string[50, 101]; // 50名顧客，每位最多購買100個商品
                int recordNo = 0, i;
                int[] itemNum = new int[50]; // 記錄每位顧客購買的商品數量
                string[] input = null;

                textBox1.Text = "客戶 購買商品代號\r\n";

                while (sr.Peek() >= 0)
                {
                    input = sr.ReadLine().Split(); // 讀取一行並以空格拆分
                    itemNum[recordNo] = input.Length; // 記錄該顧客購買的商品數量

                    for (i = 0; i < input.Length; i++)
                    {
                        record[recordNo, i] = input[i]; // 存入購買商品紀錄
                        textBox1.Text += input[i] + " "; // 顯示在 UI 上
                    }

                    textBox1.Text += "\r\n";
                    recordNo++;
                }

                sr.Close();
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
            {

            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

