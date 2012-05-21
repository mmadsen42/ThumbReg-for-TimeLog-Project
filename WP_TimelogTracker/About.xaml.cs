using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace WP_TimelogTracker
{
    public partial class About : PhoneApplicationPage
    {
        private string _version = "Version: 0.1 BETA";
        public About()
        {
            InitializeComponent();
            txtVersion.Text = _version;
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask _email = new EmailComposeTask{
                    To = "mraaskov@hotmail.com",
                    Subject="Thumb reg for TimeLog Project",
                    Body = Environment.NewLine + Environment.NewLine + _version
            };
            _email.Show();
            
        }
    }
}