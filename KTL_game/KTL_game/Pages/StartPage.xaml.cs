using KTL_game.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KTL_game.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        MainWindow window;
        public StartPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SetupGame setupGamePage = new SetupGame(window, this);
            this.window.Content = setupGamePage;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            LogicHelper test = new LogicHelper();
            test.all_colors = 3;
            test.deep_search = 2;
            test.first_time = true;
            test.game_length = 4;
            test.free_plates = 4;
            for (int i = 0; i < 4; i++)
                test.game_state.Add(new Plate());
            test.random_colors = 2;
            test.seq_length = 2;
            test.prepareMemory();
            test.all_posssible_colors = SequenceHelper.GenerateColors(test.all_colors, test.random_colors);
            List<int> randomColors = new List<int>();
            randomColors.Add(0);
            randomColors.Add(2);
            int qqqq = test.chooseColor(0, randomColors);        
        }
    }
}
