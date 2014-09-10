using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace BeastPhotoRiver
{
    /// <summary>
    /// Interaction logic for DiscussLayer.xaml
    /// </summary>
    public partial class DiscussLayer : ScatterView
    {
        public DiscussLayer()
        {
            InitializeComponent();
        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            SurfaceButton lockButton = sender as SurfaceButton;
            ScatterViewItem lockMomma = lockButton.Parent as ScatterViewItem;
            if (lockButton.Name.Equals("Team1Lock"))
            {
                foreach (ScatterViewItem s in this.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            else if (lockButton.Name.Equals("Team2Lock"))
            {
                foreach (ScatterViewItem s in this.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            else//team3
            {
                foreach (ScatterViewItem s in this.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            lockMomma.Visibility = Visibility.Collapsed;
        }

      
    }
}
