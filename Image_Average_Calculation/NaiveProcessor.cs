using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Average_Calculation
{
    public class NaiveProcessor
    {
        private readonly Image Image1;
        private readonly Image Image2;

        public NaiveProcessor(string imagePath1, string imagePath2)
        {
            Image1 = Image.FromFile(imagePath1);
            Image2 = Image.FromFile(imagePath2);
        }

        public void Process()
        {
            var avgImageWidth = Math.Min(Image1.Width, Image2.Width);
            var avgImageHeight = Math.Min(Image1.Height, Image2.Height);

            using (Bitmap bmp1 = new Bitmap(Image1, avgImageWidth, avgImageHeight))
            using (Bitmap bmp2 = new Bitmap(Image2, avgImageWidth, avgImageHeight))
            using (Bitmap bmp3 = new Bitmap(avgImageWidth, avgImageHeight))
            {
                for (int x = 0; x < avgImageWidth; x++)
                {
                    for (int y = 0; y < avgImageHeight; y++)
                    {
                        Color pxl1 = bmp1.GetPixel(x, y);
                        Color pxl2 = bmp2.GetPixel(x, y);
                        int avg = (pxl1.R + pxl2.R) / 2;
                        int avg1 = (pxl1.G + pxl2.G) / 2;
                        int avg2 = (pxl1.B + pxl2.B) / 2;

                        Color redPxl = Color.FromArgb(avg, avg1, avg2);

                        bmp3.SetPixel(x, y, redPxl);
                    }
                }
            }
        }
    }
}
