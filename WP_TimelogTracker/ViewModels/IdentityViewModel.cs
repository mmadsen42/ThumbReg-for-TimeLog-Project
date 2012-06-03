using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Linq;
using System.Windows.Shapes;
using WP_TimelogTracker.tlp;
using System.ComponentModel;
using System.Windows.Navigation;
using WP_TimelogTracker.tlpSecurity;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;

namespace WP_TimelogTracker.ViewModels
{
         
    public class IdentityViewModel
    {
        private IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;

        tlpSecurity.SecurityToken _token;
        public tlpSecurity.SecurityToken Token 
        { 
            get 
            {
                return _token;
            }
            set 
            { 
                _token = value;
                OnPropertyChanged("Token");            
            }
        }
    
        //private string _user =  
        public string User 
        {
            get 
            {
                return _isoStore.Contains("_user") ? _isoStore["_user"] as string : "";
            }
            set 
            {
                _isoStore["_user"] = value;
            }
        }

        
        public string Password
        {
            get
            {
                return _isoStore.Contains("_password") ? _isoStore["_password"] as string : "";
            }
            set 
            {
                _isoStore["_password"] = value;
            }
        }

        WP_TimelogTracker.tlp.SecurityToken _projectToken;
        public WP_TimelogTracker.tlp.SecurityToken ProjectToken {        
            get 
            {
                return _projectToken;
            }
            set 
            { 
                _projectToken = value;
                OnPropertyChanged("ProjectToken");            
            }
        }
        //public event PropertyChangedEventHandler PropertyChanged;




        public string HostAddr {
            get {
                return _isoStore.Contains("_hostAddr") ? _isoStore["_hostAddr"] as string : "";    
            }
            set {
                _isoStore["_hostAddr"] = value;
            }
        }

        public void CheckConnection()
        {
            //string _addSecurity = "https://app.timelog.dk/local/WebServices/Security/V1_0/SecurityServiceSecure.svc";
            //var _add = new System.ServiceModel.EndpointAddress();
            var _client = new SecurityServiceClient();
            _client.GetTokenCompleted += new EventHandler<GetTokenCompletedEventArgs>(_client_GetTokenCompleted);
            _client.GetTokenAsync(new GetTokenRequest(App.IdentityViewModel.User, App.IdentityViewModel.Password));
            
        }

        void _client_GetTokenCompleted(object sender, GetTokenCompletedEventArgs e)
        {
            if (e.Result.GetTokenResult.ErrorCode == 0) 
            {
                Token = e.Result.GetTokenResult.Return[0];           

            }
        }

        private void OnPropertyChanged(string name)
          {
              PropertyChangedEventHandler handler = PropertyChanged;
              if (handler != null)
              {
                  handler(this, new PropertyChangedEventArgs(name));
              }
          }

        public event PropertyChangedEventHandler PropertyChanged;



        internal void Login()
        {
              WP_TimelogTracker.tlpSecurity.SecurityServiceClient _secClient = new WP_TimelogTracker.tlpSecurity.SecurityServiceClient();
            _secClient.GetTokenCompleted += new EventHandler<WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs>(_secClient_GetTokenCompleted);
            _secClient.GetTokenAsync(new tlpSecurity.GetTokenRequest(App.IdentityViewModel.User, App.IdentityViewModel.Password));
        }


        void _secClient_GetTokenCompleted(object sender, WP_TimelogTracker.tlpSecurity.GetTokenCompletedEventArgs e)
        {
            try
            {
                tlpSecurity.SecurityToken _token = e.Result.GetTokenResult.Return.FirstOrDefault();
                if (_token == null)
                {
                    throw new Exception("Unable to connect to the service: " + e.Result.GetTokenResult.Messages[0].Message);
                }
                tlp.SecurityToken _prjToken = new tlp.SecurityToken()
                {
                    Expires = _token.Expires,
                    Hash = _token.Hash,
                    Initials = _token.Initials

                };
                App.IdentityViewModel.ProjectToken = _prjToken;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
