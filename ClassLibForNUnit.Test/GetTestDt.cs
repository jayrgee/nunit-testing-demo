using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ClassLibForNUnit.Test
{
    public class GetTestDt
    {
        //static string CSVFileName = @"<path for="" file=""> \data.csv";
        static string currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static string CSVFileName = currentDir + @"\data.csv";
        //public int input { get; set; }
        //public int output { get; set; }
        public static IEnumerable GetTestData
        {
            get
            {
                using (var inputStream = new FileStream(CSVFileName, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(inputStream))
                    {
                        string inputLine;
                        while ((inputLine = streamReader.ReadLine()) != null)
                        {
                            var data = inputLine.Split(',');
                            List<int> param = new List<int>();
                            for (int i = 0; i < data.Length; i++)
                            {
                                param.Add(Convert.ToInt32(data[i]));
                            }

                            yield return new[] { param.ToArray() };
                        }
                    }
                }
            }
        }
    }
}
