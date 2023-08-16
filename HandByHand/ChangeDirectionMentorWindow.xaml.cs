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
    /// Логика взаимодействия для ChangeDirectionMentorWindow.xaml
    /// </summary>
    public partial class ChangeDirectionMentorWindow : Window
    {
        Entities entity = new Entities();
        string nameDirection = "";
        public ChangeDirectionMentorWindow(string oldnameDirection)
        {
            InitializeComponent();
            nameDirection = oldnameDirection;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridMentorsWithDirection.ItemsSource = entity.EditDirection.ToList();
        }

        private void addDirectionMentorBttn_Click(object sender, RoutedEventArgs e)
        {
            EditDirection editDirection = dataGridMentorsWithDirection.SelectedItem as EditDirection;
            try
            {
                if(editDirection != null)
                {
                    Direction direction = new Direction()
                    {
                        ID = editDirection.Direction_ID,
                        Name_Direction = nameDirection,
                        ID_Mentor = editDirection.Mentor_ID
                    };
                    entity.Direction.AddOrUpdate(direction);
                    entity.SaveChanges();
                    MessageBox.Show("Данные были успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    var EditDirWindow = new EditDirectionWindow();
                    EditDirWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись для редактирования!","Уведомление",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var editDrctnWndw = new EditDirectionWindow();
            editDrctnWndw.Show();
            this.Close();
        }

    }
}
