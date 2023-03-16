using System.Net.Http;
using System.Text.Json;


using System;
using System.Net;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Result.getPhoneNumbers("Afghanistan", "656445445"));
            Console.WriteLine(Result.getPhoneNumbers("Afghan1ist1an", "111111111111"));
            Console.WriteLine(Result.getPhoneNumbers("Dominican Republic", "222222222222"));
        }

        class Result
        {
            /*
             * Complete the 'getPhoneNumbers' function below.
             *
             * The function is expected to return a STRING.
             * The function accepts following parameters:
             *  1. STRING country
             *  2. STRING phoneNumber
             * API URL: https://jsonmock.hackerrank.com/api/countries?name=<country>
             */
            public static string getPhoneNumbers(string country, string phoneNumber)
            {
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("https://jsonmock.hackerrank.com/api/countries?name=" + country);

                    using JsonDocument doc = JsonDocument.Parse(json);
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");
                    if (data.GetArrayLength() == 0)
                    {
                        return "-1";
                    }
                    JsonElement callingCodes = data[0].GetProperty("callingCodes");
                    var codes = JsonSerializer.Deserialize<string[]>(callingCodes);
                    if (codes.Count() > 1)
                    {
                        var maxCode = 0;
                        for (int i = 0; i < codes.Length; i++)
                        {
                            int item = Convert.ToInt32(codes[i]);
                            if (item > maxCode)
                            {
                                maxCode = item;
                            }
                        }
                        return maxCode + " " + phoneNumber;
                    }
                    else
                    {
                        return codes[0] + " " + phoneNumber;
                    }
                }
            }
        }
    }
}