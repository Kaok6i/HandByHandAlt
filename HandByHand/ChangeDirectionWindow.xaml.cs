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
    /// Логика взаимодействия для ChangeDirectionWindow.xaml
    /// </summary>
    public partial class ChangeDirectionWindow : Window
    {
        Entities entity = new Entities();
        EditDirection editDirection = new EditDirection();
        public ChangeDirectionWindow(EditDirection oldDirection)
        {
            InitializeComponent();
            editDirection = oldDirection;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editDrctnWndw = new EditDirectionWindow();
            editDrctnWndw.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameTB.Text = editDirection.Name_Direction;
        }

        private void changeMentorBttn_Click(object sender, RoutedEventArgs e)
        {
            var changeDrctnMentorWndw = new ChangeDirectionMentorWindow(nameTB.Text);
            changeDrctnMentorWndw.Show();
            this.Close();
        }
    }
}
