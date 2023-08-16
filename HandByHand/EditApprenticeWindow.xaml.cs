using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
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
using System.Xml.Linq;

namespace HandByHand
{
    /// <summary>
    /// Логика взаимодействия для EditApprenticeWindow.xaml
    /// </summary>
    public partial class EditApprenticeWindow : Window
    {
        Entities entity = new Entities();
        public EditApprenticeWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var actnChcWndw = new ActionChoiceWindow();
            actnChcWndw.Show();
            this.Close();
        }

        private void addApprenticeButton_Click(object sender, RoutedEventArgs e)
        {
            var addApprntcWndw = new AddApprenticeWindow();
            addApprntcWndw.Show();
            this.Close();
        }


        private void changeApprenticeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (apprenticeDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Вы не выбрали запись для изменения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    var result = MessageBox.Show("Вы точно хотите изменить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (apprenticeDataGrid.SelectedItem != null && result == MessageBoxResult.Yes)
                    {
                        Student student = apprenticeDataGrid.SelectedItem as Student;
                        var changeApprntcWndw = new ChangeApprenticeWindow(student);
                        changeApprntcWndw.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteApprenticeMentorButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (apprenticeDataGrid.SelectedItem != null)
                {
                    var result = MessageBox.Show("Вы точно хотите удалить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Student student = apprenticeDataGrid.SelectedItem as Student;
                        entity.Student.Remove(student);
                        entity.SaveChanges();
                        apprenticeDataGrid.ItemsSource = entity.Student.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строку для удаления!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditApprenticeWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            apprenticeDataGrid.ItemsSource = entity.Student.Where(x => x.Start_Year == entity.Student.Max(a => a.Start_Year)).ToList();
            GetComboBoxYearItem();
        }
        private void GetComboBoxYearItem()
        {
            try
            {
                int StartYear = Convert.ToInt32(entity.Student.Min(x => x.Start_Year));
                int LastYear = Convert.ToInt32(entity.Student.Max(x => x.Start_Year));
                for (int i = StartYear; i <= LastYear; i++)
                {
                    yearCB.Items.Add(i.ToString());
                }
                yearCB.SelectedItem = LastYear.ToString();
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось создать список ", NotificationType.Information);
            }
        }

        private void yearCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            apprenticeDataGrid.ItemsSource = entity.Student.Where(x => x.Start_Year == yearCB.SelectedItem.ToString()).ToList();
        }
    }
}
