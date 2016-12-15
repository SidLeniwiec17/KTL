using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class SequenceHelper
    {
        public static int NTerm(int firstTerm, int step, int n)
        {
            int nTerm = 0;
            nTerm = firstTerm + (n - 1) * step;

            return nTerm;
        }
    }
}
