using Notification.Wpf;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    { 
        Entities entity = new Entities();
        TestConnection connection = new TestConnection();

        public AutorizationWindow()
        {
            InitializeComponent();
            passwordCloseButton.Visibility = Visibility.Hidden;
        }

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            var notificationManager = new NotificationManager();
            if (connection.ConnectionChecker())
            {
                if (entity.User.Any(u => u.Login == loginTB.Text) && (entity.User.Any(u => u.Password == passwordTB.Text) || entity.User.Any(u => u.Password == passwordPB.Password)))
                {
                    Mentor mentor = new Mentor();
                    User user = new User();
                    foreach(var users in entity.User)
                    {
                        if (users.Login == loginTB.Text)
                        {
                            mentor = entity.Mentor.Find(users.ID_Mentor);
                            user = users;
                            break;
                        }
                    }
                    if (user.Is_Administartor == true)
                    {
                        var ActionChoiceWindow = new ActionChoiceWindow();
                        ActionChoiceWindow.Show();
                    }
                    else
                    {
                        var MentorWorkWindow = new SelectMentorWorkWindow(mentor);
                        MentorWorkWindow.Show();
                    }
                    notificationManager.Show("АНО Рука Об Руку", "Вы успешно зашли, "+ mentor.Name, NotificationType.Success);
                    this.Close();
                }
                else
                {
                    notificationManager.Show("АНО Рука Об Руку", "Не удалось войти в систему", NotificationType.Error);
                }
            }
            else
            {
                MessageBox.Show("Нет подключения к базе данных!\nПовторите попытку входа когда появится интернет!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void passwordOpenButton_Click(object sender, RoutedEventArgs e)
        {
            passwordOpenButton.Visibility = Visibility.Hidden;
            passwordCloseButton.Visibility = Visibility.Visible;
            passwordTB.Text = passwordPB.Password;
            passwordTB.Visibility = Visibility.Visible;
            passwordPB.Visibility = Visibility.Hidden;
        }

        private void passwordCloseButton_Click(object sender, RoutedEventArgs e)
        {
            passwordOpenButton.Visibility = Visibility.Visible;
            passwordCloseButton.Visibility = Visibility.Hidden;
            passwordPB.Password = passwordTB.Text;
            passwordTB.Visibility = Visibility.Hidden;
            passwordPB.Visibility = Visibility.Visible;
        }
    }
}
