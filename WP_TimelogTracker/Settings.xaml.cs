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
           
            txtPassword.Password = String.IsNullOrEmpty(App.IdentityViewModel.Password) ? "tsgtsg" : App.IdentityViewModel.Password;
            txtUrl.Text = String.IsNullOrWhiteSpace(App.IdentityViewModel.HostAddr) ? "app.timelog.dk/tracker_demo" : App.IdentityViewModel.HostAddr;
            txtUserName.Text = String.IsNullOrWhiteSpace(App.IdentityViewModel.User) ? "demo" : App.IdentityViewModel.User;
            try
            {
                NavigationService.RemoveBackEntry();
            }catch(InvalidOperationException){}
        }

        void IdentityViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Progress.IsVisible = false;
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

            SignIn();

        }

        private void SignIn()
        {
            lblConnected.Text = "Signing in...";
            Progress.IsVisible = true;
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