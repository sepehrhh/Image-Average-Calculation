using Microsoft.VisualStudio.TestTools.UnitTesting;
using Image_Average_Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Image_Average_Calculation.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// Naive algorithm test for 1080p images
        /// </summary>
        [TestMethod()]
        public void Naive1080pImageProcessingTest()
        {
            var url1 = @"../../TestData/1080p/1080p-TD1.jpg";
            var url2 = @"../../TestData/1080p/1080p-TD2.jpg";
            var naiveProcessor = new NaiveProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            naiveProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Naive Processor Elapsed Time(1080p): {t.ToString()}ms");
        }

        /// <summary>
        /// Fast algorithm test for 1080p images
        /// </summary>
        [TestMethod()]
        public void Parallel1080pImageProcessingTest()
        {
            var url1 = @"../../TestData/1080p/1080p-TD1.jpg";
            var url2 = @"../../TestData/1080p/1080p-TD2.jpg";
            var parallelProcessor = new ParallelProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            parallelProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Parallel Processor Elapsed Time(1080p): {t.ToString()}ms");
        }

        /// <summary>
        /// Naive algorithm test for 4K images
        /// </summary>
        [TestMethod()]
        public void Naive4KImageProcessingTest()
        {
            var url1 = @"../../TestData/4k/4k-TD1.jpg";
            var url2 = @"../../TestData/4k/4k-TD2.jpg";
            var naiveProcessor = new NaiveProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            naiveProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Naive Processor Elapsed Time(4K): {t.ToString()}ms");
        }

        /// <summary>
        /// Fast algorithm test for 4K images
        /// </summary>
        [TestMethod()]
        public void Parallel4KImageProcessingTest()
        {
            var url1 = @"../../TestData/4k/4k-TD1.jpg";
            var url2 = @"../../TestData/4k/4k-TD2.jpg";
            var parallelProcessor = new ParallelProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            parallelProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Parallel Processor Elapsed Time(4K): {t.ToString()}ms");
        }

        /// <summary>
        /// Naive algorithm test for 8K images
        /// </summary>
        [TestMethod()]
        public void Naive8KImageProcessingTest()
        {
            var url1 = @"../../TestData/8k/8k-TD1.jpg";
            var url2 = @"../../TestData/8k/8k-TD2.jpg";
            var naiveProcessor = new NaiveProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            naiveProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Naive Processor Elapsed Time(8K): {t.ToString()}ms");

        }

        /// <summary>
        /// Fast algorithm test for 8K images
        /// </summary>
        [TestMethod()]
        public void Parallel8KImageProcessingTest()
        {
            var url1 = @"../../TestData/8k/8k-TD1.jpg";
            var url2 = @"../../TestData/8k/8k-TD2.jpg";
            var parallelProcessor = new ParallelProcessor(url1, url2);
            var sw = new Stopwatch();
            sw.Start();
            parallelProcessor.Process();
            sw.Stop();
            var t = sw.ElapsedMilliseconds;
            TestContext.WriteLine($"Parallel Processor Elapsed Time(8K): {t.ToString()}ms");
        }

        
    }
}