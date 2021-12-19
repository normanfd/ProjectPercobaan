using Entity;
using System;
using System.Diagnostics;
using System.Linq;

namespace Action
{
    public static class OtherAction
    {
        public static void Parse_Exact_Datetime()
        {
            DateTime now = DateTime.Now;
            string nowString = now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime oDate = DateTime.ParseExact(nowString, "yyyy-MM-dd HH:mm:ss", null);
            Console.WriteLine(oDate);
        }
        public static void Absolute_Number()
        {
            decimal valueA, valueB;
            valueA = 6099300.29M;
            valueB = 6379493.15050000003M;
            Console.WriteLine("A :" + Math.Abs(valueA - valueB));                           // 280192,86050000003
            Console.WriteLine("B :" + Math.Abs(valueA - valueB));                           // 280192,86050000003
            Console.WriteLine("C :" + (valueA - valueB));                                   // 280192,86050000003
            Console.WriteLine("D :" + Math.Abs(valueA - Math.Abs(Math.Round(valueB, 2))));  // 280192,86050000003
        }
        public static void StopWatch()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int a = 0;
            for (int i = 0; i < 10000; i++)
            {
                a++;
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
        public static void Group_By_With_Linq()
        {
            var data = DummyDataEndorsementHistoryDetail.ListDetailEndorsementHistoryDetail();
            data = data.Where(x => x.DataBenarDesc != x.DataSalahDesc).ToList();
            //var testContain = data.Where(x => x.EndorsItem.Contains("Serial No 1")).Select(c => c.DataBenarDesc).FirstOrDefault() ?? "";
            //var testEqual = data.Where(x => x.EndorsItem == "Serial No 1").Select(c => c.DataBenarDesc).FirstOrDefault() ?? "";
            var testGroupBy = data.GroupBy(x => new { x.EndorsItem, x.DataBenarDesc }).Select(x => x.First()).ToList();
            Console.WriteLine(testGroupBy.Count);
        }
    }
}
