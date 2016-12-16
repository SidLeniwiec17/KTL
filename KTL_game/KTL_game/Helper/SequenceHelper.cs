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
        public static List<int> randColors(int total_colors, int rand_colors)
        {
            List<int> colors = new List<int>();
            Random rand = new Random();
            //Losuję listę kolorów
            for (int i = 0; i < rand_colors; i++)
            {
                while (true)
                {
                    bool foundRand = true;
                    int tmpcol = rand.Next(total_colors - 1);
                    for (int j = 0; j < colors.Count; j++)
                    {
                        if (colors[j] == tmpcol)
                        {
                            foundRand = false;
                        }
                    }
                    if (foundRand == true)
                    {
                        colors.Add(tmpcol);
                        break;
                    }
                }
            }
            return colors;
        }
        public static int all_possible_colors(int total_colors, int rand_colors)
        {
            int possibilities = 0;
            possibilities = Factorial(total_colors) / (Factorial(rand_colors) * Factorial(total_colors - rand_colors));
            return possibilities;
        }
        public static int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}
