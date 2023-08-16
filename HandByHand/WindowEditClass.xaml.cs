using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для WindowEditClass.xaml
    /// </summary>
    public partial class WindowEditClass : Window
    {
        Mentor mentor = new Mentor();
        Entities entity = new Entities();
        int SelectedDirectionID = -1;
        DateTime? DateStartYear;
        DateTime? DateLastYear;
        DateTime SelectedDateTime;
        public class MonthOfYear
        {
           public int ID { get; set; }
           public string Name { get; set; }

            public MonthOfYear(DateTime date)
            {
                ID = date.Month;
                Name = String.Format(date.ToString("MMMM"));
            }
            public MonthOfYear(int numbermonth,string namemonth)
            {
                ID = numbermonth;
                Name = namemonth;
            }
        }
        ObservableCollection<MonthOfYear> Months = new ObservableCollection<MonthOfYear>();
        ObservableCollection<int> Days = new ObservableCollection<int>();
        ObservableCollection<Class> Classes = new ObservableCollection<Class>();
        public WindowEditClass(Mentor mentorInformation, int directionid)
        {
            InitializeComponent();
            mentor = mentorInformation;
            SelectedDirectionID = directionid;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetComboBoxYearItem();
        }
        private void yearCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetNewYearItem();
        }
        private void GetNewYearItem()
        {
            try
            {
                SelectedDateTime = new DateTime(Convert.ToInt32(yearCB.SelectedItem.ToString()), 1, 1);
                FormClassFromYear(SelectedDateTime);
                GetComboBoxMonth();
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось инициализировать выбранный год для формирования списка", NotificationType.Information);
            }
        }
        private void GetComboBoxYearItem()
        {
            try
            {
                DateStartYear = entity.Class.Where(x => x.ID_Direction == SelectedDirectionID).Min(x => x.Date);
                int StartYear = DateStartYear.Value.Year;
                DateLastYear = entity.Class.Where(x => x.ID_Direction == SelectedDirectionID).Max(x => x.Date);
                int LastYear = DateLastYear.Value.Year;
                for (int i = StartYear; i <= LastYear; i++)
                {
                    yearCB.Items.Add(i.ToString());
                }
                yearCB.SelectedItem = LastYear.ToString();
                SelectedDateTime = (DateTime)DateLastYear;
                FormClassFromYear(SelectedDateTime);
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось сформировать первичный список ", NotificationType.Information);
            }
        }
        private void FormClassFromYear(DateTime date)
        {
            try
            {
                Classes.Clear();    
                foreach (Class classwork in entity.Class)
                {
                    if (classwork.ID_Direction == SelectedDirectionID && classwork.Date.Value.Year == date.Year)
                    {
                        Classes.Add(classwork);
                    }
                }
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось создать сформировать список из нового выбранного года", NotificationType.Information);
            }
        }
        private void monthCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetComboBoxDay();
        }
        private void GetComboBoxMonth()
        {
            try
            {
                Months.Clear();
                var query = Classes.Select(x => x.Date.Value.Month).Distinct().ToList();
                foreach(int date in query)
                {
                    Months.Add(new MonthOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date)));
                }
                monthCB.ItemsSource = Months;
                monthCB.SelectedIndex = Months.Count-1;
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось создать список ", NotificationType.Information);
            }
        }
        private void dayCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void GetComboBoxDay()
        {
            try
            {
                Days.Clear();
                if (monthCB.SelectedIndex != -1)
                {
                    var query = Classes.Select(x => x.Date).Distinct().Where(x => x.Value.Date.Month == Months[monthCB.SelectedIndex].ID);
                    foreach (DateTime day in query)
                    {
                        Days.Add(day.Day);
                    }
                    dayCB.ItemsSource = Days;
                    dayCB.SelectedIndex = Days.Count - 1;
                }
            }
            catch
            {
                var notificationManager = new NotificationManager();
                notificationManager.Show("АНО Рука Об Руку", "Не удалось создать список дней", NotificationType.Information);
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var selectWndw = new SelectMentorWorkWindow(mentor);
            selectWndw.Show();
            this.Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime DT = DateTime.Parse(string.Format("{0}-{1}-{2}",SelectedDateTime.Year, Months[monthCB.SelectedIndex].ID, Days[dayCB.SelectedIndex]));
            var EditWindow = new MentorEditWorkWindow(mentor, SelectedDirectionID, DT);
            EditWindow.Show();
            this.Close();
        }
    }
}
