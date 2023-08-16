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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace HandByHand
{
    /// <summary>
    /// Логика взаимодействия для AddMentorWindow.xaml
    /// </summary>
    public partial class AddMentorWindow : Window
    {
        Entities entity = new Entities();
        public AddMentorWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editMntrWndw = new EditMentorWindow();
            editMntrWndw.Show();
            this.Close();
        }

        private void CompleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Mentor mentor = new Mentor
                {
                    Name = nameTB.Text,
                    Surname = surnameTB.Text,
                    Patronymic = patronymicTB.Text,
                    Type_Of_Education = educationTB.Text,
                    Date_Of_Birthday = DateTime.Parse(birthdayDP.DisplayDate.ToShortDateString())
                };
                entity.Mentor.Add(mentor);
                entity.SaveChanges();
                User user = new User
                {
                    ID_Mentor = entity.Mentor.Max(m => m.ID),
                    Is_Administartor = false,
                    Login = loginTB.Text,
                    Password = passwordTB.Text
                };
                entity.User.Add(user);
                entity.SaveChanges();
                MessageBox.Show("Данные были успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                backButton_Click(sender, e);
            }
            catch
            {
                MessageBox.Show("Не удалось добавить запись в базу данных", "Уведомление",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
