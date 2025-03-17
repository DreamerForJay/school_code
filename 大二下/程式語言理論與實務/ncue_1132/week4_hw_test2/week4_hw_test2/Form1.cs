using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week4_hw_test2
{
    public partial class Form1 : Form
    {
        private Dictionary<string, HashSet<string>> customerPurchases = new Dictionary<string, HashSet<string>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "純文字檔案 (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        textBox1.Clear();
                        textBox1.AppendText("客戶 購買商品代號\r\n");

                        customerPurchases.Clear(); // 清空舊數據

                        while (!sr.EndOfStream)
                        {
                            string[] input = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (input.Length < 2) continue; // 確保至少有一個商品

                            string customerID = input[0];
                            HashSet<string> items = new HashSet<string>(input.Skip(1));

                            customerPurchases[customerID] = items;

                            textBox1.AppendText(customerID + " " + string.Join(" ", items) + "\r\n");
                        }
                    }

                    // 執行協同過濾推薦
                    PerformRecommendation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("讀取檔案錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void PerformRecommendation()
        {
            textBox2.Clear();
            textBox2.AppendText("客戶 相似客戶 推薦商品代號\r\n");

            foreach (var customer in customerPurchases)
            {
                string customerID = customer.Key;
                HashSet<string> customerItems = customer.Value;
                List<string> bestMatches = new List<string>();
                int maxCommonItems = 0;
                Dictionary<string, HashSet<string>> matchRecommendations = new Dictionary<string, HashSet<string>>();



                foreach (var otherCustomer in customerPurchases)
                {
                    if (customerID == otherCustomer.Key) continue;

                    HashSet<string> otherItems = otherCustomer.Value;
                    int commonItems = customerItems.Intersect(otherItems).Count();
                    if (commonItems > maxCommonItems)
                    {
                        maxCommonItems = commonItems;
                        bestMatches.Clear(); // 發現更高相似度時，清空先前的相似顧客
                        bestMatches.Add(otherCustomer.Key);
                        matchRecommendations.Clear();
                        matchRecommendations[otherCustomer.Key] = new HashSet<string>(otherItems.Except(customerItems));
                    }
                    else if (commonItems == maxCommonItems && commonItems > 0)
                    {
                        bestMatches.Add(otherCustomer.Key);
                        matchRecommendations[otherCustomer.Key] = new HashSet<string>(otherItems.Except(customerItems));
                    }

                }

                if (bestMatches.Count > 0)
                {
                    HashSet<string> recommendedItems = new HashSet<string>();
                    foreach (var match in bestMatches)
                    {
                        recommendedItems.UnionWith(matchRecommendations[match]);
                    }

                    if (recommendedItems.Count > 0)
                    {
                        textBox2.AppendText($"{customerID} ({string.Join(" ", bestMatches)}) {string.Join(" ", recommendedItems)}\r\n");
                    }
                    else
                    {
                        textBox2.AppendText($"{customerID} ({string.Join(", ", bestMatches)}) 無推薦\r\n");
                    }
                }

                else
                {
                    textBox2.AppendText($"{customerID} 無推薦\r\n");
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
