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

namespace XOGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[][] matrix;
        private bool matrixFull = false;
        public Player player1 = new Player();
        public Player player2 = new Player();
        private Player currentPlayer;

        public class Player
        {
            public string name;
            public int score = 0;
            public string symbol;
            public bool isBot = false;
        }

        public MainWindow()
        {
            InitializeComponent();

            changeVisible(Visibility.Hidden);
            firstPlayerBtn.Visibility = Visibility.Visible;
            secondPlayerBtn.Visibility = Visibility.Visible;
        }

        private void OnePlayerGame(object sender, RoutedEventArgs e)
        {
            Window1 windowOfPlayer = new Window1();
            windowOfPlayer.ShowDialog();
            player1.name = windowOfPlayer.playerName1_TextBox.Text;
            ComboBoxItem selectedItem = (ComboBoxItem)windowOfPlayer.symbolBox.SelectedItem;
            TextBlock symbolBlock = selectedItem.Content as TextBlock;
            player1.symbol = symbolBlock.Text;
            player2.name = "Бот";
            player2.symbol = player1.symbol == "O" ? "X" : "O";
            player2.isBot = true;

            changeScore();
            currentPlayer = player2;
            changeStatistic();
            initFields();

            windowOfPlayer.Close();
            changeVisible(Visibility.Visible);
            firstPlayerBtn.Visibility = Visibility.Hidden;
            secondPlayerBtn.Visibility = Visibility.Hidden;
        }

        private void TwoPlayerGame(object sender, RoutedEventArgs e)
        {
            TwoPlayers windowOfPlayer = new TwoPlayers();
            windowOfPlayer.ShowDialog();
            player1.name = windowOfPlayer.playerName1_TextBox.Text;
            player2.name = windowOfPlayer.playerName2_TextBox.Text;
            player1.symbol = "X";
            player2.symbol = "O";

            changeScore();
            currentPlayer = player2;
            changeStatistic();
            initFields();

            windowOfPlayer.Close();
            changeVisible(Visibility.Visible);
            firstPlayerBtn.Visibility = Visibility.Hidden;
            secondPlayerBtn.Visibility = Visibility.Hidden;
        }

        private void changeStatistic()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            CurrentMove_Label.Content = "Сейчас ходит " + currentPlayer.name + " - " + currentPlayer.symbol;
            if (currentPlayer.isBot)
            {
                StepOfBot();
            }
        }

        private void changeScore()
        {
            Player1_Score.Content = player1.name + ": " + player1.score;
            Player2_Score.Content = player2.name + ": " + player2.score;
        }

        private void changeVisible(Visibility v)
        {
            foreach (UIElement c in mainGrid.Children)
            {
                c.Visibility = v;
            }
        }

        private void initFields(string symbol = " ")
        {
            matrix = new string[3][];

            for (int i = 0; i < 3; i++)
            {
                matrix[i] = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    matrix[i][j] = symbol;

                }
            }
            matrixGrid.DataContext = matrix;
        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            changeVisible(Visibility.Hidden);
            firstPlayerBtn.Visibility = Visibility.Visible;
            secondPlayerBtn.Visibility = Visibility.Visible;
            player1.score = 0;
            player2.score = 0;
            changeScore();
        }

        private void Reset_State(object sender, RoutedEventArgs e)
        {
            player1.score = 0;
            player2.score = 0;
            changeScore();
        }

        private void insertSymbol(int row, int column)
        {
            
            matrix[row][column] = currentPlayer.symbol;
            matrixGrid.DataContext = null;
            matrixGrid.DataContext = matrix;
            if (checkWin())
            {
                if (matrixFull)
                    MessageBox.Show("Ничья!");
                else
                {
                    currentPlayer.score++;
                    changeScore();
                    MessageBox.Show("Победил " + currentPlayer.name + "!");
                }
                initFields();
                matrixFull = false;
            }
            changeStatistic();
        }

        private void PutSymbol(object sender, MouseButtonEventArgs e)
        {
            var myTextBox = sender as TextBlock;
            if (myTextBox != null)
            {
                int row = Grid.GetRow(myTextBox);
                int column = Grid.GetColumn(myTextBox);
                if (matrix[row][column] != " ")
                {
                    return;
                }
                insertSymbol(row, column);
            }
        }

        private bool matrixIsFull()
        {
            bool flag = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i][j] == " ")
                        flag = false;
                }
            }
            return flag;
        }

        private bool checkWin()
        {
            if (matrix[0][0] == matrix[0][1] && matrix[0][1] == matrix[0][2])
            {
                if (matrix[0][0] != " ")
                {
                    return true;
                }
            }
            else if (matrix[1][0] == matrix[1][1] && matrix[1][1] == matrix[1][2])
            {
                if (matrix[1][0] != " ")
                {
                    return true;
                }
            }
            else if (matrix[2][0] == matrix[2][1] && matrix[2][1] == matrix[2][2])
            {
                if (matrix[2][0] != " ")
                {
                    return true;
                }
            }
            else if (matrix[0][0] == matrix[1][0] && matrix[1][0] == matrix[2][0])
            {
                if (matrix[0][0] != " ")
                {
                    return true;
                }
            }
            else if (matrix[0][1] == matrix[1][1] && matrix[1][1] == matrix[2][1])
            {
                if (matrix[0][1] != " ")
                {
                    return true;
                }
            }
            else if (matrix[0][2] == matrix[1][2] && matrix[1][2] == matrix[2][2])
            {
                if (matrix[0][2] != " ")
                {
                    return true;
                }
            }
            else if (matrix[0][0] == matrix[1][1] && matrix[1][1] == matrix[2][2])
            {
                if (matrix[0][0] != " ")
                {
                    return true;
                }
            }
            else if (matrix[2][0] == matrix[1][1] && matrix[1][1] == matrix[0][2])
            {
                if (matrix[2][0] != " ")
                {
                    return true;
                }
            }
            else if (matrixIsFull())
            {
                matrixFull = true;
                return true;
            }
            return false;
        }

        private int[] mapMatrixToBoard()
        {
            List<int> s = new List<int>();
            int z = 0;
            for (int i = 0; i < 3; i++)
            {
                int j = 0;
                for (; j < 3; j++)
                {
                    if (matrix[i][j] == " ")
                        s.Add(i + z + j);
                }
                z = (j-1) * (i + 1);
            }
            return s.ToArray();
        }

        private void StepOfBot()
        {
            int[] board = mapMatrixToBoard();
            Random rand = new Random();
            int index = rand.Next(0, board.Length);
            int step = board[index];
            if (step < 3)
                insertSymbol(0, step);
            else if (step < 6)
                insertSymbol(1, step-3);
            else 
                insertSymbol(2, step - 6);
        }
    }
}
