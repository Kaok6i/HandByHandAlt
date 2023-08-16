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
    /// Логика взаимодействия для ActionChoiceWindow.xaml
    /// </summary>
    public partial class ActionChoiceWindow : Window
    {
        public ActionChoiceWindow()
        {
            InitializeComponent();
        }

        private void actionChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            switch (actionsComboBox.SelectedIndex)
            {
                case 0:
                    var EditDirection = new EditDirectionWindow();
                    EditDirection.Show();
                    this.Close();
                    break;
                case 1:
                    var EditMentor = new EditMentorWindow();
                    EditMentor.Show();
                    this.Close();
                    break;
                case 2:
                    var EditApprentice = new EditApprenticeWindow();
                    EditApprentice.Show();
                    this.Close();
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}
