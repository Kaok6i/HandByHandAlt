using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MentorEditWorkWindow.xaml
    /// </summary>
    public partial class MentorEditWorkWindow : Window
    {
        Entities entity = new Entities();
        Mentor mentor = new Mentor();
        DateTime SelectedDateTime;
        ObservableCollection<Class> ClassList = new ObservableCollection<Class>();
        int SelectedDirectionID = -1;
        public MentorEditWorkWindow(Mentor mentorInformation, int directionid, DateTime dateTime)
        {
            InitializeComponent();
            mentor = mentorInformation;
            SelectedDirectionID = directionid;
            SelectedDateTime = dateTime;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            directionTB.Text = entity.Direction.First(x => x.ID == SelectedDirectionID).Name_Direction;
            if (directionTB.Text.Length > 18)
            {
                directionTB.FontSize = 24;
            }
            themeTB.Text = entity.Class.First(x => x.Date == SelectedDateTime).Theme_Class;
            datePicker.SelectedDate = SelectedDateTime;
            foreach (Class studentClass in entity.Class)
            {
                if (datePicker.SelectedDate == studentClass.Date && studentClass.ID_Direction == SelectedDirectionID)
                {
                    ClassList.Add(studentClass);
                }
            }
            gradebookDataGrid.ItemsSource = ClassList;
            MentorWorkClass commentList = new MentorWorkClass();
            CommentCB.ItemsSource = MentorWorkClass.CommentList.ToList();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var ChangeWork = new WindowEditClass(mentor, SelectedDirectionID);
            ChangeWork.Show();
            this.Close();
        }

        private void documentButton_Click(object sender, RoutedEventArgs e)
        {
            var docWndw = new DocumentWindow(mentor, SelectedDirectionID);
            docWndw.Show();
            this.Close();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < ClassList.Count; i++)
                {
                    Class mentorClass = new Class
                    {
                        ID = ClassList[i].ID,
                        Theme_Class = themeTB.Text,
                        ID_Direction = SelectedDirectionID,
                        ID_Student = ClassList[i].ID_Student,
                        Comment = ClassList[i].Comment,
                        Time_Passed = 1,
                        Date = SelectedDateTime
                    };
                    entity.Class.AddOrUpdate(mentorClass);
                    entity.SaveChanges();
                }
                MessageBox.Show("Данные были успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Не удалось добавить запись в базу данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
