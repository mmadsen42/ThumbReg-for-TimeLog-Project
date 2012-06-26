using System;
using System.Linq;
using ThumbReg.tlpSecurity;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using SecurityToken = ThumbReg.tlp.SecurityToken;

namespace ThumbReg.ViewModels
{
         
    public class IdentityViewModel
    {
        private readonly IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;

        tlpSecurity.SecurityToken _token;
        public tlpSecurity.SecurityToken Token 
        { 
            get 
            {
                return _token;
            }
            private set 
            { 
                _token = value;
                OnPropertyChanged("Token");            
            }
        }
    
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

        SecurityToken _projectToken;
        public SecurityToken ProjectToken {        
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
            var client = new SecurityServiceClient();
            client.GetTokenCompleted += ClientGetTokenCompleted;
            client.GetTokenAsync(new GetTokenRequest(App.IdentityViewModel.User, App.IdentityViewModel.Password));
        }

        void ClientGetTokenCompleted(object sender, GetTokenCompletedEventArgs e)
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
            var secClient = new SecurityServiceClient();
            secClient.GetTokenCompleted += _secClient_GetTokenCompleted;
            secClient.GetTokenAsync(new GetTokenRequest(App.IdentityViewModel.User, App.IdentityViewModel.Password));
        }


        void _secClient_GetTokenCompleted(object sender, GetTokenCompletedEventArgs e)
        {
            tlpSecurity.SecurityToken token = e.Result.GetTokenResult.Return.FirstOrDefault();
            if (token == null)
            {
                throw new Exception("Unable to connect to the service: " + e.Result.GetTokenResult.Messages[0].Message);
            }
            var prjToken = new SecurityToken
                               {
                                   Expires = token.Expires,
                                   Hash = token.Hash,
                                   Initials = token.Initials

                               };

            App.IdentityViewModel.ProjectToken = prjToken;
        }
    }
}
