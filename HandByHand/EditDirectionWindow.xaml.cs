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
    /// Логика взаимодействия для EditDirectionWindow.xaml
    /// </summary>
    public partial class EditDirectionWindow : Window
    {
        Entities entity = new Entities();
        public EditDirectionWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var actnChcWndw = new ActionChoiceWindow();
            actnChcWndw.Show();
            this.Close();
        }

        private void addDirectionButton_Click(object sender, RoutedEventArgs e)
        {
            var addDrctnWndw = new AddDirectionWindow();
            addDrctnWndw.Show();
            this.Close();
        }
        private void changeDirectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (directionsDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Вы не выбрали запись для изменения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    var result = MessageBox.Show("Вы точно хотите изменить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (directionsDataGrid.SelectedItem != null && result == MessageBoxResult.Yes)
                    {
                        EditDirection direction = directionsDataGrid.SelectedItem as EditDirection;
                        var changeDrctnWndw = new ChangeDirectionWindow(direction);
                        changeDrctnWndw.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось изменить запись в базе данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditDirectionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            directionsDataGrid.ItemsSource = entity.EditDirection.ToList();
        }

        private void deleteDirectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (directionsDataGrid.SelectedItem != null)
                {
                    var result = MessageBox.Show("Вы точно хотите удалить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        int rowReceiptSelected = (directionsDataGrid.SelectedItem as EditDirection).Direction_ID;
                        Direction direction = (from d in entity.Direction where d.ID == rowReceiptSelected select d).SingleOrDefault();
                        entity.Direction.Remove(direction);
                        entity.SaveChanges();
                        directionsDataGrid.ItemsSource = entity.EditDirection.ToList();
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
    }
}
