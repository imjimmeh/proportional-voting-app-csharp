using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Helpers.Randoms
{
    public static class RNG
    {
        private static Random? _random;

        public static Random Random { get { return _random ??= (_random = new Random()); } }

        public static Random CreateNewRandom(int? seed = null)
        {
            if (seed.HasValue)
                _random = new Random(seed.Value);
            else
                _random = new Random();

            return _random;
        }
    }
}
