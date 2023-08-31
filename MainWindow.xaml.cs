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


namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthofSecondsElapsed;
        int matchesfound;
       
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval =  TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // updates the new TextBlock with the elasped time 
            // and stops  the timer once the play has found
            // all of the matches
            tenthofSecondsElapsed++;
            timeTextBlock.Text = (tenthofSecondsElapsed / 10f).ToString("0.0s");
            if(matchesfound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + "- Play again?";
            }
        }

        private void SetUpGame()


        {
            // create a list of eight pairs of emojis
            List<string> animalEmoji = new List<string>()
            {"🐙","🐙",
             "🦘","🦘",
             "🐪","🐪",
             "🐳","🐳",
             "🐘","🐘",
             "🦕","🦕",
             "🦔","🦔",
             "🦉","🦉",

            };

            // create a random no. generator
            Random random = new Random();

            //Find every TextBlock in the maingrid and reapeat
            // the following statements for each of them
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {

                    textBlock.Visibility = Visibility.Visible;
                    //Pick a random number b/n 0 and the no. of emojis
                    // left in the list and call it 'index'
                    int index = random.Next(animalEmoji.Count);

                    //Use the random no. called 'index' to get a random
                    //emoji from the list
                    string nextEmoji = animalEmoji[index];

                    //Update the TextBlock with the random emoji
                    // from the lists
                    textBlock.Text = nextEmoji;

                    //Remove the random emoji from the list
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthofSecondsElapsed = 0;
            matchesfound = 0;
        }

        TextBlock lastTextBlockClicked;
        // keep track of whether or not the player just clicked on the first animal
        // in a pair and is now trying to find a match
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*if its the first in the pair being clicked
             Keep track of which TextBlock was clicked and
            make the animal disappear. If it's the second
            one, either make it disappear(if it's a match)
            or bring back the first one(if it's not)*/

            TextBlock textBlock = sender as TextBlock;

            //The player's first click & makes invisible & track
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            //player found a match & make second invisible 
            //& reset findingMatch
            else if(textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesfound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            //if it doesn't matach make first animal visible again
            //& reset findingMatch
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // this resets the game if all 8 matched pairs have been
            // found (otherwise it does nothing bcos the game is still
            // running).
            if(matchesfound == 8)
            {
                SetUpGame();
            }
        }
    }
}
