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
using System.Windows.Shapes;

namespace XOGame
{
    /// <summary>
    /// Логика взаимодействия для TwoPlayers.xaml
    /// </summary>
    public partial class TwoPlayers : Window
    {
        public TwoPlayers()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (playerName1_TextBox.Text.Length > 0 && playerName2_TextBox.Text.Length > 0)
                this.Close();
            else
                MessageBox.Show("Заполните пустые поля!");
        }
    }
}
