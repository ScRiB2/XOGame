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
        private char currentSymbol = 'O';
        private char[][] matrix;
        private bool matrixFull = false;

        public MainWindow()
        {
            InitializeComponent();

            initFields();

            // changeVisible(Visibility.Hidden);
        }

        private void changeVisible(Visibility v)
        {
            foreach (UIElement c in mainGrid.Children)
            {
                c.Visibility = v;
            }
        }

        private char ChangeSymbol()
        {
            return currentSymbol == 'X' ? currentSymbol = 'O' : currentSymbol = 'X';
        }

        private void initFields(char symbol = ' ')
        {
            matrix = new char[3][];

            for (int i = 0; i < 3; i++)
            {
                matrix[i] = new char[3];
                for (int j = 0; j < 3; j++)
                {
                    matrix[i][j] = symbol;

                }
            }

            matrixGrid.DataContext = matrix;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            initFields();
        }

        private void PutSymbol(object sender, MouseButtonEventArgs e)
        {
            var myTextBox = sender as TextBlock;
            if (myTextBox != null)
            {
                int row = Grid.GetRow(myTextBox);
                int column = Grid.GetColumn(myTextBox);
                if (matrix[row][column] != ' ')
                {
                    return;
                }
                matrix[row][column] = ChangeSymbol();
                matrixGrid.DataContext = null;
                matrixGrid.DataContext = matrix;
                if (checkWin()) {
                    if (matrixFull)
                        MessageBox.Show("Ничья!");
                    else
                        MessageBox.Show("Победили " + matrix[row][column] + "!");
                    initFields();
                }
            }
        }

        private bool matrixIsFull()
        {
            bool flag = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (matrix[i][j] == ' ')
                        flag = false;
                }
            }
            return flag;
        }

        private bool checkWin()
        {
            if (matrix[0][0] == matrix[0][1] && matrix[0][1] == matrix[0][2])
            {
                if (matrix[0][0] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[1][0] == matrix[1][1] && matrix[1][1] == matrix[1][2])
            {
                if (matrix[1][0] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[2][0] == matrix[2][1] && matrix[2][1] == matrix[2][2])
            {
                if (matrix[2][0] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[0][0] == matrix[1][0] && matrix[1][0] == matrix[2][0])
            {
                if (matrix[0][0] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[0][1] == matrix[1][1] && matrix[1][1] == matrix[2][1])
            {
                if (matrix[0][1] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[0][2] == matrix[1][2] && matrix[1][2] == matrix[2][2])
            {
                if (matrix[0][2] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[0][0] == matrix[1][1] && matrix[1][1] == matrix[2][2])
            {
                if (matrix[0][0] != ' ')
                {
                    return true;
                }
            }
            else if (matrix[2][0] == matrix[1][1] && matrix[1][1] == matrix[0][2])
            {
                if (matrix[2][0] != ' ')
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
    }
}
