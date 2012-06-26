using System;
using Microsoft.Phone.Controls;

namespace ThumbReg
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();            
            App.IdentityViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(IdentityViewModel_PropertyChanged);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {            
            base.OnNavigatedTo(e);
            txtPassword.Password = App.IdentityViewModel.Password;
            txtUrl.Text = App.IdentityViewModel.HostAddr;
            txtUserName.Text = App.IdentityViewModel.User;
        }

        void IdentityViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Token") {
                if (App.IdentityViewModel.Token != null)
                {
                    lblConnected.Text = "Connected";
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                }
                else { 
                    lblConnected.Text = "Login failed";
                }
                
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            
            lblConnected.Text = "Signing in...";
            App.IdentityViewModel.User = txtUserName.Text;
            App.IdentityViewModel.Password = txtPassword.Password;
            App.IdentityViewModel.HostAddr = txtUrl.Text;
            App.IdentityViewModel.CheckConnection();

        }

        private void About_Click(object sender, EventArgs e)
        {
              NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
    }         
}