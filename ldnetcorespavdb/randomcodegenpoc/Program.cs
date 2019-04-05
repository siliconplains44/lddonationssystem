using System;

namespace randomcodegenpoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            var randomString = "";

            for (int i = 0; i < 100; i++)
            {
                randomString += random.Next(0, 9).ToString();
            }

            Console.WriteLine(randomString);
        }
    }
}
