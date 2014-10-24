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
using System.Windows.Threading;
using BeastPhotoRiver.PhotoCard;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;

delegate void Callback(ScatterViewItem svi, BitmapImage bitmapImageForImageContainer, String[] photoInfo, String authorInfo);


namespace BeastPhotoRiver
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        #region Foundation Code: Variables, 8 Functions: UpdateImagePositions,_change_timer_Tick, UpdateImageContainer, CreateImageContainer, svi_ContainerManipulationStarted, TransferCopyToNewScatterView

        #region Variables

        public const int SCREEN_WIDTH = 3240;
        public const int SCREEN_HEIGHT = 1850;
        public const int MIN_SIZE = 300;
        public const int MAX_SIZE = 1000;

        public const int END_OF_FIRST_DRAWER = 1080;
        public const int END_OF_SECOND_DRAWER = 2160;
        public const int END_OF_THIRD_DRAWER = 3240;

        public const int READY_TO_SWITCH_MODE_NUMBER = 0;

        public String _pictures_directory = "";

        public int off_screen_counter = 0;
        private bool _ok_to_update = true;
        private String[] _images_paths_array_of_strings;
        public Random _random = new Random();
        public DispatcherTimer _timer = new DispatcherTimer();
        public DispatcherTimer _fade_timer = new DispatcherTimer();
        public List<ScatterViewItem> _fade_list = new List<ScatterViewItem>();
        private PhotoGrabber _photo_grabber;
        private delegate void NoArgDelegate();
        private delegate void OneArgDelegate(ScatterViewItem svi);
        private delegate ScatterViewItem OneArgOneReturnDelegate(ScatterViewItem svi);
        private Dispatcher mainDispatcher = Dispatcher.CurrentDispatcher;
        public DispatcherTimer _change_timer = new DispatcherTimer();
        private int ChangeModeButtonCounter;


        #endregion


        #region Variables and DLL Imports for multiple keyboards

        //private variables
        private InputDevice id;
        private int _numberOfKeyboards;
        private Message message = new Message();
        private HwndSource _mainHwndSource;

        private Hashtable _keyboardList;
        private ArrayList _keyboardArray;

        //dll import (IMPORTANT for mapping virtual keys!)
        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);
        #endregion


        TcpListener listener;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();

            string currentDir = Environment.CurrentDirectory;
            int index = currentDir.IndexOf("bin");

            currentDir = currentDir.Substring(0, index);
            _pictures_directory = currentDir + "Resources\\ARTeMuse Pics\\";

            InitUI();
            SetDrawers();


            #region listens to tablets
            //threading listener, will need to process inputs from all tablets
            
            //listener    = new TcpListener(IPAddress.Parse("149.130.165.23"), 3000);
            //listener.Start();
            //ThreadStart start = delegate()
            //{
            //    while (true)
            //    {
            //        try{
            //            Socket s = listener.AcceptSocket();
            //            byte[] b = new byte[100];
            //            s.Receive(b);
            //            String recieved = ASCIIEncoding.ASCII.GetString(b);
            //            recieved = recieved.Trim('\0');

            //            if (recieved.Contains("BROWSE"))
            //            {
            //                this.Dispatcher.Invoke((Action)(() =>
            //                            {
            //                                BottomDrawer1.RiverButton_Click(this, new RoutedEventArgs());

            //                            }
            //                ));
            //            }
            //            else if (recieved.Contains("PLAN"))
            //            {
            //                this.Dispatcher.Invoke((Action)(() =>
            //                            {
            //                                BottomDrawer1.PlanButton_Click(this, new RoutedEventArgs());
            //                            }
            //                ));
            //            }
            //            else if (recieved.Contains("DISCUSS"))
            //            {
            //                this.Dispatcher.Invoke((Action)(() =>
            //                            {
            //                                BottomDrawer1.ToDiscuss_Checked(this, new RoutedEventArgs());
            //                            }
            //                ));
            //            }
            //            else
            //            {//assume sending pic
            //                foreach (object o in BottomDrawer1.ActualDrawer.Items)
            //                {
            //                    Thumbnail t = o as Thumbnail;
            //                    if (t.MyPhotoCard._imageString.Contains(recieved))
            //                    {
            //                        PhotoCard.PhotoCard cardBeingTakenFromDrawer = t.MyPhotoCard;
            //                        ScatterView parentSV = t.Parent as ScatterView;
            //                        Grid gr = parentSV.Parent as Grid;
            //                        ScatterViewItem s2 = gr.Parent as ScatterViewItem;
            //                        ScatterView s3 = s2.Parent as ScatterView;//actual parent
            //                        ScatterViewItem parent = s3.Parent as ScatterViewItem;

            //                        this.Dispatcher.Invoke((Action)(() =>
            //                            {
            //                                //PhotoCard.PhotoCard copyCard = cardBeingTakenFromDrawer.DeepClone();
            //                                PhotoCard.PhotoCard copyCard = cardBeingTakenFromDrawer;
            //                                Double yPoint = t.Center.Y;
            //                                Double xPoint = t.Center.X;

            //                                if (PlanScatterViewLeft.Visibility == Visibility.Visible)
            //                                    PlanScatterViewLeft.Items.Add(copyCard);
            //                                else if (DiscussScatterView.Visibility == Visibility.Visible)
            //                                    DiscussScatterView.Items.Add(copyCard);
            //                                else
            //                                    RiverScatterView.Items.Add(copyCard);
            //                                BottomDrawer1.ActualDrawer.Items.Remove(t);
            //                                copyCard.Center = new Point(_random.Next(0, END_OF_FIRST_DRAWER), _random.Next(925, auto));

            //                            }));
            //                    }


            //                }
            //            }
            //        }
            //        catch (Exception e) { Console.WriteLine(e); 
                        
            //        }
            //    }
            //               };
            //new Thread(start).Start();
            #endregion //commented out to fix xaml parse

            #region code for initializing multiple keyboards
            // Add handlers for window availability events
            //AddWindowAvailabilityHandlers();

            //this.SourceInitialized += new EventHandler(OnSourceInitialized);

            //_keyboardList = new Hashtable();
            #endregion
        }

        public void InitUI()
        {
            //TODO: get rid of this here - for testing
            //EmailWindow em = new EmailWindow();
            //em.Name = "Copy";
            //ScatterViewItem temp = new ScatterViewItem();
            //temp.Content = em;
            //temp.Height = 300;
            //temp.Width = 300;
            //temp.Center = new Point(1, 200);         

            _photo_grabber = new PhotoGrabber(_pictures_directory);
            _images_paths_array_of_strings = _photo_grabber.GetBigPhotos();

            //add imageSVIs to scatter ScatterView
            for (int i = 0; i < 13; i++)
            {
                CreateImageContainer(i);
            }

            #region Timers
            // Create a timer to update the imageSVIs every 10 milliseconds.
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            _timer.Tick += UpdateImagePositions;
            _timer.Start();

            //Create a timer that will update image containers every two seconds
            _change_timer = new DispatcherTimer();
            _change_timer.Interval = new TimeSpan(0, 0, 0, 2, 0);
            _change_timer.Tick += new EventHandler(_change_timer_Tick);
            _change_timer.Start();
            #endregion
        }

        private void SetDrawers()
        {
            //TopDrawer1._surfaceWindow = this;

            //TopDrawer1.setTop();
            //TopDrawer2._surfaceWindow = this;
            //TopDrawer2.setTop();
            //TopDrawer3._surfaceWindow = this;
            //TopDrawer3.setTop();


            //BottomDrawer1._surfaceWindow = this;
            //BottomDrawer2._surfaceWindow = this;
            //BottomDrawer3._surfaceWindow = this;

        }

        /// <summary>
        /// H. Wang, C. Valdes
        /// For each tick of the _timer, images are moved to the right by incrementing their center x coordinate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateImagePositions(object sender, EventArgs e)
        {
            for (int i = 0; i < RiverScatterView.Items.Count; i++)
            {
                ScatterViewItem imageView = (ScatterViewItem)RiverScatterView.Items.GetItemAt(i);
                if (imageView.Name.Equals("play"))
                {
                    Point position = imageView.Center;
                    double newX = (position.X + 1);
                    // Set the new position of the imageViewer
                    Point newPosition = new Point(newX, position.Y);

                    if (newX > (3240 + imageView.Width + 200))//beyond screen
                    {//get rid of old imageSVI and grab a new one
                        if (!_ok_to_update)
                        {
                            if (_fade_list.Contains(imageView))
                                _fade_list.Remove(imageView);

                            imageView.Center = new Point(-150, 1000);
                        }
                        else
                        {
                            _ok_to_update = false;
                            ThreadStart start = delegate()
                            {
                                int m = _random.Next(_images_paths_array_of_strings.Length);
                                BitmapImage bitmapImageForImageFront = new BitmapImage(new Uri(_images_paths_array_of_strings[m]));
                            };
                            new Thread(start).Start();
                        }
                    }
                    else//move to right
                    {
                        newX = (position.X + 1);
                        imageView.Center = newPosition;
                    }
                }
            }

        }

        void _change_timer_Tick(object sender, EventArgs e)
        {
            _ok_to_update = true;
        }

        /// <summary>
        /// C. Valdes
        /// Grabs image path from images array and gets all pertinent info, creates customUCs for ImageFront & ImageBack
        /// Adds them to an SVI with UI customizations. Sets center to left (off-screen) and adds to svi rotation list.
        /// </summary>
        /// <param name="i">int for reference to _images_paths_array_of_strings</param>
        public void UpdateImageContainer(ScatterViewItem svi, BitmapImage bitmapImageForImageContainer, String[] photoInfo, String authorInfo)
        {
            //Viewbox vb1 = (Viewbox)svi.Content;
            PhotoCard.PhotoCard imageCard = (PhotoCard.PhotoCard)svi.Content;
            //update frontpanel           
            imageCard.imgFront.Source = bitmapImageForImageContainer;
            ////update backpanel
            imageCard.imgBack.Source = bitmapImageForImageContainer;
            svi.Name = "play";
            svi.Center = new Point(0, 1000);
        }

        /// <summary>
        /// C. Valdes
        /// Grabs image path from images array and gets all pertinent info, creates customUCs for ImageFront & ImageBack
        /// Adds them to an SVI with UI customizations. Sets center to left (off-screen) and adds to svi rotation list.
        /// </summary>
        /// <param name="i">int for reference to _images_paths_array_of_strings</param>
        public void CreateImageContainer(int i)
        {
            BitmapImage bitmapImageForFrontAndBack = new BitmapImage(new Uri(_images_paths_array_of_strings[i]));
            double w = bitmapImageForFrontAndBack.Width;
            double h = bitmapImageForFrontAndBack.Height;

            //photocard
            PhotoCard.PhotoCard imageCard = new PhotoCard.PhotoCard();
            imageCard.ArrayIndex = i;
            imageCard.ImageString = _images_paths_array_of_strings[i];
            imageCard.imgFront.Source = bitmapImageForFrontAndBack;
            imageCard.imgFront.Stretch = Stretch.Uniform;
            //imageCard.imgBack.Source = bitmapImageForFrontAndBack;
            //imageCard.Thumbnail.ThumbImage.Source = bitmapImageForFrontAndBack;   
            imageCard.Name = "play";
            imageCard.CanScale = false;
            imageCard.ContainerManipulationStarted += new ContainerManipulationStartedEventHandler(svi_ContainerManipulationStarted);
            ///imageCard.Center = new Point(300 * i, 1000);
            //imageCard.Center = new Point(300 * i, 500);
            imageCard.Center = new Point(300 * i, this.ActualHeight / 2);
            imageCard.Orientation = 90;

            this.RiverScatterView.Items.Add(imageCard);
        }

        /// <summary>
        /// Called on photos in the river. Copies the one you touched 
        /// (shows up underneath original) to replace it in the river.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void svi_ContainerManipulationStarted(object sender, ContainerManipulationStartedEventArgs e)
        {
            PhotoCard.PhotoCard originalSVI = sender as PhotoCard.PhotoCard;
            RiverScatterView.Items.Add(TransferCopyToNewScatterView(originalSVI));
        }

        /// <summary>
        /// Takes an SVI, copies everything about it and 
        /// returns the copy as a "Copy." Won't be animated as if in river
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public PhotoCard.PhotoCard TransferCopyToNewScatterView(PhotoCard.PhotoCard original)
        {
            PhotoCard.PhotoCard copyCard = (PhotoCard.PhotoCard)original.DeepClone();
            original.Name = "Copy";
            original.ContainerManipulationStarted -= new ContainerManipulationStartedEventHandler(svi_ContainerManipulationStarted);
            original.ContainerManipulationCompleted += new ContainerManipulationCompletedEventHandler(CopiedSVI_ContainerManipulationCompleted);
            copyCard.Name = "play";
            copyCard.BorderThickness = new Thickness(0);
            copyCard.CanScale = false;
            copyCard.Center = original.Center;
            copyCard.Orientation = original.Orientation;
            copyCard.ContainerManipulationStarted += new ContainerManipulationStartedEventHandler(svi_ContainerManipulationStarted);
            
            //buttons for voting and closing appear
            original.showButtons();
            
            return copyCard;
        }

        #endregion

        #region Emaling: 1 Function: SendMessage
        /// <summary>
        /// Sends an e-mail message using the designated SMTP mail server.
        /// </summary>
        /// <param name="subject">The subject of the message being sent.</param>
        /// <param name="messageBody">The message body.</param>
        /// <param name="toAddress">The recipient's e-mail address (separate multiple e-mail addresses
        /// with a semi-colon).</param>
        /// <param name="ccAddress">The address(es) to be CC'd (separate multiple e-mail addresses with
        /// a semi-colon).</param>
        /// <remarks>You must set the SMTP server within this method prior to calling.</remarks>
        /// <example>
        /// <code>
        ///   // Send a quick e-mail message
        ///   SendEmail.SendMessage("This is a Test", 
        ///                         "This is a test message...",
        ///                         "somebody@somewhere.com", 
        ///                         "ccme@somewhere.com");
        /// </code>
        /// </example>
        public static void SendMessage(string subject, string messageBody, string toAddress, string ccAddress)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();

            // Set the sender's address
            message.From = new MailAddress("wellesleyhcilab@gmail.com");

            // Allow multiple "To" addresses to be separated by a semi-colon
            if (toAddress.Trim().Length > 0)
            {
                foreach (string addr in toAddress.Split(';'))
                {
                    message.To.Add(new MailAddress(addr));
                }
            }

            // Allow multiple "Cc" addresses to be separated by a semi-colon
            if (ccAddress.Trim().Length > 0)
            {
                foreach (string addr in ccAddress.Split(';'))
                {
                    message.CC.Add(new MailAddress(addr));
                }
            }

            // Set the subject and message body text
            message.Subject = subject;
            message.Body = messageBody;

            // TODO: *** Modify for your SMTP server ***
            // Set the SMTP server to be used to send the message
            client.Host = "smtp.gmail.com";

            // Send the e-mail message
            client.Send(message);
        }
        #endregion

        #region For deletion and adding to drawer of Photocards: 2 Functions: CopiedSVI_ContainerManipulationCompleted,SavedSVI_ContainerManipulationCompleted

        /// <summary>
        /// Saves SVI into correct Drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopiedSVI_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e)
        {
            //check for center and see if it's close to a drawer
            PhotoCard.PhotoCard cardThrownInFromMainSV = sender as PhotoCard.PhotoCard;

            Double yPoint = cardThrownInFromMainSV.Center.Y;
            Double xPoint = cardThrownInFromMainSV.Center.X;

            //if near edge check which drawer it should go into TOP or BOTTOM
            if (yPoint > 1700 || yPoint < 0)
            {
                PhotoCard.PhotoCard copyCard = (PhotoCard.PhotoCard)cardThrownInFromMainSV.DeepClone();
                copyCard.ArrayIndex = cardThrownInFromMainSV.ArrayIndex;
                //copyCard.TagList = cardThrownInFromMainSV.TagList;
                copyCard.Orientation = cardThrownInFromMainSV.Orientation;
                String sendit =copyCard.imgFront.Source.ToString();
                copyCard.Name = "Copy";
                copyCard.CanScale = false;

                ScatterView currentParent = cardThrownInFromMainSV.Parent as ScatterView;

                /*
                //for copying into drawer
                if (yPoint > 1700) //bottom half of screen
                {
                    if (xPoint < END_OF_FIRST_DRAWER)
                    {
                        BottomDrawer1.ActualDrawer.Items.Add(copyCard.Thumbnail);
                        //PUT TRANSFER CODE HERE!!!!
                        //BottomDrawer1.send(cardThrownInFromMainSV.Name);
                        BottomDrawer1.send(sendit);
                        currentParent.Items.Remove(cardThrownInFromMainSV);
                    }
                    else if (END_OF_FIRST_DRAWER < xPoint && xPoint < END_OF_SECOND_DRAWER)
                    {
                        BottomDrawer2.ActualDrawer.Items.Add(copyCard.Thumbnail);
                        currentParent.Items.Remove(cardThrownInFromMainSV);
                    }
                    else if (END_OF_SECOND_DRAWER < xPoint && xPoint < END_OF_THIRD_DRAWER)
                    {
                        BottomDrawer3.ActualDrawer.Items.Add(copyCard.Thumbnail);
                        currentParent.Items.Remove(cardThrownInFromMainSV);
                    }

                }
                else// for y <925 aka in top half of screen
                {
                    if (xPoint < END_OF_FIRST_DRAWER)
                    {
                        try
                        {
                            TopDrawer1.ActualDrawer.Items.Add(copyCard.Thumbnail);
                            currentParent.Items.Remove(cardThrownInFromMainSV);
                        }
                        catch (Exception exc)
                        {
                            Console.WriteLine(exc.Message.ToString());
                        }
                    }
                    else if (END_OF_FIRST_DRAWER < xPoint && xPoint < END_OF_SECOND_DRAWER)
                    {
                        TopDrawer2.ActualDrawer.Items.Add(copyCard.Thumbnail);
                        currentParent.Items.Remove(cardThrownInFromMainSV);
                    }
                    else if (END_OF_SECOND_DRAWER < xPoint && xPoint < END_OF_THIRD_DRAWER)
                    {
                        TopDrawer3.ActualDrawer.Items.Add(copyCard.Thumbnail);
                        currentParent.Items.Remove(cardThrownInFromMainSV);
                    }

                }
                */

                copyCard._thumbnail.ContainerManipulationCompleted += new ContainerManipulationCompletedEventHandler(SavedSVI_ContainerManipulationCompleted);
            }
        }



        /// <summary>
        /// Takes SVI out of drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SavedSVI_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e)
        {

            /*
            Thumbnail thumbnailInDrawer = (Thumbnail)sender;
            PhotoCard.PhotoCard cardBeingTakenFromDrawer = thumbnailInDrawer.MyPhotoCard;
            ScatterView parentSV = thumbnailInDrawer.Parent as ScatterView;
            Grid gr = parentSV.Parent as Grid;
            ScatterViewItem s2 = gr.Parent as ScatterViewItem;
            ScatterView s3 = s2.Parent as ScatterView;//actual parent
            ScatterViewItem parent = s3.Parent as ScatterViewItem;

            PhotoCard.PhotoCard copyCard = cardBeingTakenFromDrawer.DeepClone();
            Double yPoint = thumbnailInDrawer.Center.Y;
            Double xPoint = thumbnailInDrawer.Center.X;

            Console.WriteLine("copycARD parent...." + s2.Name);

            if (yPoint < 50)
            {
                copyCard.Orientation = cardBeingTakenFromDrawer.Orientation;
                copyCard.Name = "Copy";
                copyCard.CanScale = false;

                //check which layer we're in and add it there...

                //taking photo out of drawer
                switch (parent.Name)
                {
                    case "BottomDrawer1":
                        if (PlanScatterViewLeft.Visibility == Visibility.Visible)
                            PlanScatterViewLeft.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        BottomDrawer1.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(0, END_OF_FIRST_DRAWER), _random.Next(925, auto));
                        break;
                    case "BottomDrawer2":
                        if (PlanScatterViewMiddle.Visibility == Visibility.Visible)
                            PlanScatterViewMiddle.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        BottomDrawer2.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(END_OF_FIRST_DRAWER, END_OF_SECOND_DRAWER), _random.Next(925, auto));
                        break;
                    case "BottomDrawer3":
                        if (PlanScatterViewRight.Visibility == Visibility.Visible)
                            PlanScatterViewRight.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        BottomDrawer3.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(END_OF_SECOND_DRAWER, END_OF_THIRD_DRAWER), _random.Next(925, auto));
                        break;
                    case "TopDrawer1":
                        if (PlanScatterViewLeft.Visibility == Visibility.Visible)
                            PlanScatterViewLeft.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        TopDrawer1.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(0, END_OF_FIRST_DRAWER), _random.Next(0, 925));
                        break;
                    case "TopDrawer2":
                        if (PlanScatterViewMiddle.Visibility == Visibility.Visible)
                            PlanScatterViewMiddle.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        TopDrawer2.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(END_OF_FIRST_DRAWER, END_OF_SECOND_DRAWER), _random.Next(0, 925));
                        break;
                    case "TopDrawer3":
                        if (PlanScatterViewRight.Visibility == Visibility.Visible)
                            PlanScatterViewRight.Items.Add(copyCard);
                        else if (DiscussScatterView.Visibility == Visibility.Visible)
                            DiscussScatterView.Items.Add(copyCard);
                        else
                            RiverScatterView.Items.Add(copyCard);
                        TopDrawer3.ActualDrawer.Items.Remove(thumbnailInDrawer);
                        copyCard.Center = new Point(_random.Next(END_OF_SECOND_DRAWER, END_OF_THIRD_DRAWER), _random.Next(0, 925));
                        break;
                    default:
                        break;
                }
            }
            */
        }

        #endregion

        #region PhotoCard Surface Button Event Handlers and Adding button to PhotoCard

        public void AddCommentButton(PhotoCard.PhotoCard photoCard)
        {
            //SurfaceButton commentButton = new SurfaceButton();
            //commentButton.Name = "CommentButton";
            //commentButton.Click += new RoutedEventHandler(CommentButton_Click);

            //photoCard.Back.Children.Add(commentButton);
            ////commentButton.Margin = new Thickness(100, 50, 100, 15);
            //commentButton.Margin = new Thickness(100, 100, 90, 30);
            //commentButton.Background = Brushes.Transparent;
        }

        void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            //get SVI container of photocard
            System.Windows.Controls.Button b = sender as System.Windows.Controls.Button;
            Grid g = b.Parent as Grid;
            Grid g2 = g.Parent as Grid;
            PhotoCard.PhotoCard mi = g2.Parent as PhotoCard.PhotoCard;
            ScatterViewItem originalSVI = mi.Parent as ScatterViewItem;

            //Test SVI's location to determine octant number
            int octantNum;

            Console.WriteLine("PhotoCard current center: " + originalSVI.Center);
            double yCoord = originalSVI.Center.Y;
            double xCoord = originalSVI.Center.X;

            #region NOT USED: boundary box checking to get octantNum (according to given SV size)
            //if ((xCoord >= 0 && xCoord < 810) &&
            //    (yCoord >= 0 && yCoord < 925))
            //    octantNum = 1;
            //else if ((xCoord >= 810 && xCoord < 1620) &&
            //    (yCoord >= 0 && yCoord < 925))
            //    octantNum = 2;
            //else if ((xCoord >= 1620 && xCoord < 2430) &&
            //    (yCoord >= 0 && yCoord < 925))
            //    octantNum = 3;
            //else if ((xCoord >= 2430 && xCoord < 3240) &&
            //    (yCoord >= 0 && yCoord < 925))
            //    octantNum = 4;
            //else if ((xCoord >= 0 && xCoord < 810) &&
            //     (yCoord >= 925 && yCoord < auto))
            //    octantNum = 5;
            //else if ((xCoord >= 810 && xCoord < 1620) &&
            //     (yCoord >= 925 && yCoord < auto))
            //    octantNum = 6;
            //else if ((xCoord >= 1620 && xCoord < 2430) &&
            //      (yCoord >= 925 && yCoord < auto))
            //    octantNum = 7;
            //else if ((xCoord >= 2430 && xCoord < 3240) &&
            //      (yCoord >= 925 && yCoord < auto))
            //    octantNum = 8;
            //else
            //    octantNum = 0;//not within Beast screen boundary or all the way on the edge
            #endregion

            #region boundary box checking to get octantNum (according to actual projection estimates 1/12/12)
            if ((xCoord >= 0 && xCoord < 812) &&
                (yCoord >= 0 && yCoord < 925))
            {
                octantNum = 1;
                Console.WriteLine("KEyboard in octant 1 activated");
            }
            else if ((xCoord >= 812 && xCoord < 1624) &&
                (yCoord >= 0 && yCoord < 925))
            {
                octantNum = 2;
                Console.WriteLine("KEyboard in octant 2 activated");
            }
            else if ((xCoord >= 1624 && xCoord < 2436) &&
                (yCoord >= 0 && yCoord < 925))
            {
                octantNum = 3;
                Console.WriteLine("KEyboard in octant 3 activated");
            }
            else if ((xCoord >= 2436 && xCoord < 3248) &&
                (yCoord >= 0 && yCoord < 925))
            {
                octantNum = 4;
                Console.WriteLine("KEyboard in octant 4 activated");
            }
            else if ((xCoord >= 0 && xCoord < 812) &&
                 (yCoord >= 925 && yCoord < 1850))
            {
                octantNum = 5;
                Console.WriteLine("KEyboard in octant 5 activated");
            }
            else if ((xCoord >= 812 && xCoord < 1624) &&
                 (yCoord >= 925 && yCoord < 1850))
            {
                octantNum = 6;
                Console.WriteLine("KEyboard in octant 6 activated");
            }
            else if ((xCoord >= 1624 && xCoord < 2436) &&
                  (yCoord >= 925 && yCoord < 1850))
            {
                octantNum = 7;
                Console.WriteLine("KEyboard in octant 7 activated");
            }
            else if ((xCoord >= 2436 && xCoord < 3248) &&
                  (yCoord >= 925 && yCoord < 1850))
            {
                octantNum = 8;
                Console.WriteLine("KEyboard in octant 8 activated");
            }
            else
            {
                octantNum = 0;//not within Beast screen boundary or all the way on the edge
                Console.WriteLine("keyboard outside of octant activated");
            }
            #endregion

            #region Activate corresponding keyboard for commenting
            ////IMPORTANT: Make sure keyboard handles specified here match currently connected devices
            ////(Changed for each device and each USB port if applicable)
            //switch (octantNum)
            //{
            //    case 1://HP Wireless keyboard 1
            //        ((Keyboard)_keyboardArray[0]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[0]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 2://HP Wireless keyboard 2
            //        ((Keyboard)_keyboardArray[1]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[1]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 3://HP Wireless keyboard 3 
            //        ((Keyboard)_keyboardArray[2]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[2]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 4://HP Wireless keyboard 4
            //        ((Keyboard)_keyboardArray[3]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[3]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 5: //GIGABYTE wired keyboard
            //        ((Keyboard)_keyboardArray[4]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[4]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 6: //Lenovo wired keyboard 1
            //        ((Keyboard)_keyboardArray[5]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[5]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 7: //Lenovo wired keyboard 2
            //        ((Keyboard)_keyboardArray[6]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[6]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    case 8: //Microsoft Bluetooth keyboard
            //        ((Keyboard)_keyboardArray[7]).IsEnabled = true;
            //        ((Keyboard)_keyboardArray[7]).AssociatedCommentArea = ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea; //assign current PhotoCard comment area to current Keyboard
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Background = new SolidColorBrush(Colors.White); //change comment area to indicate mode change
            //        ((PhotoCard.PhotoCard)originalSVI.Content).CommentArea.Focus();//reassign focus so button doens't blink
            //        break;
            //    default: break; //if not in octant, do nothing
            //}

            #endregion

        }

        #endregion

        #region 8 Switch Layer methods: (RiverButton_Click, PlanButton_Click, >Moved to drawer class) SwitchToPlanLayout, SwitchToRiver, CopyForTeam1Presentation, CopyForTeam2Presentation, CopyForTeam3Presentation

        public void SyncButtons(string switchingto)
        {
            switch(switchingto)
            {
               case "river" :
                    //TopDrawer1.SyncButtonsForRiver();
                    //BottomDrawer1.SyncButtonsForRiver();

                    //TopDrawer2.SyncButtonsForRiver();
                    //BottomDrawer2.SyncButtonsForRiver();

                    //TopDrawer3.SyncButtonsForRiver();
                    //BottomDrawer3.SyncButtonsForRiver();
                   break;
               case "plan":                   
                    //TopDrawer1.SyncButtonForPlan();
                    //BottomDrawer1.SyncButtonForPlan();
                   
                    //TopDrawer2.SyncButtonForPlan();
                    //BottomDrawer2.SyncButtonForPlan();
                   
                    //TopDrawer3.SyncButtonForPlan();
                    //BottomDrawer3.SyncButtonForPlan();                   
                   break;
               default:
                   break;
           }
        }

        public void SwitchToRiver(String drawer)
        {
            //starts up animation timers
            //_timer.Start();
            //_change_timer.Start();

            if (PlanScatterViewMiddle.Visibility == Visibility.Visible ||
                PlanScatterViewRight.Visibility == Visibility.Visible ||
                PlanScatterViewLeft.Visibility == Visibility.Visible)
            {
                if (drawer.Contains("1"))
                {
                    PlanScatterViewLeft.Visibility = Visibility.Collapsed;
                    //sync buttons
                    //TopDrawer1.SyncButtonsForRiver();
                    //BottomDrawer1.SyncButtonsForRiver();
                }
                else if (drawer.Contains("2"))
                {
                    PlanScatterViewMiddle.Visibility = Visibility.Collapsed;
                    //TopDrawer2.SyncButtonsForRiver();
                    //BottomDrawer2.SyncButtonsForRiver();
                }
                else if (drawer.Contains("3"))
                {
                    PlanScatterViewRight.Visibility = Visibility.Collapsed;
                    //TopDrawer3.SyncButtonsForRiver();
                    //BottomDrawer3.SyncButtonsForRiver();
                }
            }
            else if (DiscussScatterView.Visibility == Visibility.Visible)//we were presenting
            {
                DiscussScatterView.Visibility = Visibility.Collapsed;

                //TopDrawer1.SyncButtonsOutOfAudience();
                //BottomDrawer1.SyncButtonsOutOfAudience();
                //TopDrawer2.SyncButtonsOutOfAudience();
                //BottomDrawer2.SyncButtonsOutOfAudience();
                //TopDrawer3.SyncButtonsOutOfAudience();
                //BottomDrawer3.SyncButtonsOutOfAudience();
                //SyncButtons("river");
            }
        }

        public void SwitchToPlanLayout(string drawer)
        {
            if (DiscussScatterView.Visibility == Visibility.Visible) //we were in the discussion
            {
                //GrabAllCopiedCards(DiscussScatterView, PlanScatterViewMiddle);
                foreach (ScatterViewItem s in PlanScatterViewMiddle.Items)
                {
                    s.CanScale = true;
                }
                DiscussScatterView.Visibility = Visibility.Collapsed;

                //TopDrawer1.SyncButtonsOutOfAudience();
                //BottomDrawer1.SyncButtonsOutOfAudience();
                //TopDrawer2.SyncButtonsOutOfAudience();
                //BottomDrawer2.SyncButtonsOutOfAudience();
                //TopDrawer3.SyncButtonsOutOfAudience();
                //BottomDrawer3.SyncButtonsOutOfAudience();
                //SyncButtons("plan");

            }
            else //we were in the river
            {
                if (drawer.Contains("1"))
                {
                    GrabAllCopiedCards(RiverScatterView, PlanScatterViewLeft);
                    PlanScatterViewLeft.Visibility = Visibility.Visible;
                    //TopDrawer1.SyncButtonForPlan();
                    //BottomDrawer1.SyncButtonForPlan();
                }
                else if (drawer.Contains("2"))
                {
                    GrabAllCopiedCards(RiverScatterView, PlanScatterViewMiddle);
                    PlanScatterViewMiddle.Visibility = Visibility.Visible;
                    //TopDrawer2.SyncButtonForPlan();
                    //BottomDrawer2.SyncButtonForPlan();
                }
                else if (drawer.Contains("3"))
                {
                    GrabAllCopiedCards(RiverScatterView, PlanScatterViewRight);
                    PlanScatterViewRight.Visibility = Visibility.Visible;
                    //TopDrawer3.SyncButtonForPlan();
                    //BottomDrawer3.SyncButtonForPlan();
                }
            }
        }

        public void CopyForTeam1Presentation()
        {
            foreach (ScatterViewItem s in PlanScatterViewLeft.Items)
            {
                //ignore the floorplan SVIs
                if (s.Name.Equals("Copy"))
                {
                    if (s.Center.X < 1080)
                    {
                        PhotoCard.PhotoCard oCard = (PhotoCard.PhotoCard)s;
                        PhotoCard.PhotoCard copyCard = oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard2 = oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard3 = oCard.DeepClone();


                        DiscussScatterView.Items.Add(copyCard);
                        DiscussScatterView.Items.Add(copyCard2);
                        DiscussScatterView.Items.Add(copyCard3);

                        copyCard2.CanMove = false;
                        copyCard2.Name = "DoNotMove";
                        copyCard3.CanMove = false;
                        copyCard2.Name = "DoNotMove";

                        copyCard.Center = s.Center;
                        copyCard.Background = Brushes.Thistle;
                        copyCard2.Center = new Point(s.Center.X + END_OF_FIRST_DRAWER, s.Center.Y);
                        copyCard2.Background = Brushes.PowderBlue;
                        copyCard3.Center = new Point(s.Center.X + END_OF_SECOND_DRAWER, s.Center.Y);
                        copyCard3.Background = Brushes.Bisque;

                        copyCard.Orientation = s.Orientation;
                        copyCard2.Orientation = s.Orientation;
                        copyCard3.Orientation = s.Orientation;
                    }
                }
            }
            DiscussScatterView.Visibility = Visibility.Visible;

            //TopDrawer1.SyncButtonsAsPresenter();
            //BottomDrawer1.SyncButtonsAsPresenter();

            //TopDrawer2.SyncButtonsAsAudience();
            //BottomDrawer2.SyncButtonsAsAudience();
            //TopDrawer3.SyncButtonsAsAudience();
            //BottomDrawer3.SyncButtonsAsAudience();


        }

        public void CopyForTeam2Presentation()
        {
            foreach (ScatterViewItem s in PlanScatterViewMiddle.Items)
            {
                //ignore the floorplan SVIs
                if (s.Name.Equals("Copy"))
                {
                    if (s.Center.X > END_OF_FIRST_DRAWER && s.Center.X < END_OF_SECOND_DRAWER)
                    {
                        PhotoCard.PhotoCard oCard = (PhotoCard.PhotoCard)s;
                        PhotoCard.PhotoCard copyCard = (PhotoCard.PhotoCard)oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard2 = (PhotoCard.PhotoCard)oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard3 = (PhotoCard.PhotoCard)oCard.DeepClone();

                        copyCard.ImageDescription.Visibility = Visibility.Visible;
                        copyCard2.ImageDescription.Visibility = Visibility.Visible;
                        copyCard3.ImageDescription.Visibility = Visibility.Visible;

                        DiscussScatterView.Items.Add(copyCard);
                        DiscussScatterView.Items.Add(copyCard2);
                        DiscussScatterView.Items.Add(copyCard3);

                        copyCard2.CanMove = false;
                        copyCard2.Name = "DoNotMove";
                        copyCard3.CanMove = false;
                        copyCard2.Name = "DoNotMove";

                        copyCard.Center = s.Center;//middle
                        copyCard2.Center = new Point(s.Center.X + END_OF_FIRST_DRAWER, s.Center.Y);
                        copyCard3.Center = new Point(s.Center.X - END_OF_FIRST_DRAWER, s.Center.Y);

                        copyCard.Orientation = s.Orientation;
                        copyCard2.Orientation = s.Orientation;
                        copyCard3.Orientation = s.Orientation;
                                               
                    }
                }
            }
            DiscussScatterView.Visibility = Visibility.Visible;

            //TopDrawer1.SyncButtonsAsAudience();
            //BottomDrawer1.SyncButtonsAsAudience();

            //TopDrawer2.SyncButtonsAsPresenter();
            //BottomDrawer2.SyncButtonsAsPresenter();
           
            //TopDrawer3.SyncButtonsAsAudience();
            //BottomDrawer3.SyncButtonsAsAudience();

        }

        public void CopyForTeam3Presentation()
        {
            foreach (ScatterViewItem s in PlanScatterViewRight.Items)
            {
                //ignore the floorplan SVIs
                if (s.Name.Equals("Copy"))
                {
                    if (s.Center.X > 2160)
                    {
                        PhotoCard.PhotoCard oCard = (PhotoCard.PhotoCard)s;
                        PhotoCard.PhotoCard copyCard = (PhotoCard.PhotoCard)oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard2 = (PhotoCard.PhotoCard)oCard.DeepClone();
                        PhotoCard.PhotoCard copyCard3 = (PhotoCard.PhotoCard)oCard.DeepClone();

                        copyCard.ImageDescription.Visibility = Visibility.Visible;
                        copyCard2.ImageDescription.Visibility = Visibility.Visible;
                        copyCard3.ImageDescription.Visibility = Visibility.Visible;

                        DiscussScatterView.Items.Add(copyCard);
                        DiscussScatterView.Items.Add(copyCard2);
                        DiscussScatterView.Items.Add(copyCard3);

                        copyCard2.CanMove = false;
                        copyCard2.Name = "DoNotMove";
                        copyCard3.CanMove = false;
                        copyCard2.Name = "DoNotMove";

                        copyCard.Center = s.Center;//last 
                        copyCard2.Center = new Point(s.Center.X - END_OF_FIRST_DRAWER, s.Center.Y);
                        copyCard3.Center = new Point(s.Center.X - END_OF_SECOND_DRAWER, s.Center.Y);

                        copyCard.Orientation = s.Orientation;
                        copyCard2.Orientation = s.Orientation;
                        copyCard3.Orientation = s.Orientation;
                    }
                }
            }
            DiscussScatterView.Visibility = Visibility.Visible;

            //TopDrawer1.SyncButtonsAsAudience();
            //BottomDrawer1.SyncButtonsAsAudience();

            //TopDrawer2.SyncButtonsAsAudience();
            //BottomDrawer2.SyncButtonsAsAudience();

            //TopDrawer3.SyncButtonsAsPresenter();
            //BottomDrawer3.SyncButtonsAsPresenter();
        }

        /// <summary>
        /// Called when the "Present" button on drawer is pressed.
        /// Switch to presentation mode if everyone from TEAM has pressed button.
        /// Team 2 and 3 have 3 mewmbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void NotYetButton_Click(object sender, RoutedEventArgs e)
        {
            PlanScatterViewMiddle.T1Ready.Visibility = Visibility.Collapsed;

        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            SurfaceButton lockButton = sender as SurfaceButton;
            ScatterViewItem lockMomma = lockButton.Parent as ScatterViewItem;
            if (lockButton.Name.Equals("Team1Lock"))
            {
                foreach (ScatterViewItem s in DiscussScatterView.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            else if (lockButton.Name.Equals("Team2Lock"))
            {
                foreach (ScatterViewItem s in DiscussScatterView.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            else//team3
            {
                foreach (ScatterViewItem s in DiscussScatterView.Items)
                {
                    if (s.Name.Equals("DoNotMove"))
                        s.CanMove = true;
                }
            }
            lockMomma.Visibility = Visibility.Collapsed;
        }

        private void HideAlert_Click(object sender, RoutedEventArgs e)
        {
            if (PlanScatterViewMiddle.T1NoAlert.Visibility == Visibility.Visible)
            {
                PlanScatterViewMiddle.T1NoAlert.Visibility = Visibility.Collapsed;
                //Team1Top.IsEnabled = true;
                //Team1Bottom.IsEnabled = true;
            }

        }



        /// <summary>
        /// Go through all svis in scatter. Check if they are "Copy"ies and 
        /// if they have been rated. THen copy those to next layer
        /// </summary>
        private void GrabAllCopiedCards(ScatterView switchingFrom, ScatterView switchingTo)
        {
            foreach (ScatterViewItem s in switchingFrom.Items)
            {
                if (s.Name.Equals("Copy"))
                {
                    PhotoCard.PhotoCard card = (PhotoCard.PhotoCard)s;
                    //if (card.TagList.Count > 0)
                    //{
                    PhotoCard.PhotoCard copy = (PhotoCard.PhotoCard)card.DeepClone();
                    copy.Name = "Copy";
                    switchingTo.Items.Add(copy);
                    copy.Center = card.Center;
                    copy.Orientation = card.Orientation;
                    //}
                }
            }
        }

        #endregion

        #region Multi-Keyboard: Modified Code from WPFRawInput Sample Code file: http://www.codeproject.com/KB/system/rawinput.aspx?fid=375378&df=90&mpp=25&sort=Position&tid=3555672

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _mainHwndSource = (HwndSource)PresentationSource.FromVisual(this);
            //source.AddHook(new HwndSourceHook(HandleMessages));

            StartWndProcHandler();
        }

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (id != null)
            {
                #region Sample code author note:
                // I could have done one of two things here.
                // 1. Use a Message as it was used before.
                // 2. Changes the ProcessMessage method to handle all of these parameters(more work).
                //    I opted for the easy way.

                //Note: Depending on your application you may or may not want to set the handled param.
                #endregion

                message.HWnd = hwnd;
                message.Msg = msg;
                message.LParam = lParam;
                message.WParam = wParam;

                id.ProcessMessage(message);
            }
            return IntPtr.Zero;
        }


        /// <summary>
        /// Method for handling event when a key is pressed. Translates virtual key code into
        /// corresponding alphanumeric characters, and output key to specified field.
        /// </summary>
        /// @T. Feng 1/4/2012
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyPressed(object sender, InputDevice.KeyControlEventArgs e)
        {
            //If keyboard handle not enough to identify keyboard, use keyboard name

            String virtualKey = e.Keyboard.vKey;
            String handle = e.Keyboard.deviceHandle.ToString();

            Keyboard currentKeyboard = (Keyboard)_keyboardList[handle];

            //Only update comments if current keyboard is enabled
            if (currentKeyboard.IsEnabled)
            {
                if (currentKeyboard.AssociatedCommentArea != null)
                    OutputKeyboardInput(mapKeyfromInput(virtualKey, currentKeyboard), currentKeyboard);
            }
        }

        /// <summary>
        /// Helpder method for _KeyPressed(). Given a virtual key from input device, translate it into corresponding character for 
        /// output (takes into account keyboard's CapsLk, Shift, and Backspace booleans). 
        /// </summary>
        /// @T.Feng 1/5/2012
        /// <param name="virtualKeyCode">Virtual Key corresponding to the physical key pressed on the keyboard</param>
        /// <param name="vkeySender">Keyboard object that sent the virtual key</param>
        /// <returns>Translated output string from the corresponding virtual key</returns>
        private String mapKeyfromInput(String virtualKeyCode, Keyboard vkeySender)
        {
            String mappedKey = "";

            if (virtualKeyCode.Length == 1)
            {
                #region alphabets
                if (vkeySender.CapsLkOn || vkeySender.ShiftPressed)
                {
                    mappedKey = virtualKeyCode.ToUpper();
                    vkeySender.ShiftPressed = false;
                }
                else
                    mappedKey = virtualKeyCode.ToLower();
                #endregion
            }
            else if (virtualKeyCode.Length == 2 && virtualKeyCode[0] == 'D')
            {
                #region numeric keys and their shift alternatives
                if (!vkeySender.ShiftPressed)
                    mappedKey = virtualKeyCode[1].ToString();//output numbers of shift's not pressed
                else //handle numeric keys with shift held
                {
                    switch (virtualKeyCode[1])
                    {
                        case '1': mappedKey = "!"; break;
                        case '2': mappedKey = "@"; break;
                        case '3': mappedKey = "#"; break;
                        case '4': mappedKey = "$"; break;
                        case '5': mappedKey = "%"; break;
                        case '6': mappedKey = "^"; break;
                        case '7': mappedKey = "&"; break;
                        case '8': mappedKey = "*"; break;
                        case '9': mappedKey = "("; break;
                        case '0': mappedKey = ")"; break;
                        default: mappedKey = ""; Console.WriteLine("No mapping of current key: " + virtualKeyCode); break;
                    }
                    vkeySender.ShiftPressed = false;
                }
                #endregion
            }
            else
            {
                #region special keys (i.e. SPACE, RETURN, punctuations, etc.)

                switch (virtualKeyCode)
                {
                    case "Oemtilde": mappedKey = (vkeySender.ShiftPressed) ? "~" : "`"; break;
                    case "OemMinus": mappedKey = (vkeySender.ShiftPressed) ? "_" : "-"; break;
                    case "Oemplus": mappedKey = (vkeySender.ShiftPressed) ? "+" : "="; break;
                    case "OemOpenBrackets": mappedKey = (vkeySender.ShiftPressed) ? "{" : "["; break;
                    case "Oem6": mappedKey = (vkeySender.ShiftPressed) ? "}" : "]"; break;
                    case "Oem5": mappedKey = (vkeySender.ShiftPressed) ? "|" : "\\"; break;
                    case "Oem7": mappedKey = (vkeySender.ShiftPressed) ? "\"" : "'"; break;
                    case "Oem1": mappedKey = (vkeySender.ShiftPressed) ? ":" : ";"; break;
                    case "Oemcomma": mappedKey = (vkeySender.ShiftPressed) ? "<" : ","; break;
                    case "OemPeriod": mappedKey = (vkeySender.ShiftPressed) ? ">" : "."; break;
                    case "OemQuestion": mappedKey = (vkeySender.ShiftPressed) ? "?" : "/"; break;
                    default: mappedKey = "passedShift"; Console.WriteLine("Uncaught special key: " + virtualKeyCode); break; //any uncaught special keys outputs to console only
                }
                vkeySender.ShiftPressed = false;

                //conditional used to handle SHIFT key being pressed previously for special character alternates
                if (mappedKey == "passedShift")
                {
                    switch (virtualKeyCode)
                    {
                        case "Return": mappedKey = "\n"; break; //new line for "Return"
                        case "Space": mappedKey = " "; break;
                        case "Capital": vkeySender.CapsLkOn = (vkeySender.CapsLkOn) ? false : true; mappedKey = ""; break;//toggle capsLkOn boolean
                        case "ShiftKey": vkeySender.ShiftPressed = true; mappedKey = ""; break;
                        case "Back": vkeySender.BackspacePressed = true; mappedKey = ""; break;
                        case "Tab": mappedKey = "   "; break;
                        case "ControlKey": mappedKey = ""; break; //Ctrl do nothing for now
                        case "Menu": mappedKey = ""; break; //Alt do nothing for now
                        case "Escape": mappedKey = ""; //disable keyboard, break linkage to PhotoCard
                            vkeySender.IsEnabled = false;
                            vkeySender.AssociatedCommentArea.Background = new SolidColorBrush(Colors.DarkGray);
                            vkeySender.AssociatedCommentArea = null;
                            break;
                        default: mappedKey = ""; Console.WriteLine("Uncaught special key: " + virtualKeyCode); break; //any uncaught special keys outputs to console only
                    }
                }

                #endregion
            }

            return mappedKey;
        }

        /// <summary>
        /// Helper method for _KeyPressed(). Update specified TextBlock's text with keyboard's input
        /// </summary>
        /// @T.Feng 1/5/2012
        /// <param name="outputContent">String to be added to designated TextBlock</param>
        /// <param name="inputSender">Keyboard associated with TextBlock</param>
        private void OutputKeyboardInput(String outputContent, Keyboard inputSender)
        {
            TextBlock outputDestination = inputSender.AssociatedCommentArea;
            if (outputDestination != null)
            {
                outputDestination.Text += outputContent; //concatenate string to original text 

                //Handles event when Backspace key is pressed to erase a character. In this case, the outputContent
                //would've been empty. So remove one character from original TextBlock's text.
                if (inputSender.BackspacePressed && outputDestination.Text.Length > 0)
                    outputDestination.Text = outputDestination.Text.Substring(0, outputDestination.Text.Length - 1);
            }
            inputSender.BackspacePressed = false; //reset backspace bool
        }


        private void StartWndProcHandler()
        {
            #region Non-applicable code (for use with Windows.Form applications)
            //IntPtr hwnd = IntPtr.Zero;

            //Window myWin = System.Windows.Application.Current.MainWindow;

            //try
            //{
            //    hwnd = new WindowInteropHelper(myWin).Handle;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //Get the Hwnd source   
            //HwndSource source = HwndSource.FromHwnd(hwnd); 
            #endregion
            //Hwnd handle for WPF populated in OnSourceInitialized()

            //Win32 queue sink
            _mainHwndSource.AddHook(new HwndSourceHook(WndProc));

            id = new InputDevice(_mainHwndSource.Handle);

            _numberOfKeyboards = id.EnumerateDevices();
            id.KeyPressed += new InputDevice.DeviceEventHandler(_KeyPressed);

            _keyboardArray = new ArrayList();

            //Create a Keyboard object for each recognized and registered device in the list (8 in total)
            foreach (Object keyboardDevice in id.DeviceList)
            {
                InputDevice.DeviceInfo currentDevice = (InputDevice.DeviceInfo)((DictionaryEntry)keyboardDevice).Value;
                Keyboard newKeyboard = new Keyboard(currentDevice.deviceHandle.ToString(), (currentDevice.deviceName));
                _keyboardList.Add(newKeyboard.Handle, newKeyboard); //add Keyboard to table 
                _keyboardArray.Add(newKeyboard);
                Console.WriteLine("handle: " + newKeyboard.Handle);
            }
            Console.WriteLine("Total keyboards registered: " + _keyboardList.Count);

        }


        #endregion

        #region default Window Interaction: 6 Functions

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }
        #endregion

        private void Present_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}