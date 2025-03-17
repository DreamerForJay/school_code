using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s1121454_hw1
{
    public partial class s1121454 : Form
    {
        public s1121454()
        {
            InitializeComponent();
        }


        private void btnOpen(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox3.Image = new Bitmap(dlg.FileName);
            }

        }

        private Bitmap RotateImageByMatrix(Bitmap source, float angle)
        {
            // 將角度轉換為弧度
            double radians = angle * Math.PI / 180;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            // 計算旋轉後新圖片的寬高
            int newWidth = (int)(Math.Abs(source.Width * cos) + Math.Abs(source.Height * sin));
            int newHeight = (int)(Math.Abs(source.Width * sin) + Math.Abs(source.Height * cos));

            // 創建一個新的空白位圖
            Bitmap rotated = new Bitmap(newWidth, newHeight);

            // 設置解析度以保持原始質量
            rotated.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            // 中心點的座標
            float centerX = source.Width / 2f;
            float centerY = source.Height / 2f;
            float newCenterX = newWidth / 2f;
            float newCenterY = newHeight / 2f;

            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    // 計算原始圖像中的對應位置
                    int xOrig = (int)((x - newCenterX) * cos + (y - newCenterY) * sin + centerX);
                    int yOrig = (int)((y - newCenterY) * cos - (x - newCenterX) * sin + centerY);

                    // 如果原始座標在圖像範圍內，則取其像素，否則設為背景色
                    if (xOrig >= 0 && xOrig < source.Width && yOrig >= 0 && yOrig < source.Height)
                    {
                        // 取原圖像素，並將其轉換為灰階
                        Color originalColor = source.GetPixel(xOrig, yOrig);
                        int grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);
                        Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);

                        rotated.SetPixel(x, y, grayColor);
                    }
                    else
                    {
                        // 可選：設置為白色或其他背景色
                        rotated.SetPixel(x, y, Color.White);
                    }
                }
            }

            return rotated;
        }

        private Bitmap ApplyGrayScale(Bitmap source)
        {
            Bitmap grayScaled = new Bitmap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    Color originalColor = source.GetPixel(x, y);
                    int grayValue = (int)(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B);
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayScaled.SetPixel(x, y, grayColor);
                }
            }
            return grayScaled;
        }

        private void btnRotation(object sender, EventArgs e)
        {
            if (this.pictureBox3.Image != null)
            {
                string input = this.textBox2.Text;
                float angle;

                if (float.TryParse(input, out angle))
                {
                    if (angle == 360)
                    {
                        angle = 0;
                    }
                    // 旋轉並灰階化圖片
                    this.pictureBox4.Image = RotateImageByMatrix((Bitmap)this.pictureBox3.Image, angle);
                    this.pictureBox4.Refresh();
                }
                else if (input.Equals("Mirror", StringComparison.OrdinalIgnoreCase))
                {
                    // 左右鏡像翻轉
                    Bitmap mirrored = new Bitmap((Bitmap)this.pictureBox3.Image);
                    mirrored.RotateFlip(RotateFlipType.RotateNoneFlipX); // 左右翻轉

                    // 將鏡像圖像轉換為灰階
                    Bitmap grayMirrored = ApplyGrayScale(mirrored);

                    this.pictureBox4.Image = grayMirrored;
                    this.pictureBox4.Refresh();
                }
                else if (input.Equals("Flip", StringComparison.OrdinalIgnoreCase))
                {
                    // 上下翻轉
                    Bitmap flipped = new Bitmap((Bitmap)this.pictureBox3.Image);
                    flipped.RotateFlip(RotateFlipType.RotateNoneFlipY); // 上下翻轉

                    // 將翻轉圖像轉換為灰階
                    Bitmap grayFlipped = ApplyGrayScale(flipped);

                    this.pictureBox4.Image = grayFlipped;
                    this.pictureBox4.Refresh();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

