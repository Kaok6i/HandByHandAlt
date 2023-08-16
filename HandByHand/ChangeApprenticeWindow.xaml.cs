using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для ChangeApprenticeWindow.xaml
    /// </summary>
    public partial class ChangeApprenticeWindow : Window
    {
        Entities entity = new Entities();
        Student newStudent = new Student();
        List<int> DirectionsList = new List<int>();
        public ChangeApprenticeWindow(Student oldStudent)
        {
            InitializeComponent();
            newStudent = oldStudent;
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
                    ID = newStudent.ID,
                    Name = nameTB.Text,
                    Surname = surnameTB.Text,
                    Patronymic = patronymicTB.Text,
                    Date_Of_Birthday = birthdayDP.DisplayDate.Date,
                    Start_Year = yearTB.Text,
                    ID_Direction = DirectionsList[directionCB.SelectedIndex],
                };
                entity.Student.AddOrUpdate(student);
                entity.SaveChanges();
                MessageBox.Show("Данные были успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                backButton_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeApprenticeWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            nameTB.Text = newStudent.Name;
            surnameTB.Text = newStudent.Surname;
            patronymicTB.Text = newStudent.Patronymic;
            birthdayDP.Text = newStudent.Date_Of_Birthday.ToString();
            yearTB.Text = newStudent.Start_Year.ToString();
            foreach (Direction direction in entity.Direction)
            {
                directionCB.Items.Add(direction.Name_Direction);
                DirectionsList.Add(direction.ID);
            }
            directionCB.SelectedItem = newStudent.NameDirection;
        }
    }
}
