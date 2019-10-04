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
        private char currentSymbol = 'X';
        private bool matrixIsFill = false;
        private char[][] matrix;

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
                matrix[row][column] = ChangeSymbol();
                matrixGrid.DataContext = matrix;
            }
        }
    }
}
