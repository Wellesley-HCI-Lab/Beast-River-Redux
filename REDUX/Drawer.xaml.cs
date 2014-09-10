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
using Microsoft.Surface.Presentation.Controls.Primitives;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace REDUX
{
    /// <summary>
    /// Interaction logic for Drawer.xaml
    /// </summary>
    public partial class Drawer : ScatterViewItem
    {
        public SurfaceWindow1 _surfaceWindow;
        private int ChangeModeButtonCounter;
        private bool _top;
        public const int DRAWER_MIDDLE_X = 540;
        public const int DRAWER_MIDDLE_Y = 90;//top
        public const int DRAWER_BTMMIDDLE_Y = 150;//top
        public SolidColorBrush DISABLED_BUTTON_COLOR = new SolidColorBrush(new Color { A = 255, B = (byte)99, G = (byte)146, R = (byte)183 });
        public SolidColorBrush ENABLED_BUTTON_COLOR = Brushes.SkyBlue;



        #region server stuff

        IPAddress ipAd = IPAddress.Parse("149.130.165.23");
        TcpClient client = new TcpClient();
        IPEndPoint tabletendpoint = new IPEndPoint(IPAddress.Parse("149.130.195.40"),3000 );
        IPEndPoint beastendpoint = new IPEndPoint(IPAddress.Parse("149.130.165.23"), 3000); 

        ASCIIEncoding encoding = new ASCIIEncoding();

        #endregion

        public Drawer()
        {
            InitializeComponent();
            

        }




        public SurfaceWindow1 MainSurfaceWindow
        {
            set
            { _surfaceWindow = value; }
        }

        /// <summary>
        /// Top property tells you whether the drawer belongs to the top of screen.
        /// </summary>
        public bool Top
        {
            get { return _top; }
        }

        /// <summary>
        /// This method is called if the drawer belongs to the top half of the screen
        /// Sets the orientation and the _top property.
        /// </summary>
        public void setTop()
        {
            this.DrawerSVI.Orientation = 180;
            DrawerSVI.Center = new Point(DRAWER_MIDDLE_X, DRAWER_MIDDLE_Y);
            _top = true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
    
        public void SyncButtonsForRiver()
        {
            ToRiver.Background = ENABLED_BUTTON_COLOR;

            ToPlan.Background = DISABLED_BUTTON_COLOR;
            ToDiscuss.Background = DISABLED_BUTTON_COLOR; 
        }

        /// <summary>
        /// Called when the river button is clicked. 
        /// Meant to take the users back to the river.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RiverButton_Click(object sender, RoutedEventArgs e)
        {
            ToRiver.Background = ENABLED_BUTTON_COLOR;

            ToPlan.Background = DISABLED_BUTTON_COLOR;            
            ToDiscuss.Background = DISABLED_BUTTON_COLOR; 
            

            if (ChangeModeButtonCounter >= SurfaceWindow1.READY_TO_SWITCH_MODE_NUMBER)
            { 
                _surfaceWindow.SwitchToRiver(this.Name);

                ChangeModeButtonCounter = 0;
            }
        }

        public void SyncButtonForPlan()
        {
            ToPlan.Background = ENABLED_BUTTON_COLOR;

            ToRiver.Background = DISABLED_BUTTON_COLOR;
            ToDiscuss.Background = DISABLED_BUTTON_COLOR;
        }

        /// <summary>
        /// Called when the plan button is clicked.
        /// Meant to take the users to the plan layer. 
        /// Need to keep track of when to switch to plan layer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlanButton_Click(object sender, RoutedEventArgs e)
        {
            ToPlan.Background = ENABLED_BUTTON_COLOR;

            ToRiver.Background = DISABLED_BUTTON_COLOR;
            ToDiscuss.Background = DISABLED_BUTTON_COLOR;

            ChangeModeButtonCounter++;
            
            if (ChangeModeButtonCounter >= SurfaceWindow1.READY_TO_SWITCH_MODE_NUMBER) //change to EIGHT later.
            {
                _surfaceWindow.SwitchToPlanLayout(this.Name);
                
                ChangeModeButtonCounter = 0;
            }
        }

        public void SyncButtonsAsPresenter()
        {
            ToPlan.Background = DISABLED_BUTTON_COLOR;
            ToRiver.Background = DISABLED_BUTTON_COLOR;

            ToDiscuss.Background = ENABLED_BUTTON_COLOR;
        }

        public void ToDiscuss_Checked(object sender, RoutedEventArgs e)
        {
            ToPlan.Background = DISABLED_BUTTON_COLOR;
            ToRiver.Background = DISABLED_BUTTON_COLOR;

            ToDiscuss.Background = ENABLED_BUTTON_COLOR;
                        
            if (ChangeModeButtonCounter >= SurfaceWindow1.READY_TO_SWITCH_MODE_NUMBER)
            {
                if(this.Name.Contains("1"))
                {
                    _surfaceWindow.CopyForTeam1Presentation();
                }else if(this.Name.Contains("2"))
                {
                    _surfaceWindow.CopyForTeam2Presentation();
                }else{
                    _surfaceWindow.CopyForTeam3Presentation(); 
                }
                
                ChangeModeButtonCounter = 0;
            }
        }

        public void SyncButtonsAsAudience()
        {
            ToPlan.IsEnabled = false;
            ToRiver.IsEnabled = false;
            ToDiscuss.IsEnabled = false;
        }

        public void SyncButtonsOutOfAudience()
        {
            ToPlan.IsEnabled = true;
            ToRiver.IsEnabled = true;
            ToDiscuss.IsEnabled = true;
        }

        /// <summary>
        /// Handles docking for drawers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawerSVI_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e)
        {   
            if (_top)
            {//one of the top drawers
                if (DrawerSVI.Center.Y < 150)
                {
                    DrawerSVI.Center = new Point(DRAWER_MIDDLE_X, DRAWER_MIDDLE_Y);
                    DrawerSVI.Orientation = 180;
                }
            }
            else
            {//one of the bottom drawers
                if (DrawerSVI.Center.Y > 250)
                {
                    DrawerSVI.Center = new Point(DRAWER_MIDDLE_X, DRAWER_BTMMIDDLE_Y);
                    DrawerSVI.Orientation = 0;
                }
            }
        }

        /// <summary>
        /// Called when drawer is moved. 
        /// Meant to make it move only up and down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawerSVI_ContainerManipulationDelta(object sender, ContainerManipulationDeltaEventArgs e)
        {
            //if ((e.ManipulationOrigin.Y > 20))
            //{
                DrawerSVI.Center = new Point(DRAWER_MIDDLE_X, DrawerSVI.Center.Y);
            //}
            Console.WriteLine("Touch" + e.ManipulationOrigin.Y);
            
        }

        private void SurfaceToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }



        internal void send(string p)
        {
            int i = p.IndexOf("Pics");
            String sendstring = p.Substring(i + 5);
            //p = p.Replace("\\\\", "|");
            //String[] splitarray = p.Split('|');
            //tring sendstring = splitarray[splitarray.Length];
            try
            {
                client.Connect(tabletendpoint);
                NetworkStream clientStream = client.GetStream();
                Byte[] buffer = encoding.GetBytes(sendstring);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
                clientStream.Close();
                client = new TcpClient();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    


}
