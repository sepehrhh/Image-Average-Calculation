using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management.Instrumentation;
using System.Drawing.Imaging;

namespace Image_Average_Calculation
{
    public class ParallelProcessor
    {
        private readonly Image Image1;
        private readonly Image Image2;
        /// <summary>
        /// number of CPU cores used for parallel processing
        /// </summary>
        private readonly int Cores;

        public ParallelProcessor(string imagePath1, string imagePath2)
        {
            Image1 = Image.FromFile(imagePath1);
            Image2 = Image.FromFile(imagePath2);

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                Cores += int.Parse(item["NumberOfCores"].ToString());
            }
        }


        public void Process()
        {
            Size avgImageSize = new Size(Math.Min(Image1.Width, Image2.Width),
                Math.Min(Image1.Height, Image2.Height));

            using (Bitmap bmp1 = new Bitmap(Image1, avgImageSize.Width,
                avgImageSize.Height))
            using (Bitmap bmp2 = new Bitmap(Image2, avgImageSize.Width,
                avgImageSize.Height))
            using (Bitmap bmpAvg = new Bitmap(avgImageSize.Width,
                avgImageSize.Height))
            {
                ParallelProcess(bmp1, bmp2, bmpAvg, avgImageSize);
            }
        }


        private unsafe void ParallelProcess(Bitmap image1,
            Bitmap image2,
            Bitmap avgImage,
            Size avgImageSize)
        {
            BitmapData imageData1 = image1.LockBits(new Rectangle(0, 0,
                image1.Width, image1.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            BitmapData imageData2 = image2.LockBits(new Rectangle(0, 0,
                image2.Width, image2.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            BitmapData imageDataAvg = avgImage.LockBits(new Rectangle(0, 0,
                avgImage.Width, avgImage.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int bytesPerPixel = 3;

            byte* scan0_1 = (byte*)imageData1.Scan0.ToPointer();
            byte* scan0_2 = (byte*)imageData2.Scan0.ToPointer();
            byte* scan0_avg = (byte*)imageDataAvg.Scan0.ToPointer();


            int stride1 = imageData1.Stride;
            int stride2 = imageData2.Stride;
            int strideAvg = imageDataAvg.Stride;

            Task[] tasks = new Task[this.Cores];
            var height = avgImageSize.Height / this.Cores;
            for (int i = 0; i < tasks.Length; i++)
            {
                var ii = i;
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    for (int y = ii * height; y < (ii + 1) * height; y++)
                    {
                        byte* row1 = scan0_1 + (y * stride1);
                        byte* row2 = scan0_2 + (y * stride2);
                        byte* rowRes = scan0_avg + (y * strideAvg);

                        for (int x = 0; x < avgImageSize.Width; x++)
                        {
                            int bIndex = x * bytesPerPixel;
                            int gIndex = bIndex + 1;
                            int rIndex = bIndex + 2;

                            byte pixelR1 = row1[rIndex];
                            byte pixelR2 = row2[rIndex];
                            byte pixelG1 = row1[gIndex];
                            byte pixelG2 = row2[gIndex];
                            byte pixelB1 = row1[bIndex];
                            byte pixelB2 = row2[bIndex];

                            rowRes[rIndex] = (byte)((pixelR1 + pixelR2) / 2);
                            rowRes[bIndex] = (byte)((pixelB1 + pixelB2) / 2);
                            rowRes[gIndex] = (byte)((pixelG1 + pixelG2) / 2);
                        }
                    }
                });
                
            }
            Task.WaitAll(tasks);
            image1.UnlockBits(imageData1);
            image2.UnlockBits(imageData2);
            avgImage.UnlockBits(imageDataAvg);
            avgImage.Save("../../AvgImage.jpeg", ImageFormat.Jpeg);
        }

    }
}

