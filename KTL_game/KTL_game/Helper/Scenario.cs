using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class Scenario
    {
        SequencesMemory current_sequences { get; set; }
        List<Plate> game_state { get; set; }
        int alfa { get; set; }
        int beta { get; set; }
        List<Scenario> children { get; set; }
        int current_depth { get; set; }
        int max_depth { get; set; }
        int all_colors { get; set; }
        int random_colors { get; set; }
        int free_plates { get; set; }

        public Scenario(int _alfa, int _beta, List<Plate> _game_state, int _current_depth, int _max_depth, SequencesMemory _memory, int _all_colors, int _random_colors, int _free_plates)
        {
            this.game_state = _game_state;
            this.alfa = _alfa;
            this.beta = _beta;
            this.children = new List<Scenario>();
            this.current_depth = _current_depth;
            this.max_depth = _max_depth;
            this.current_sequences = _memory;
            this.all_colors = _all_colors;
            this.random_colors = _random_colors;
            this.free_plates = _free_plates;
        }
        public void MakeMove(int selected_number, List<int> random_colors)
        {
            if (current_depth <= max_depth)
            {
                for (int i = 0; i < random_colors.Count; i++)
                {
                    List<Plate> temp_game_state = this.game_state;
                    temp_game_state[selected_number].is_checked = true;
                    temp_game_state[selected_number].color = random_colors[i];
                    SequencesMemory temp_memory = this.current_sequences;
                    int answ = temp_memory.Update(selected_number, random_colors[i]);
                    //calc alfa, beta
                    int tmp_alfa = this.alfa, tmp_beta = this.beta;
                    if (answ < 0)
                        tmp_alfa++;
                    else
                        tmp_beta--;

                    if (tmp_alfa <= tmp_beta)
                    {
                        Scenario temp_scenario = new Scenario(tmp_alfa, tmp_beta, temp_game_state, this.current_depth++, this.max_depth, temp_memory,this.all_colors, this.random_colors, this.free_plates - 1);
                        this.children.Add(temp_scenario);

                        int counter = 0;
                        for(int p = 0 ; p < this.free_plates ; p++)
                        {
                            for (int j = counter; j < game_state.Count; j++)
                            if(this.game_state[j].is_checked == false)
                            {
                                int new_selected_number = j;
                                counter = j;
                                /*
                                 *  dla tego new_selected_numer znajduje WSZYSTKIE mozliwe kombinacje losowych kolorow
                                 *  List<int> tmp_rnd_col = SequenceHelper.randColors(this.all_colors, this.random_colors);
                                 *  i robie 
                                 *  this.children[this.children.Count - 1].MakeMove(new_selected_number, tmp_rnd_col);
                                 */
                            }
                        }
                    }
                }
                //tu jakas czarymary rekursja
            }
            else
            {
                /* zwrot  wyniku
                 * jade po drzewie. i wybieram takie gdzie jest najwieksze beta - alfa 
                 */ 
            }
        }
    }
}
