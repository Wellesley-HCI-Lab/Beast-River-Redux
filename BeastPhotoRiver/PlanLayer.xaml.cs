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
    /// Interaction logic for PlanLayer.xaml
    /// </summary>
    public partial class PlanLayer : ScatterView
    {
        private int AskingToPresent = 0;

        public PlanLayer()
        {
            InitializeComponent();
        }


        private void NotYetButton_Click(object sender, RoutedEventArgs e)
        {
            T1Ready.Visibility = Visibility.Collapsed;
            //T2Ready.Visibility = Visibility.Collapsed;
            //T3Ready.Visibility = Visibility.Collapsed;

            switch (AskingToPresent)
            {
                case 1:
                    T1NoAlert.Visibility = Visibility.Visible;
                    AskingToPresent = 0;
                    break;
                case 2:
                    //T2NoAlert.Visibility = Visibility.Visible;
                    AskingToPresent = 0;
                    break;
                case 3:
                    //T3NoAlert.Visibility = Visibility.Visible;
                    AskingToPresent = 0;
                    break;
                default:

                    break;
            }
        }

    }
}
