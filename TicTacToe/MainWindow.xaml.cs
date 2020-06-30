using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary> 
        /// Holds the current results of cells in the active game 
        /// </summary> 
        private MarkType[] mResults;

        /// <summary> 
        /// True if it is player l's turn (X) or player 2's turn (0) 
        /// </summary> 
        private bool mPlayer1Turn;

        /// <summary> 
        /// True if the game has ended 
        /// </summary> 
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary> /// Default constructor /// </summary>  0 references 
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            //Create new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //To be sure thet P1 starts the game
            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //Background and Foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.Pink;
                button.Foreground = Brushes.Blue;
            });
            //Too be sure that the game hasn't finish
            mGameEnded = false;
        }
        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button thet was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game on the click after it finish
            if (mGameEnded)
            {

                NewGame();
                //mPlayer1Turn = false;
                return;
            }

            //Cast the sender to a button
            var button = (Button)sender;

            //Find the button possition in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anyting if the cell has already value in it
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on witch player turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            //Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change nought color
            if (!mPlayer1Turn)
            button.Foreground = Brushes.Red;

            //Toggle the players turns
           mPlayer1Turn ^= true; //doing same like commands below

            //if (mPlayer1Turn)
            //    mPlayer1Turn = false;
            //else
            //    mPlayer1Turn = true;

            //Check for Winner
            CheckForWinner();
        }

        /// <summary>
        /// Check if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal wins
            //Check for horizontal wins
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical wins
            //Check for vertical wins
            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal wins

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game over
                mGameEnded = true;

                //Highlighting winning cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No winner
            //Check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                // Game ended 
                mGameEnded = true;
                //MessageBox.Show("НЯМА ПОБЕДИТЕЛ!");
                // Turn all cells orange 
                Container.Children.Cast<Button>().ToList().ForEach(button => 
                {
                   button.Background = Brushes.Orange;
                });
            }
            #endregion

        }

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}