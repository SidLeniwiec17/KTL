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
    /// Interaction logic for GameplayPage.xaml
    /// </summary>
    public partial class GameplayPage : Page
    {
        private int GameLength { get; set; }
        private int SeriesLength { get; set; }
        private int ColorsCount { get; set; }
        private int ColorListCount { get; set; }
        private MainWindow Window { get; set; }
        private StartPage StartPage { get; set; }
        private List<Button> ButtonList { get; set; }
        private int NumberOfFieldsInRow { get; set; }
        private int NumberOfRows { get; set; }
        private List<Color> ColorsList {get; set;}
        public GameplayPage(MainWindow window, StartPage startPage, int gameLenght, int seriesLength, int colorsCount, int colorListCount)
        {
            this.Window = window;
            this.StartPage = startPage;
            this.GameLength = gameLenght;
            this.SeriesLength = seriesLength;
            this.ColorsCount = colorsCount;
            this.ColorListCount = colorListCount;
            this.NumberOfFieldsInRow = 15;
            InitializeComponent();
            InitLabelsValues();
            InitGrid();
            InitFields();
            InitColorsList();
        }

        private void InitLabelsValues()
        {
            GameLengthLabel.Content = "Game length: "+ GameLength + "   ";
            SeriesLengthLabel.Content = "Series length " + SeriesLength + "   ";
            ColorCountLabel.Content = "Colors count " + ColorsCount + "  ";
            ColorCountListLabel.Content = "List colors count " + ColorListCount + "  ";
        }

        private void InitGrid()
        {
            NumberOfRows = 1 + GameLength / NumberOfFieldsInRow;
            GameGrid.Height = NumberOfRows * 100;
            for(int i=0;i<NumberOfFieldsInRow;i++)
            {
                var columnDefinition = new ColumnDefinition();
                GameGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for(int i=0;i<NumberOfRows;i++)
            {
                var rowDefinition = new RowDefinition();
                GameGrid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void InitFields()
        {
            int counter = 0;
            for (int j = 0; j < NumberOfRows; j++)
            {
                for (int i = 0; i < NumberOfFieldsInRow; i++)
                {

                    var button = new Button();
                    button.Content = counter + 1;
                    button.Click += new RoutedEventHandler(FieldButtonClick);
                    Grid.SetRow(button, j);
                    Grid.SetColumn(button, i);
                    GameGrid.Children.Add(button);
                    counter++;
                    if (counter == GameLength)
                    {
                        j++;
                        break;
                    }
                }
            }
        }

        private void InitColorsList()
        {
            Random rand = new Random();
            ColorsList = new List<Color>();
            for(int i=0;i<ColorsCount;i++)
            {
                byte red = (byte)rand.Next(255);
                byte green = (byte)rand.Next(255);
                byte blue = (byte)rand.Next(255);
                var color = Color.FromRgb(red, green, blue);
                ColorsList.Add(color);
            }
        }

        private void FieldButtonClick(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            //Losuję listę kolorów
            var tmpColorList = new List<Color>();
            for (int i = 0; i < ColorListCount; i++)
            {
                while (true)
                {
                    bool foundRand = true;
                    int tmpNum = rand.Next(ColorsCount - 1);
                    for(int j=0;j<tmpColorList.Count;j++)
                    {
                        if(ColorsList[tmpNum] == tmpColorList[j])
                        {
                            foundRand = false;
                        }
                    }
                    if(foundRand == true)
                    {
                        tmpColorList.Add(ColorsList[tmpNum]);
                        break;
                    }
                }
            }
            //Losuję --- Wybieram kolor z listy kolorów
            int colorIndex = rand.Next(tmpColorList.Count - 1);
            var button = (Button)sender;
            button.Background = new SolidColorBrush(tmpColorList[colorIndex]);
            button.IsEnabled = false;
            //sprawdzam czy powstał ciąg
        }
    }
}
