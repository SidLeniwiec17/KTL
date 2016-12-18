using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class HumanScenario
    {
        SequencesMemory current_sequences { get; set; }
        List<Plate> game_state { get; set; }
        int alfa { get; set; }
        int beta { get; set; }
        public List<Scenario> children { get; set; }
        int current_depth { get; set; }
        int max_depth { get; set; }
        int all_colors { get; set; }
        int random_colors { get; set; }
        int free_plates { get; set; }
        public int choosen_color { get; set; }
        List<int> random_choosen_colors { get; set; }

        public HumanScenario()
        {
            this.game_state = new List<Plate>();
            this.alfa = 0;
            this.beta = 0;
            this.children = new List<Scenario>();
            this.current_depth = -1;
            this.max_depth = -1;
            this.current_sequences = new SequencesMemory(1);
            this.all_colors = -1;
            this.random_colors = -1;
            this.free_plates = -1;
            this.choosen_color = -1;
            this.random_choosen_colors = new List<int>();

        }

        public HumanScenario(HumanScenario oldScenario)
        {
            this.game_state = new List<Plate>();
            for (int i = 0; i < oldScenario.game_state.Count; i++)
                this.game_state.Add(new Plate(oldScenario.game_state[i]));
            this.alfa = oldScenario.alfa;
            this.beta = oldScenario.beta;
            this.children = new List<Scenario>();
            for (int i = 0; i < oldScenario
                .children.Count; i++)
                this.children.Add(new Scenario(oldScenario.children[i]));
            this.current_depth = oldScenario.current_depth;
            this.max_depth = oldScenario.max_depth;
            this.current_sequences = new SequencesMemory(oldScenario.current_sequences);
            this.all_colors = oldScenario.all_colors;
            this.random_colors = oldScenario.random_colors;
            this.free_plates = oldScenario.free_plates;
            this.choosen_color = oldScenario.choosen_color;
            this.random_choosen_colors = new List<int>();
            for (int i = 0; i < oldScenario.random_choosen_colors.Count; i++)
                this.random_choosen_colors.Add(oldScenario.random_choosen_colors[i]);

        }

        public HumanScenario(int _alfa, int _beta, List<Plate> _game_state, int _current_depth, int _max_depth, SequencesMemory _memory, int _all_colors, int _random_colors, int _free_plates)
        {
            this.game_state = _game_state;
            this.alfa = _alfa;
            this.beta = _beta;
            this.children = new List<Scenario>();
            this.current_depth = _current_depth;
            this.max_depth = _max_depth;
            this.current_sequences = new SequencesMemory(1);
            this.current_sequences = _memory;
            this.all_colors = _all_colors;
            this.random_colors = _random_colors;
            this.free_plates = _free_plates;
        }
        public void MakeMove(List<List<int>> all_possible_colors, Scenario oldScenario)
        {
            int counter = 0;
            for (int j = counter; j < game_state.Count; j++)
            {
                if (this.game_state[j].is_checked == false)
                {
                    int new_selected_number = j;
                    counter = j;

                    for (int c = 0; c < all_possible_colors.Count; c++)
                    {
                        List<Plate> temp_game_state = new List<Plate>();
                        for (int g = 0; g < this.game_state.Count(); g++)
                            temp_game_state.Add(new Plate(this.game_state.ElementAt(g).color, this.game_state.ElementAt(g).is_checked));

                        SequencesMemory temp_memory = new SequencesMemory(this.current_sequences);
                        Scenario tmpScenario = new Scenario(this.alfa, this.beta, temp_game_state, this.current_depth, this.max_depth, temp_memory, this.all_colors, this.random_colors, this.free_plates);
                        tmpScenario.choosen_number = new_selected_number;
                        tmpScenario.random_choosen_colors = all_possible_colors[c];
                        this.children.Add(tmpScenario);
                    }
                }
            }
            for(int i = 0 ; i < this.children.Count ; i++)
            {
                this.children[i].MakeMove(this.children[i].choosen_number, this.children[i].random_choosen_colors, all_possible_colors);
            }
        }
    }
}