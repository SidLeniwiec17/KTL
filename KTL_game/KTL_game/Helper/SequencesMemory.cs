using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class SequencesMemory
    {
        public List<List<Sequence>> sequences { get; set; }

        public SequencesMemory(int all_colors)
        {
            this.sequences = new List<List<Sequence>>();
            for (int i = 0; i < all_colors; i++)
                this.sequences.Add(new List<Sequence>());
        }
        public SequencesMemory(SequencesMemory oldMemory)
        {
            this.sequences = new List<List<Sequence>>();
            for (int i = 0; i < oldMemory.sequences.Count; i++)
            {
                List<Sequence> tmpList = new List<Sequence>();
                for (int j = 0; j < oldMemory.sequences[i].Count; j++)
                    tmpList.Add(new Sequence(oldMemory.sequences[i][j]));
                this.sequences.Add(tmpList);
            }
        }


        public int Update(int selected_number, int color)
        {
            bool add = true;
            for (int i = 0; i < this.sequences[color].Count; i++)
            {
                if (sequences[color][i].step == -1)
                {
                    int tmp_step = Math.Abs( selected_number - sequences[color][i].first_term);
                    sequences[color][i].step = tmp_step;
                    if (sequences[color][i].is_still_seq(selected_number)== 1)
                    {
                        sequences[color][i].curr_lengt++;
                        add = false;
                    }
                    else if (sequences[color][i].is_still_seq(selected_number) == 2)
                    {
                        sequences[color][i].curr_lengt++;
                        sequences[color][i].first_term = selected_number;
                        add = false;
                    }
                }
                else if (sequences[color][i].step >= 1)
                {
                    if (sequences[color][i].is_still_seq(selected_number)== 1)
                    {
                        sequences[color][i].curr_lengt++;
                        add = false;
                    }
                    else if (sequences[color][i].is_still_seq(selected_number) == 2)
                    {
                        sequences[color][i].curr_lengt++;
                        sequences[color][i].first_term = selected_number;
                        add = false;
                    }
                }
            }
            CombineSequences(color);
            if (add == true)
            {
                Sequence new_seq = new Sequence();
                new_seq.curr_lengt = 1;
                new_seq.step = -1;
                new_seq.first_term = selected_number;
                sequences[color].Add(new_seq);
                return 1;
            }
            else
                return -1;
        }

        public void CombineSequences(int color)
        {
            for (int i = 0; i < this.sequences[color].Count; i++)
            {
                for (int j = 1; j < this.sequences[color].Count; j++)
                {
                    Sequence first = this.sequences[color][i];
                    Sequence second = this.sequences[color][j];
                    if (first.step == second.step && first.curr_lengt > 1 && second.curr_lengt > 1)
                    {
                        if (SequenceHelper.NTerm(first.first_term,first.step,first.curr_lengt+1) == second.first_term)
                        {
                            this.sequences[color][i].curr_lengt += this.sequences[color][j].curr_lengt;
                            this.sequences[color].RemoveAt(j);
                        }
                    }
                }
            }
        }
    }
}
