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
    /// Логика взаимодействия для AddDirectionMentorWindow.xaml
    /// </summary>
    public partial class AddDirectionMentorWindow : Window
    {
        Entities entity = new Entities();
        string nameDirection = "";
        List<Mentor> mentorsList = new List<Mentor>();
        List<int> usedmentors = new List<int>();
        public AddDirectionMentorWindow(string Direction)
        {
            InitializeComponent();
            nameDirection = Direction;
        }

        private void AddDirectionMentorWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
                foreach(EditDirection direction in entity.EditDirection)
                {
                    usedmentors.Add(direction.Mentor_ID);
                }
                foreach (Mentor mentor in entity.Mentor)
                {
                        if (!(usedmentors.Contains(mentor.ID)))
                        {
                            mentorsList.Add(mentor);
                        } 
                }
            dataGridMentorsWithoutDirection.ItemsSource = mentorsList.ToList();
        }

        private void AddDirectionMentorBttn_OnClick(object sender, RoutedEventArgs e)
        {
            Mentor mentor = dataGridMentorsWithoutDirection.SelectedItem as Mentor;
            if (mentor != null)
            {
                try
                {
                    Direction direction = new Direction()
                    {
                        Name_Direction = nameDirection,
                        ID_Mentor = mentor.ID,
                    };
                    entity.Direction.Add(direction);
                    entity.SaveChanges();
                    MessageBox.Show("Данные были успешно загружены в базу данных","Уведомление",MessageBoxButton.OK,MessageBoxImage.Information);
                    var EditDrctnWndw = new EditDirectionWindow();
                    EditDrctnWndw.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось добавить запись в базу данных!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var addDrctnWndw = new AddDirectionWindow();
            addDrctnWndw.Show(); 
            this.Close();
        }
    }
}
