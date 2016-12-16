using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class LogicHelper
    {
        int game_length { get; set; }
        int all_colors { get; set; }
        int random_colors { get; set; }
        int seq_length { get; set; }
        int deep_search { get; set; }
        List<Plate> game_state { get; set; }
        SequencesMemory sequences { get; set; }
        int free_plates { get; set; }

        public LogicHelper()
        {
            this.game_length = -1;
            this.all_colors = -1;
            this.random_colors = -1;
            this.seq_length = -1;
            this.deep_search = -1;
            this.game_state = new List<Plate>();
            this.sequences = new SequencesMemory();
            this.free_plates = -1;
        }

        public LogicHelper(int _game_length, int _all_colors, int _random_colors, int _seq_length, int _deep_search, int _free_plates)
        {
            this.game_length = -1;
            this.all_colors = -1;
            this.random_colors = -1;
            this.seq_length = -1;
            this.deep_search = -1;
            this.game_state = new List<Plate>();
            this.free_plates = _free_plates;
        }
        public int chooseColor (int selected_number, List<int> random_colors)
        {
            int answ_color = -1;
            Scenario scenariusz = new Scenario(0, 0, this.game_state, 1, this.deep_search, this.sequences, this.all_colors, this.random_colors, this.free_plates);
            scenariusz.MakeMove(selected_number, random_colors);
            return answ_color;
        }
    }
}
