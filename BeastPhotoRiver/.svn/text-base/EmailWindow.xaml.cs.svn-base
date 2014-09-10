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
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.Net.Mail;

namespace BeastPhotoRiver
{
    /// <summary>
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : System.Windows.Controls.UserControl
    {
        public EmailWindow()
        {
            InitializeComponent();
            
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            string toAddress = ToTextBox.Text;
            string ccAddress = CCTextBox.Text;
            string subject = SubjectTextBox.Text;
            string messageBody = MessageTextBox.Text;

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
    }
}
