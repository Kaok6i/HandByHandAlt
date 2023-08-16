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

namespace HandByHand
{
    /// <summary>
    /// Логика взаимодействия для AddDirectionWindow.xaml
    /// </summary>
    public partial class AddDirectionWindow : Window
    {
        public AddDirectionWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editDrctnWndw = new EditDirectionWindow();
            editDrctnWndw.Show();
            this.Close();
        }

        private void AddMentorBttn_OnClick(object sender, RoutedEventArgs e)
        {
            string nameDirection = nameTB.Text;
            var addDrctnWndw = new AddDirectionMentorWindow(nameDirection);
            addDrctnWndw.Show();
            this.Close();
        }
    }
}
