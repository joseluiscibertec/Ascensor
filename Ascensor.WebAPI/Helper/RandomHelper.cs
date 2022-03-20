using System;

namespace Ascensor.WebAPI.Helper
{
    public class RandomHelper
    {
        public int RandomGenerate(int cant)
        {
            Random random = new Random();
            return random.Next(1, cant);
        }
    }
}