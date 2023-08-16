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
    /// Логика взаимодействия для AddApprenticeWindow.xaml
    /// </summary>
    public partial class AddApprenticeWindow : Window
    {
        Entities entity = new Entities();
        List<int> DirectionsList = new List<int>();
        public AddApprenticeWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editApprntcWndw = new EditApprenticeWindow();
            editApprntcWndw.Show();
            this.Close();
        }

        private void CompleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Student student = new Student
                {
                    Name = nameTB.Text,
                    Surname = surnameTB.Text,
                    Patronymic = patronymicTB.Text,
                    Date_Of_Birthday = DateTime.Parse(birthdayDP.DisplayDate.ToShortDateString()),
                    Start_Year = yearTB.Text,
                    ID_Direction = DirectionsList[directionCB.SelectedIndex],
                };
                entity.Student.Add(student);
                entity.SaveChanges();
                MessageBox.Show("Данные были успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                backButton_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Не удалось добавить запись в базу данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Direction direction in entity.Direction)
            {
                directionCB.Items.Add(direction.Name_Direction);
                DirectionsList.Add(direction.ID);
            }
        }
    }
}
