using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Helpers.Randoms
{
    public static class RNGGenerator
    {
        public static char GenerateChar(Random rng)
        {
            // 'Z' + 1 because the range is exclusive
            return (char)(rng.Next('A', 'Z' + 1));
        }
        public static char GenerateChar() => GenerateChar(RNG.Random);

        public static string GenerateString(Random rng, int length)
        {
            char[] letters = new char[length];
            for (int i = 0; i < length; i++)
            {
                letters[i] = GenerateChar(rng);
            }
            return new string(letters);
        }


        public static string GenerateString(int length) => GenerateString(RNG.Random, length);

    }
}
