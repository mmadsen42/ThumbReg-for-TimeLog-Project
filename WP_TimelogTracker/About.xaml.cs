namespace ThumbReg
{
    using System;
    using System.Windows;
    using Microsoft.Phone.Tasks;

    public partial class About
    {
        private const string Version = "Version: 0.1 BETA";

        /// <summary>
        /// CTOR
        /// </summary>
        public About()
        {
            InitializeComponent();
            txtVersion.Text = Version;
        }

        private void BtnSendMailClick(object sender, RoutedEventArgs e)
        {
            var email = new EmailComposeTask
                                          {
                                              To = "mraaskov@hotmail.com",
                                              Subject = "Thumb reg for TimeLog Project",
                                              Body = Environment.NewLine + Environment.NewLine + Version
                                          };
            email.Show();           
        }
    }
}