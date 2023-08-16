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
    /// Логика взаимодействия для ChangeMentorWindow.xaml
    /// </summary>
    public partial class ChangeMentorWindow : Window
    {
        Entities entity = new Entities();
        Mentor newMentor = new Mentor();
        User newUser = new User();
        public ChangeMentorWindow(Mentor oldMentor)
        {
            InitializeComponent();
            newMentor = oldMentor;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editMntrWndw = new EditMentorWindow();
            editMntrWndw.Show();
            this.Close();
        }

        private void ChangeMentorWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            nameTB.Text = newMentor.Name;
            surnameTB.Text = newMentor.Surname;
            patronymicTB.Text = newMentor.Patronymic;
            educationTB.Text = newMentor.Type_Of_Education;
            birthdayDP.Text = newMentor.Date_Of_Birthday.ToString();
            foreach (User user in entity.User)
            {
                if (user.ID_Mentor == newMentor.ID)
                {
                    loginTB.Text = user.Login;
                    passwordTB.Text = user.Password;
                    newUser = user;
                }
            }
        }

        private void CompleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Mentor mentor = new Mentor
                {
                    ID = newMentor.ID,
                    Name = nameTB.Text,
                    Surname = surnameTB.Text,
                    Patronymic = patronymicTB.Text,
                    Type_Of_Education = educationTB.Text,
                    Date_Of_Birthday = DateTime.Parse(birthdayDP.DisplayDate.ToShortDateString())
                };
                entity.Mentor.AddOrUpdate(mentor);
                entity.SaveChanges();
                User user = new User
                {
                    ID = newUser.ID,
                    ID_Mentor = mentor.ID,
                    Is_Administartor = false,
                    Login = loginTB.Text,
                    Password = passwordTB.Text
                };
                entity.User.AddOrUpdate(user);
                entity.SaveChanges();
                MessageBox.Show("Данные были успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                backButton_Click(sender,e);
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
