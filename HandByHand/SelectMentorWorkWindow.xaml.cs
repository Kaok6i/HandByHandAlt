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
    /// Логика взаимодействия для SelectMentorWorkWindow.xaml
    /// </summary>
    public partial class SelectMentorWorkWindow : Window
    {
        Mentor mentor = new Mentor();
        Entities entity = new Entities();
        bool MultitudeDirections = false;
        List<int> ListDirectionsID = new List<int>();
        int SelectedDirectionID = -1;
        public SelectMentorWorkWindow(Mentor mentorInformation)
        {
            InitializeComponent();
            mentor = mentorInformation;
        }

        private void selectBttn_Click(object sender, RoutedEventArgs e)
        {
            if (MultitudeDirections)
            {
                SelectedDirectionID = ListDirectionsID[directionCB.SelectedIndex];
            }
            else
            {
                SelectedDirectionID = entity.Direction.Where(d => d.ID_Mentor == mentor.ID).FirstOrDefault().ID;
            }
            switch (ActivitiesCB.SelectedIndex)
            {
                case 0:
                    var MentorWork = new MentorWorkWindow(mentor, SelectedDirectionID);
                    MentorWork.Show();
                    break;
                case 1:
                    var ChangeWork = new WindowEditClass(mentor, SelectedDirectionID);
                    ChangeWork.Show();
                    break;
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int count = entity.Direction.Count(x => x.ID_Mentor == mentor.ID);
            if (count >= 2)
            {
                MultitudeDirections = true;
                directionCB.Visibility = Visibility.Visible;
                foreach (Direction direction in entity.Direction)
                {
                    if (direction.ID_Mentor == mentor.ID)
                    {
                        directionCB.Items.Add(direction.Name_Direction);
                        ListDirectionsID.Add(direction.ID);
                    }
                }
            }
        }
    }
}
