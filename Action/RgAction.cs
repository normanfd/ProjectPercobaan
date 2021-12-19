using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action
{
    public static class RgAction
    {
        public static void RG3()
        {
            string inputNumber = Console.ReadLine();
            var dict = new Dictionary<int, List<string>>();
            for (int i = 1; i <= Convert.ToInt32(inputNumber); i++)
            {
                var line = Console.ReadLine();
                var data = line.Split(' ').ToList();
                int N = Convert.ToInt32(data[0]);
                int hash = Convert.ToInt32(data[1]);

                List<string> candidate = new List<string>();
                int maxDigit = Convert.ToInt32(Math.Pow(10, N));
                for (int j = 0; j < maxDigit; j++)
                {
                    int value = HashFunction(j);
                    if (hash == value)
                    {
                        StringBuilder val = new StringBuilder();
                        int lengthCandidate = j.ToString().Length;
                        if (lengthCandidate < N)
                        {
                            for (int k = 0; k < N - lengthCandidate; k++)
                            {
                                val.Append("0");
                            }
                            val = val.Append(j);
                        }
                        else
                        {
                            val = val.Append(j);
                        }
                        candidate.Add(val.ToString());
                    }
                }
                dict.Add(i, candidate.Count > 0 ? candidate : new List<string> { "Invalid" });
            }

            int count = 1;
            foreach (var kvp in dict)
            {
                StringBuilder sbOut = new StringBuilder();
                sbOut.Append("Case #" + count++ + ": ");
                sbOut.Append(string.Join(" ", kvp.Value));
                Console.WriteLine(sbOut.ToString());
            }
        }

        public static int HashFunction(int k)
        {
            return (k * (k + 3)) % 1000003;
        }
        public static void RG2()
        {
            string inputNumber = Console.ReadLine();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 1; i <= Convert.ToInt32(inputNumber); i++)
            {
                var inputLine1 = Console.ReadLine();
                var firstLine = inputLine1.Split(' ').ToList();
                int L = Convert.ToInt32(firstLine[0]);
                int M = Convert.ToInt32(firstLine[1]);
                int N = Convert.ToInt32(firstLine[2]);

                var inputLine2 = Console.ReadLine();
                var secondLine = inputLine2.Split(' ').ToList();
                secondLine = secondLine.Take(N).ToList();

                int totalL = 0;
                int totalM = 0;
                List<int> numberTemp = new List<int>();
                for (int j = 0; j < N; j++)
                {
                    int number = Convert.ToInt32(secondLine[j]);
                    numberTemp.Add(number);
                    if (j != N - 1)
                    {
                        totalL += number;
                        totalM = totalL > M ? totalM + 1 : totalM;
                    }
                }
                int check = numberTemp.TakeLast(L).Sum();
                totalM = check > M ? totalM + 1 : totalM;
                dictionary.Add(i, totalM);
            }

            foreach (var kvp in dictionary)
            {
                Console.WriteLine("Case #" + kvp.Key + ": " + kvp.Value);
            }
        }
        public static void RG1()
        {
            string inputNumber = Console.ReadLine();
            Dictionary<int, List<string>> dictionaryTemp = new Dictionary<int, List<string>>();
            for (int i = 1; i <= Convert.ToInt32(inputNumber); i++)
            {
                var inputLine = Console.ReadLine();
                var urlCollection = inputLine.Split(' ').ToList();
                List<string> listTemp = new List<string>();
                for (int j = 0; j < urlCollection.Count; j++)
                {
                    var urlDomain = urlCollection[j].Split('/')[0];
                    listTemp.Add(urlDomain);
                }
                dictionaryTemp.Add(i, listTemp);
            }

            Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
            foreach (var a in dictionaryTemp)
            {
                dictionary.Add(a.Key, a.Value.Distinct().ToList());
            }

            foreach (var kvp in dictionary)
            {
                Console.WriteLine("Case #" + kvp.Key + ": " + kvp.Value.Count);
            }
        }
        public static void RG0()
        {
            //var input = Console.ReadLine();
            string input = "{\"general\": [2, 3, 4], \"infra\": [3, 5], \"humor\": [4, 6]}";
            var jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(input);
            List<int> distinctNumber = jsonData.GroupBy(x => x.Value.Count).Select(x => x.Key).ToList();
            List<int> orderedDistinctNumber = (from item in distinctNumber orderby item select item).ToList();

            var list = new ArrayList();
            foreach (var alpha in orderedDistinctNumber)
            {
                list.Add(jsonData.Where(x => x.Value.Count == alpha).Select(y => y.Key).ToList());
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < orderedDistinctNumber.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("[");
                }

                sb.Append("[");
                for (int j = 0; j < ((List<string>)list[i]).Count; j++)
                {

                    sb.Append("\"" + ((List<string>)list[i])[j] + "\"");
                    if (j != ((List<string>)list[i]).Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");
                if (i == orderedDistinctNumber.Count - 1)
                {
                    sb.Append("]");
                }
                else
                {
                    sb.Append(", ");
                }
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
