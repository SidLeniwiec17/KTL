using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class LogicHelper
    {
        public int game_length { get; set; }
        public int all_colors { get; set; }
        public int random_colors { get; set; }
        public int seq_length { get; set; }
        public int deep_search { get; set; }
        public List<Plate> game_state { get; set; }
        SequencesMemory sequences { get; set; }
        public int free_plates { get; set; }
        Scenario scenariusz { get; set; }
        public bool first_time { get; set; }
        public List<List<int>> all_posssible_colors { get; set; }
        int previous_color { get; set; }

        public LogicHelper()
        {
            this.game_length = -1;
            this.all_colors = -1;
            this.random_colors = -1;
            this.seq_length = -1;
            this.deep_search = -1;
            this.game_state = new List<Plate>();
            this.sequences = new SequencesMemory(1);
            this.free_plates = -1;
            this.scenariusz = new Scenario();
            this.first_time = true;
            this.all_posssible_colors = new List<List<int>>();
            this.previous_color = -1;
        }

        public LogicHelper(int _game_length, int _all_colors, int _random_colors, int _seq_length, int _deep_search, int _free_plates)
        {
            this.game_length = _game_length;
            this.all_colors = _all_colors;
            this.random_colors = _random_colors;
            this.seq_length = _seq_length;
            this.deep_search = _deep_search;
            this.game_state = new List<Plate>();
            for (int i = 0; i < _game_length; i++)
                this.game_state.Add(new Plate());
            this.free_plates = _free_plates;
            this.first_time = true;
            this.sequences = new SequencesMemory(_all_colors);
            this.all_posssible_colors = SequenceHelper.GenerateColors(this.all_colors, this.random_colors);
            this.previous_color = -1;
        }
        public void prepareMemory()
        {
            this.sequences = new SequencesMemory(this.all_colors);
        }

        public int chooseColor(int selected_number, List<int> random_colors)
        {
            int answ_color = -1;
            if (this.first_time == true)
            {
                this.scenariusz = new Scenario(0, 0, this.game_state, 0, this.deep_search, this.sequences, this.all_colors, this.random_colors, this.free_plates);
                this.scenariusz.MakeMove(selected_number, random_colors, this.all_posssible_colors);
                this.first_time = false;

                int bestVal = -1000;
                int bestCol = -1;
                for (int i = 0; i < this.scenariusz.children.Count; i++)
                {
                    int tmpVal = SequenceHelper.SearchHTree(this.scenariusz.children[i]);
                    if (tmpVal >= bestVal)
                    {
                        bestVal = tmpVal;
                        bestCol = i;
                    }
                }
                answ_color = bestCol;
                previous_color = bestCol;
                
            }
            else
            {
                Scenario future_scenario = new Scenario();
                for(int i = 0 ; i < this.scenariusz.children[previous_color].children.Count ; i++)
                {
                    if(this.scenariusz.children[previous_color].children[i].choosen_number == selected_number && SequenceHelper.ScrambledEquals(this.scenariusz.children[previous_color].children[i].random_choosen_colors,random_colors))
                    {
                        Console.WriteLine("test");
                        future_scenario = new Scenario(this.scenariusz.children[previous_color].children[i]);
                        this.scenariusz = new Scenario(future_scenario);
                        this.scenariusz.max_depth++;
                        this.scenariusz.MakeMove(selected_number, random_colors, this.all_posssible_colors);

                        int bestVal = -1000;
                        int bestCol = -1;
                        for (int j = 0; j < this.scenariusz.children.Count; j++)
                        {
                            int tmpVal = SequenceHelper.SearchHTree(this.scenariusz.children[j]);
                            if (tmpVal >= bestVal)
                            {
                                bestVal = tmpVal;
                                bestCol = j;
                            }
                        }
                        answ_color = bestCol;
                        previous_color = bestCol;
                        break;
                    }
                }
            }
            return answ_color;
        }
    }
}
