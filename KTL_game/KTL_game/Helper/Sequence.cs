﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class Sequence
    {
        public int first_term { get; set; }
        public int step { get; set; }
        public int curr_lengt { get; set; }

        public Sequence()
        {
            this.first_term = -1;
            this.step = -1;
            this.curr_lengt = -1;
        }
        public Sequence(Sequence oldSequence)
        {
            this.first_term = oldSequence.first_term;
            this.step = oldSequence.step;
            this.curr_lengt = oldSequence.curr_lengt;
        }
        public int is_still_seq(int index)
        {
            if (SequenceHelper.NTerm(this.first_term, this.step, this.curr_lengt + 1) == index)
                return 1;
            else if (SequenceHelper.NTerm(index , this.step, 1) == this.first_term)
                return 2;
            else
                return 0;
        }
    }
}
