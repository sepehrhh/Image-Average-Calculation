# Image-Average-Calculation
The project is a simple C# code to manipulate image. The code gets two images and produces an average image of them by getting average value of each pixel.
The NaiveProcessor class implements the algorithm using System.Drawing Bitmap class GetPixel and SetPixel methods that let you acquire and change color of chosen pixels. Those methods are very easy to use but are also extremely slow.
ParallelProcessing class code makes use of Bitmap.LockBits method that is a wrapper for native GdipBitmapLockBits (GDI+, gdiplus.dll) function. LockBits creates a temporary buffer that contains pixel information in desired format (in our case RGB, 8 bits per color component). Any changes to this buffer are copied back to the bitmap upon UnlockBits call.
Bitmap.LockBits returns BitmapData object (System.Drawing.Imaging namespace) that has two interesting properties: Scan0 and Stride. Scan0 returns an address of the first pixel data. Stride is the width of single row of pixels (scan line) in bytes (with optional padding to make it dividable by 4).
Code marked with unsafe keyword allows C# program to take advantage of pointer arithmetic.
For making pixel manipulation even faster ParallelProcessor class processes different parts of the image in parallel. The class has Cores property which sets with number of system's CPU cores. The ParallelProcess method initiates a Task array with length of number of CPU's cores and manipulates pixels of different parts of the image in parallel.
These are the test results executed on MSI GE620 DX laptop: Intel Core i7-7500U 2.70GHz (2 cores, 4 logical processors), 12GB DDR4 RAM, NVIDIA GeForce 940MX 2GB GDDR5, Windows 10 Pro x64:

                                1080p                    4K                       8K
Naive Processor:               2540ms         -        10757ms         -        45708ms
Fast Parallel Processor:  118ms(21X Faster)   -    370ms(29X Faster)   -   1444ms(31X Faster)
