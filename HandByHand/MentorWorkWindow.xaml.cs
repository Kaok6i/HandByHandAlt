using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для MentorWorkWindow.xaml
    /// </summary>
    public partial class MentorWorkWindow : Window
    {
        Entities entity = new Entities();
        Mentor mentor = new Mentor();
        List<MentorWorkClass> ClassList = new List<MentorWorkClass>();
        int SelectedDirectionID = -1;

        public MentorWorkWindow(Mentor mentorInformation, int directionid)
        {
            InitializeComponent();
            mentor = mentorInformation;
            SelectedDirectionID = directionid;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            directionTB.Text = entity.Direction.Where(x => x.ID == SelectedDirectionID).FirstOrDefault().Name_Direction;
            datePicker.SelectedDate = DateTime.Now;
            if (directionTB.Text.Length > 18)
            {
                directionTB.FontSize = 24;
            }
            foreach (Student student in entity.Student)
            {
                if (student.ID_Direction == SelectedDirectionID)
                {
                    MentorWorkClass newStudent = new MentorWorkClass();
                    newStudent = newStudent.ConvertStudent(student);
                    ClassList.Add(newStudent);
                }
            }
            gradebookDataGrid.ItemsSource = ClassList;
            CommentCB.ItemsSource = MentorWorkClass.CommentList.ToList();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (themeTB.Text != null && themeTB.Text != "")
                {
                    DateTime date = DateTime.Parse(DateTime.Now.ToShortDateString());
                    if (entity.Class.Any(x => x.Date == date && x.ID_Direction == SelectedDirectionID))
                    {
                        MessageBox.Show("Нельзя создать новую запись!\nДля редактирования, перейдите в другое окно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (ClassList.Any(x => x.Comment == "" || x.Comment == null))
                        {
                            MessageBox.Show("Вы забыли добавить ученику комментарий", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            for (int i = 0; i < ClassList.Count; i++)
                            {
                                Class mentorClass = new Class
                                {
                                    Theme_Class = themeTB.Text,
                                    ID_Direction = SelectedDirectionID,
                                    ID_Student = ClassList[i].ID,
                                    Comment = ClassList[i].Comment,
                                    Time_Passed = 1,
                                    Date = date
                                };
                                entity.Class.Add(mentorClass);
                                entity.SaveChanges();
                            }
                            MessageBox.Show("Данные были успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Вы забыли добавить тему урока", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось добавить запись в базу данных", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void documentButton_Click(object sender, RoutedEventArgs e)
        {
            var docWndw = new DocumentWindow(mentor, SelectedDirectionID);
            docWndw.Show();
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var selectWndw = new SelectMentorWorkWindow(mentor);
            selectWndw.Show();
            this.Close();
        }
    }
}
