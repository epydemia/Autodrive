using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive
{
    public static class MathUtils
    {
        private static Random random;
        private static object syncObj = new object();

        public static double Bound(double value, double Min, double Max)
        {
            return (value < Min ? Min : (value > Max ? Max : value));
        }



        private static void InitRandomNumber(int seed)
        {
            random = new Random(seed);
        }

        public static int GenerateRandomNumber(int max)
        {
            lock (syncObj)
            {
                if (random == null)
                    random = new Random(); // Or exception...
                return random.Next(max);
            }
        }

        public static double RandomDouble(double min, double max)
        {
            if (random == null)
                random = new Random(); // Or exception...
    

            return (random.NextDouble() * (max - min))+min;

        }

    }
}
