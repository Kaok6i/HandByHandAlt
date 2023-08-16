using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
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
using MessageBox = System.Windows.MessageBox;
using System.Diagnostics;
using Notification.Wpf;
using System.Xml.Linq;

namespace HandByHand
{
    /// <summary>
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {
        Mentor mentor = new Mentor();
        int SelectedDirectionID = -1;
        public DocumentWindow(Mentor mentorInformation, int directionid)
        {
            InitializeComponent();
            mentor = mentorInformation;
            SelectedDirectionID = directionid;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var mntrWrkWndw = new MentorWorkWindow(mentor, SelectedDirectionID);
            mntrWrkWndw.Show();
            this.Close();
        }

        private void choiceBttn_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            var notificationManager = new NotificationManager();
            switch (docActivitiesCB.SelectedIndex)
            {
                case 0:
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Word File| *.doc; *docx";
                    fileDialog.Multiselect = false;
                    if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        path = fileDialog.FileName;
                        try
                        {
                            FileStream fileAdd = new FileStream("Config.config", FileMode.Create);
                            StreamWriter streamWriter = new StreamWriter(fileAdd);
                            streamWriter.Write(path);
                            streamWriter.Close();
                            fileAdd.Close();
                            notificationManager.Show("АНО Рука Об Руку", "Вы успешно добавили файл", NotificationType.Success);
                        }
                        catch
                        {
                            notificationManager.Show("АНО Рука Об Руку", "Не удалось сохранить файл.\nПовторите попытку!", NotificationType.Error);
                        }
                    }
                    break;
                case 1:
                    try
                    {
                        FileStream fileStart = new FileStream("Config.config", FileMode.Open);
                        StreamReader streamReader = new StreamReader(fileStart);
                        path = streamReader.ReadLine();
                        Process.Start(path);
                        fileStart.Close();
                        streamReader.Close();
                        notificationManager.Show("АНО Рука Об Руку", "Вы успешно открыли файл", NotificationType.Success);
                    }
                    catch
                    {
                        notificationManager.Show("АНО Рука Об Руку", "Не удалось открыть файл.\nВозможно файл был перемещен или удален.", NotificationType.Warning);
                    }
                    break;
                case 2:
                    try
                    {
                        FileStream fileDelete = new FileStream("Config.config", FileMode.Create);
                        fileDelete.Close();
                        notificationManager.Show("АНО Рука Об Руку", "Вы успешно удалили файл", NotificationType.Success);
                    }
                    catch
                    {
                        notificationManager.Show("АНО Рука Об Руку", "Не удалось удалить файл.\nПовторите попытку!", NotificationType.Error);
                    }
                    break;
            }
        }
    }
}
