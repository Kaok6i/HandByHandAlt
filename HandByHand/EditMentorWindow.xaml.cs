﻿using System;
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
    /// Логика взаимодействия для EditMentorWindow.xaml
    /// </summary>
    public partial class EditMentorWindow : Window
    {
        Entities entity = new Entities();
        TestConnection connection = new TestConnection();
        public EditMentorWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var actnChcWndw = new ActionChoiceWindow();
            actnChcWndw.Show();
            this.Close();
        }

        private void addMentorButton_Click(object sender, RoutedEventArgs e)
        {
            var addMntrWndw = new AddMentorWindow();
            addMntrWndw.Show();
            this.Close();
        }
        private void changeMentorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mentorsDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Вы не выбрали запись для изменения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    var result = MessageBox.Show("Вы точно хотите изменить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (mentorsDataGrid.SelectedItem != null && result == MessageBoxResult.Yes)
                    {
                        Mentor mentor = mentorsDataGrid.SelectedItem as Mentor;
                        var changeMntrWndw = new ChangeMentorWindow(mentor);
                        changeMntrWndw.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!","Уведомление", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void DeleteMentorButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mentorsDataGrid.SelectedItem != null)
                {
                    var result = MessageBox.Show("Вы точно хотите удалить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Mentor mentor = mentorsDataGrid.SelectedItem as Mentor;
                        entity.Mentor.Remove(mentor);
                        entity.SaveChanges();
                        mentorsDataGrid.ItemsSource = entity.Mentor.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали строку для удаления!", "Уведомление",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить запись в базе данных!", "Уведомление",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void EditMentorWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            mentorsDataGrid.ItemsSource = entity.Mentor.ToList();
        }
    }
}
